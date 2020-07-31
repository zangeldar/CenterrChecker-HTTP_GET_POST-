using System;
using System.Collections.Generic;
using System.Text;

namespace MyHTMLParser
{
    /// <summary>
    /// Базовый класс ПРОТО-ТЕГ, содержит только значение Value, признак IsProto и ссылку на Родителя Parent
    /// К примеру, для строки <title>Поиск тендеров и электронных торгов по 223-ФЗ, 44-ФЗ и коммерческих закупок: все регионы России</title>
    ///     ProtoTag будет содержать:
    ///         Value = "Поиск тендеров и электронных торгов по 223-ФЗ, 44-ФЗ и коммерческих закупок: все регионы России"
    ///         Parent = Tag.Title
    /// </summary>
    public class ProtoTag : IComparable<ProtoTag>
    {
        public override string ToString()
        {
            //return base.ToString();
            return Value;
        }        
        virtual public bool IsProto { get; protected set; }
        public string Value { get; protected set; }
        public int Level { get; private set; }
        public ProtoTag Parent { get; private set; }
        /*
        public ProtoTag(string inpString, int level=0)
        {
            this.IsProto = true;
            this.Value = inpString;
            this.Level = level;
        }
        */
        public ProtoTag(string inpString, ProtoTag parent)
        {
            this.IsProto = true;
            this.Value = inpString;
            this.Parent = parent;
            this.Level = 0;
            if (parent != null)
                this.Level = parent.Level + 1;
        }

        int IComparable<ProtoTag>.CompareTo(ProtoTag other)
        {
            //throw new NotImplementedException();
            if (other == null) return 1;
            return Level.CompareTo(other.Level);            
        }
    }

    /// <summary>
    /// Класс Tag, описывающий любой ТЕГ HTML
    /// Содержит:
    ///     IsProto         - признак, что класс может быть приведен к PrototTag без потерь
    ///     IsSelfClosed    - признак самозакрытого ТЕГа
    ///     Name            - имя ТЕГа ("html", "head", "body" etc..)
    ///     SourceString    - исходная строка, переданная в конструктор
    ///     Attributes      - словарь Аттрибутов имени ТЕГа
    ///     ChildTags       - список дочерних ТЕГов
    ///     CutOffBefore    - строка ДО начала текущего ТЕГа
    ///     CutOffAfter     - строка ПОСЛЕ конца текущего (имени?) ТЕГа
    ///     ErrorCount      - счетчик ошибок разбора HTML (на случай не полноценной входящей строки, отрезка, выдержки)
    ///     
    ///     Механизм разбора следующий:
    ///     1. В конструктор передается строка HTML целиком
    ///     2. В конструкторе вызывается функция MakeTag, в которой происходит все самое интересное
    ///     3. В первую очередь, отправляется на заполнение заголовка ТЕГа в FillTagHeader
    ///     4. Там разыскивается первый сегмент между символами "<" и ">", заполняется имя ТЕГа и аттрибуты. 
    ///         Если строка не содержит эти символы, или содержит в неправильном порядке, то ТЕГ назначается ПРОТО-ТЕГом и строка до первого "<" заносится как его значение.
    ///     5. FillTagHeader возвращает строку, обрезанную на разобранный ранее ТЕГ.
    ///     6. Затем возвращенная строка разбирается на внутренние ТЕГи, которые рекурсивно добавляются в список ChildTag
    ///     7. Если возвращенная строка нового ChildTag начинается с "</"+ИмяТЕГа, значит текущий ТЕГ закрывается.
    ///     
    /// </summary>
    public class Tag : ProtoTag
    {
        public override string ToString()
        {
            if (IsProto)
                return base.ToString();
            
            string result = String.Format("Lvl: {2} | Name: {0} | ChildCount: {1}", Name, ChildTags.Count, Level);
            if (Attributes.ContainsKey("class"))
                result += " | " + Attributes["class"];
            return result;
        }        
        public override bool IsProto
        {
            get => base.IsProto;
            protected set
            {
                IsSelfClosed = true;
                base.IsProto = value;                
            }
        }
        public bool IsSelfClosed { get; private set; }
        public bool IsComment { get; private set; }
        public string Name { get; private set; }
        public string SourceString { get; private set; }
        public Dictionary<string, string> Attributes { get; private set; }
        public List<Tag> ChildTags { get; private set; }        
        public string CutOffBefore { get; private set; }
        public string CutOffAfter { get; private set; }
        public int ErrorCount { get; private set; }

        /*
        public Tag(string inpString, int level=0) : base(inpString, level)
        {
            MakeTag(inpString);
        }
        */

        public Tag(string inpString, Tag parent) : base(inpString, parent)
        {
            MakeTag(inpString);
        }

        public Tag(string inpString, Tag parent, bool isProto=false) : base(inpString, parent)
        {
            if (!isProto)
                MakeTag(inpString);
            IsProto = isProto;
        }

        private void MakeTag(string inpString)
        {            
           IsProto = false;
            SourceString = inpString;
            Initialise();
            /*
            if (!FillTagHeader(inpString))
                return;
                */
            CutOffAfter = FillTagHeader(inpString.Trim());

            // если имени нет, то это ПРОТО-ТЕГ (просто значение)
            if (Name == "" || Name == null)
            {
                //this = (ProtoTag)this;
                IsProto = true;
                return;
            }

            if (IsSelfClosed)
                return;

            // если CutOffAfter начинается с закрытия одноименного ТЕГа, то этот ТЕГ считается закрытым и созданным
            //if (CutOffAfter.IndexOf("</" + Name) == 0)
            if (CutOffAfter.StartsWith("</" + Name)) 
            {
                CutOffAfter = CutOffAfter.Substring(CutOffAfter.IndexOf(">") + 1).Trim();
                return;
            }

            if (Name == "script")
            {                 
                int endTagInd = CutOffAfter.IndexOf("</" + Name);
                string value = CutOffAfter.Substring(0, endTagInd-0);
                ChildTags.Add(new Tag(value, this, true));
                CutOffAfter = CutOffAfter.Substring(endTagInd);
                endTagInd = CutOffAfter.IndexOf(">");
                CutOffAfter = CutOffAfter.Substring(endTagInd+1).Trim();
                return;
            }

            // далее обрабатываем содержимое ТЕГа, если оно есть
            string cutOffAfter = CutOffAfter;
            int watchDog = 0;
            while (cutOffAfter.Length > 0)
            {
                watchDog = cutOffAfter.Length;


                Tag inTag = new Tag(cutOffAfter, this);

                cutOffAfter = inTag.CutOffAfter.Trim();
                /* // отключим, чтобы не париться с кастованием
                // Если ТЕГ был ПРОТО-ТЕГ, 
                if (inTag.IsProto)
                {
                    // то добавляем его как ПРОТО-ТЕГ
                    //ChildTags.Add((ProtoTag)inTag);
                    ChildTags.Add(inTag as ProtoTag);
                }
                else
                    */
                    ChildTags.Add(inTag);

                // и проверяем, не закрылся ли после него родительский ТЕГ
                //if (cutOffAfter.IndexOf("</" + Name) == 0)
                if (cutOffAfter.StartsWith("</" + Name))
                {
                    CutOffAfter = cutOffAfter.Substring(cutOffAfter.IndexOf(">") + 1).Trim();
                    break;
                }

                if (watchDog == cutOffAfter.Length)
                    throw new Exception("Warning! Infinity cycle detected! String value: " + cutOffAfter);
                // убираем оборванные закрывающие ТЕГи на случай созданиа ТЕГа из части HTML
                while (cutOffAfter.StartsWith("</"))
                {
                    cutOffAfter = cutOffAfter.Substring(cutOffAfter.IndexOf(">") + 1).Trim();
                    ErrorCount++;
                }
            }
        }

        /// <summary>
        ///  Инициализируем параметры ТЕГа
        /// </summary>
        private void Initialise()
        {
            IsSelfClosed = false;
            Attributes = new Dictionary<string, string>();
            ChildTags = new List<Tag>();
        }
        /// <summary>
        /// Заполняем имя первого ТЕГа из входящей строки HTML.
        /// Если до ТЕГа имелась какая-то информация, она заносится в параметр CutOffBefore
        /// После заполнения имени ТЕГа, здесь же вызывается фнукция, заполняющая аттрибуты ТЕГа
        /// </summary>
        /// <param name="inpString">Входящая строка HTML</param>
        /// <param name="startTag">Символ/Строка символа, обозначающего начало ТЕГа, по умолчанию "<"</param>
        /// <param name="endTag">Символ/Строка символа, обозначающего конец ТЕГа, по умолчанию ">"</param>
        /// <returns>Если строка представляет некорректный HTML, то вызывается исключение, или возвращается false </returns>
        private string FillTagHeader(string inpString, string startTag="<", string endTag = ">")
        {
            //bool result = false;

            if (inpString.StartsWith("<!--"))
            {
                //Name = inpString.Substring(0+1, endTagInd-0-1);                

                int curEndTagInd = GetRealEndOfTagName(inpString, "<!--", "-->");
                Value = inpString.Substring(0 + 1, curEndTagInd - 0 - 1 + 2);
                IsProto = true;
                IsComment = true;
                return inpString.Substring(curEndTagInd + 3).Trim();

                //return inpString.Substring(endTagInd+1);

            }

            int startTagInd, endTagInd;
            // находим начало тега
            // и проверяем, есть ли оно
            startTagInd = inpString.IndexOf("<");            
            if (startTagInd < 0)
            {
                Value = inpString;
                IsProto = true;
                //throw new Exception("Couldn't find start of Tag \"" + startTag + "\"");
                //return false;
                //return -1;
                return "";
            } 
            else if (startTagInd > 0)   // если до открытия тега были данные, то сохраняем их как CutOffbefore
            {
                //CutOffBefore = inpString.Substring(0, startTagInd - 0);                   //  то сохраняем их как CutOffbefore
                //ChildTags.Add(new ProtoTag(inpString.Substring(0, startTagInd - 0)));     // то сохраняем их как протоТег во внутренние теги.
                Value = inpString.Substring(0, startTagInd - 0);                            // то это - протоТЕГ
                IsProto = true;
                return inpString.Substring(startTagInd);
            }
            // находим конец тега            
            // и проверяем, есть ли он

            // ВОЗМОЖНА ИСКЛЮЧИТЕЛЬНАЯ СИТУАЦИЯ, КОГДА ВНУТРИ АТТРИБУТОВ ЕСТЬ ТЕГи и символы "<" или ">": 
            // <button name="button" type="submit" class="headerControls__search button button--square40 button--asGainsboroughFrame" title="<span class="translation_missing" title="translation missing: ru.header.titles.search">Search</span>" data-selector="headerControlsSearchButton">
            // Надо посчитать, сколько открывающих "<" встретилось до ">" и искать следующую ">"

            //endTagInd = inpString.IndexOf(">");            
            endTagInd = GetRealEndOfTagName(inpString, startTag, endTag);
            if (endTagInd < 0)
            {
                Value = inpString;
                IsProto = true;
                //throw new Exception("Couldn't find end of Tag \"" + endTag + "\"");
                //return false;
                //return -1;
                return "";
            }
            // проверяем, чтобы конец был только после начала
            if (endTagInd < startTagInd)
            {
                Value = inpString.Substring(0, startTagInd - 0);
                IsProto = true;
                //throw new Exception("Incorrect format: \"" + endTag + "\" can't be before " + "\"" + startTag + "\"");
                //return false;
                //return -1;
                //return "";
                return inpString.Substring(startTagInd);
            }

           


            // ищем конец имени тега
            // и заполняем ИмяТега
            int endTagName;
            //char[] separator = new char[]{ ' ', '/', '>' };
            char[] separator = new char[] { ' ', '>' };
            endTagName = inpString.IndexOfAny(separator, startTagInd);            
            Name = inpString.Substring(startTagInd+1, endTagName - startTagInd - 1).Trim();

            // если тег - meta, тогда метим его самозакрытым и заполняем аттрибуты
            IsSelfClosed = (Name.StartsWith("meta") || IsSelfClosed);
            IsSelfClosed = (Name.StartsWith("link") || IsSelfClosed);
            IsSelfClosed = (Name.StartsWith("br") || IsSelfClosed);
            IsSelfClosed = (Name.StartsWith("hr") || IsSelfClosed);
            IsSelfClosed = (Name.StartsWith("img") || IsSelfClosed);
            IsSelfClosed = (Name.StartsWith("input") || IsSelfClosed);

            if (Name.StartsWith("!"))
            {
                IsSelfClosed = true;
                /*
                if (Name.StartsWith("!--"))
                {
                    //Name = inpString.Substring(0+1, endTagInd-0-1);

                    int curEndTagInd = GetRealEndOfTagName(inpString, "<--", "-->");
                    Name = inpString.Substring(0 + 1, curEndTagInd - 0 - 1 + 2);
                    return inpString.Substring(curEndTagInd + 3).Trim();
                    
                    //return inpString.Substring(endTagInd+1);

                }
                */
            }

            // что это за дичь?
            /*
            {
                IsSelfClosed = true;
                //CutOffBefore = inpString.Substring(endTagName+1);
                //return true;
            }
            */

            // заполняем аттрибуты, если в возвращаемом ответе остался символ "/", значит тег самозакрытый
            string attResult = ParseAttributes(inpString.Substring(endTagName, endTagInd - endTagName));
            if (attResult.StartsWith("/"))
                IsSelfClosed = true;
            //else                
                //Attributes.Add(attResult, "");            

            //result = true;
            //return result;
            //return endTagInd;
            return inpString.Substring(endTagInd+1).Trim();
        }

        private int GetRealEndOfTagName(string inpString, string startTag="<", string endTag=">")
        {
            int result = -1;
            //int startTagInd = inpString.IndexOf("<", 1); // = 0
            int startTagInd = 0;
            
            while (startTagInd > -1)
            {
                result = inpString.IndexOf(endTag,result+1);
                startTagInd = inpString.IndexOf(startTag, startTagInd+1, result - startTagInd);
            }

            return result;
        }

        /// <summary>
        ///  Парсим аттрибуты ТЕГа
        /// </summary>
        /// <param name="inpString">Строка аттрибутов открывающего тега</param>
        /// <returns></returns>
        private string ParseAttributes(string inpString)
        {
            inpString = inpString.Trim();
            bool openQuotes = false;
            bool noQuotesButValue = false;
            string currentStr = "";
            string attName = "";
            string attValue = "";
            char prevChar = new Char();
            foreach (char item in inpString)
            {                
                if (item == '"' || item == '\'' || noQuotesButValue)
                {
                    if (openQuotes) // если до этого кавычки были открыты, то curStr  - это значение
                    {
                        attValue = currentStr;
                        currentStr = "";
                        if (attName == "")
                        {
                            if (Attributes.ContainsKey(attValue))
                                attValue += attValue;
                            Attributes.Add(attValue, attName);
                        }                            
                        else
                        {
                            if (Attributes.ContainsKey(attName))
                                attName += attName;
                            Attributes.Add(attName, attValue);
                        }
                            
                        attName = "";
                        attValue = "";
                        noQuotesButValue = false;
                    }
                    openQuotes = !openQuotes;
                    prevChar = item;
                    continue;   // чтобы не добавлять кавычки в значение
                }
                if (!openQuotes)            // если кавычки не открывались
                {
                    if (item == ' ' || item == '"' || item == '\'' || item== '\n' || item == '\t')         // и встретили пробел, то следующим будет символ нового аттрибута
                    {
                        if (prevChar == ' ')
                        {
                            prevChar = item;
                            continue;
                        }
                        /*
                        attValue = currentStr;
                        currentStr = "";
                        if (attName=="")
                            Attributes.Add(attValue, attName);
                        else
                            Attributes.Add(attName, attValue);
                        attName = "";
                        attValue = "";
                        */
                        prevChar = item;
                        continue;
                    }
                    else if (item == '=')
                    {

                        attName = currentStr;
                        currentStr = "";
                        prevChar = item;
                        continue;
                    }
                    if (prevChar == '=')
                    {
                        openQuotes = true;
                        noQuotesButValue = true;
                    }
                        
                }
                currentStr += item;
                prevChar = item;
            }

            if (attName != "" & !Attributes.ContainsKey(attName))
            {
                Attributes.Add(attName, currentStr);
                currentStr = "";
            }                

            // если после цикла остался символ "/", значит тег самозакрытый
            //IsSelfClosed = currentStr.Contains("/");

            return currentStr;
        }

        /// <summary>
        /// Функция рекурсивного поиска по ChildTags ТЕГа с нужным названием и/или аттрибутами
        /// </summary>
        /// <param name="nameTag"></param>
        /// <param name="tagAttribute"></param>
        /// <returns></returns>
        public List<Tag> LookForChildTag(string nameTag, bool LookInnerTags = false, KeyValuePair<string, string> tagAttribute = new KeyValuePair<string, string>())
        {
            List<Tag> result = new List<Tag>();            

            bool LookAttributes = false;
            if (tagAttribute.Key != null)
                if (tagAttribute.Key != "")
                    LookAttributes = true;

            foreach (Tag itemTag in this.ChildTags)
            {
                if (!itemTag.IsProto)
                {
                    if (itemTag.Name == nameTag)                                                    // если имя ТЕГа искомое
                    {
                        if (LookAttributes)                                                             // и поиск по аттрибутам активен{
                        {
                            if (itemTag.Attributes.ContainsKey(tagAttribute.Key))                           // и есть аттрибут с искомым именем
                                if (itemTag.Attributes[tagAttribute.Key] == tagAttribute.Value)                 // и искомым значением
                                    result.Add(itemTag);                                                            // тогда добавляем ТЕГ в результат
                        }
                        else                                                                            // если поиск по аттрибутам не активен,
                            result.Add(itemTag);                                                            // тогда просто добавляем ТЕГ в результат

                        if (LookInnerTags)                                                              // Если ищем вхождения и во внутренних ТЕГах,
                            result.AddRange(itemTag.LookForChildTag(nameTag, LookInnerTags, tagAttribute));              // то продолжаем поиск во внутренних ТЕГах
                    }
                    else                                                                            // если ТЕГ не найден
                        result.AddRange(itemTag.LookForChildTag(nameTag, LookInnerTags, tagAttribute));              // тогда продолжаем поиск во внутренних ТЕГах
                }
                else if (nameTag == null)
                    result.Add(itemTag);
            }

            return result;
        }

        /*
        public List<Tag> LookForChildTag(int targetLevel)
        {

            List<Tag> result = new List<Tag>();

            if (targetLevel < Level)
                return result;



            return result;
        }
        */

        public List<Tag> LookForParentTag(string nameTag, bool LookToRoot = false, KeyValuePair<string, string> tagAttribute = new KeyValuePair<string, string>())
        {
            List<Tag> result = new List<Tag>();

            bool LookAttributes = false;
            if (tagAttribute.Key != null)
                if (tagAttribute.Key != "")
                    LookAttributes = true;

            Tag workTag = (Tag)this.Parent;
            bool found = false;

            while (workTag != null)
            {
                if (workTag.Name == nameTag)
                {
                    if (LookAttributes)
                    {
                        if (workTag.Attributes.ContainsKey(tagAttribute.Key))                           // и есть аттрибут с искомым именем
                            if (workTag.Attributes[tagAttribute.Key] == tagAttribute.Value)                 // и искомым значением
                                found = true;
                    }
                    else
                        found = true;
                }

                if (found)
                {
                    found = false;
                    result.Add(workTag);
                    if (!LookToRoot)
                        break;
                }

                workTag = (Tag)workTag.Parent;
            }

            return result;
        }
        public Tag LookForParentTag(int targetLvl)
        {
            if (targetLvl < 0)
                return null;
            Tag workTag = this;
            while (workTag != null)
            {
                if (workTag.Level == targetLvl)
                    break;
                //return workTag;                
                else if (workTag.Level < targetLvl)
                    return null;
                workTag = (Tag)workTag.Parent;
            }
            return workTag;
        }
    }

    public static class HTMLParser
    {
        public static List<Tag> Parse(string inpString)
        {
            List<Tag> HTMLDoc = new List<Tag>();
            Tag myTag;
            string workStr = inpString.Trim().Replace("  ", " ");

            while (workStr.Length > 0)
            {
                myTag = new Tag(workStr, null);
                if (myTag.IsProto)
                    HTMLDoc.Add(myTag);
                else
                    HTMLDoc.Add(myTag);
                workStr = myTag.CutOffAfter;

                while (workStr.StartsWith("</"))
                {
                    workStr = workStr.Substring(workStr.IndexOf(">") + 1);
                    //continue;
                }
            }

            return HTMLDoc;
        }
    }
}

