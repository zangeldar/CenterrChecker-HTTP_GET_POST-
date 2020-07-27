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
    public class ProtoTag
    {
        public override string ToString()
        {
            //return base.ToString();
            return Value;
        }
        virtual public bool IsProto { get; protected set; }
        public string Value { get; protected set; }
        public ProtoTag Parent { get; private set; }
        public ProtoTag(string inpString)
        {
            this.IsProto = true;
            this.Value = inpString;            
        }
        public ProtoTag(string inpString, ProtoTag parent)
        {
            this.IsProto = true;
            this.Value = inpString;
            this.Parent = parent;
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
            
            string result = String.Format("Name: {0} | ChildCount: {1}", Name, ChildTags.Count);
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
        public string Name { get; private set; }
        public string SourceString { get; private set; }
        public Dictionary<string, string> Attributes { get; private set; }
        public List<ProtoTag> ChildTags { get; private set; }        
        public string CutOffBefore { get; private set; }
        public string CutOffAfter { get; private set; }
        public int ErrorCount { get; private set; }

        public Tag(string inpString) : base(inpString)
        {
            MakeTag(inpString);
        }

        public Tag(string inpString, ProtoTag parent) : base(inpString, parent)
        {
            MakeTag(inpString);
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
            CutOffAfter = FillTagHeader(inpString);

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
                CutOffAfter = CutOffAfter.Substring(CutOffAfter.IndexOf(">") + 1);
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
                // Если ТЕГ был ПРОТО-ТЕГ, 
                if (inTag.IsProto)
                {
                    // то добавляем его как ПРОТО-ТЕГ
                    //ChildTags.Add((ProtoTag)inTag);
                    ChildTags.Add(inTag as ProtoTag);
                }
                else
                    ChildTags.Add(inTag);

                // и проверяем, не закрылся ли после него родительский ТЕГ
                if (cutOffAfter.IndexOf("</" + Name) == 0)
                {
                    CutOffAfter = cutOffAfter.Substring(cutOffAfter.IndexOf(">") + 1);
                    break;
                }

                if (watchDog == cutOffAfter.Length)
                    throw new Exception("Warning! Infinity cycle detected! String value: " + cutOffAfter);
                // убираем оборванные закрывающие ТЕГи на случай созданиа ТЕГа из части HTML
                while (cutOffAfter.StartsWith("</"))
                {
                    cutOffAfter = cutOffAfter.Substring(cutOffAfter.IndexOf(">") + 1);
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
            ChildTags = new List<ProtoTag>();
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
            // находим конец тега            
            // и проверяем, есть ли он
            endTagInd = inpString.IndexOf(">");            
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

            // если до открытия тега были данные, то сохраняем их как CutOffbefore
            if (startTagInd > 0)
            {
                //CutOffBefore = inpString.Substring(0, startTagInd - 0);                   //  то сохраняем их как CutOffbefore
                //ChildTags.Add(new ProtoTag(inpString.Substring(0, startTagInd - 0)));     // то сохраняем их как протоТег во внутренние теги.
                Value = inpString.Substring(0, startTagInd - 0);                            // то это - протоТЕГ
                IsProto = true;
                return inpString.Substring(startTagInd);
            }


            // ищем конец имени тега
            // и заполняем ИмяТега
            int endTagName;
            char[] separator = new char[]{ ' ', '/', '>' };
            endTagName = inpString.IndexOfAny(separator, startTagInd);            
            Name = inpString.Substring(startTagInd+1, endTagName - startTagInd - 1);

            // если тег - meta, тогда метим его самозакрытым и заполняем аттрибуты
            IsSelfClosed = (Name == "meta" || IsSelfClosed);
            IsSelfClosed = (Name == "link" || IsSelfClosed);

            IsSelfClosed = (Name == "br" || IsSelfClosed);
            if (Name.StartsWith("!"))
            {
                IsSelfClosed = true;
                if (Name.StartsWith("!--"))
                {
                    Name = inpString.Substring(0+1, endTagInd-0-1);
                    return inpString.Substring(endTagInd+1);
                }
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
            return inpString.Substring(endTagInd+1);
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
            string currentStr = "";
            string attName = "";
            string attValue = "";
            foreach (char item in inpString)
            {
                if (item == '"' || item == '\'')
                {
                    openQuotes = !openQuotes;
                    continue;   // чтобы не добавлять кавычки в значение
                }
                if (!openQuotes)            // если кавычки не открывались
                {
                    if (item == ' ' || item == '"' || item == '\'')         // и встретили пробел, то следующим будет символ нового аттрибута
                    {
                        attValue = currentStr;
                        currentStr = "";
                        Attributes.Add(attName, attValue);
                        attName = "";
                        attValue = "";
                        continue;
                    }
                    else if (item == '=')
                    {
                        attName = currentStr;
                        currentStr = "";
                        continue;
                    }
                }
                currentStr += item;
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
    }

    public static class HTMLParser
    {
        public static List<ProtoTag> Parse(string inpString)
        {
            List<ProtoTag> HTMLDoc = new List<ProtoTag>();
            Tag myTag;
            string workStr = inpString;

            while (workStr.Length > 0)
            {
                myTag = new Tag(workStr, null);
                if (myTag.IsProto)
                    HTMLDoc.Add((ProtoTag)myTag);
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
