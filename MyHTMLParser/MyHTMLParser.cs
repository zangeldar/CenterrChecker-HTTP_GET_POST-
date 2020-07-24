using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace MyHTMLParser
{
    public class _Tag
    {
        private string tagName;
        private string tagValue;
        private List<tagAttribute> tagAttrList;
        private List<_Tag> innerTags;

        /*
        public _Tag(string inpString)
        {
            tagNameContent = myHTMLParser.NormalizeString(tagNameContent);
            tagValueContent = myHTMLParser.NormalizeString(tagValueContent);
            fillName(tagNameContent);
            tagValue = tagValueContent;
            fillAttr(tagNameContent);
            NewFillInnerTags(tagValueContent);
        }
        */




        public _Tag(string tagNameContent, string tagValueContent)
        {
            tagNameContent = myHTMLParser.NormalizeString(tagNameContent);
            tagValueContent = myHTMLParser.NormalizeString(tagValueContent);
            fillName(tagNameContent);
            tagValue = tagValueContent;
            fillAttr(tagNameContent);
            fillInnerTags(tagValueContent);
        }
        
        /// <summary>
        /// Возвращает только имя тэга
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="lookSpace"></param>
        /// <returns></returns>
        private string sysGetNameOnly(string inputStr, bool lookSpace = true)
        {
            if (!inputStr.Contains("<"))
                return null;
            
            string result;
            int endTagName;

            int startTag = inputStr.IndexOf('<');                               // открытие тэга
            int endTag = inputStr.IndexOf('>', startTag + 1);                   // закрытие тэга
            int endTagNameNextTag = inputStr.IndexOf('<', startTag + 1);        // открытие след.тэга
            int endTagNameSpace = inputStr.IndexOf(' ', startTag + 1);          // пробе            
            /*  // о чем я думал, когда писал это? 
            endTagName = endTagNameSpace;
            if (endTagNameSpace < startTag)            
                endTagName = endTagNameNextTag;
                */
            // Проверка, что встретится раньше: пробел, или закрытие тэга?,
            endTagName = endTag;
            if (endTagNameSpace > 0)
                if (endTagNameSpace < endTag)
                    endTagName = endTagNameSpace;

            int length;
            if (endTag < 0)
                length = endTagName;
            else if (endTagName < 0)
                length = endTag;
            else
                length = Math.Min(endTag, endTagName) - startTag - 1;

            result = inputStr.Substring(startTag + 1, length);

            return result;
        }
        private void fillName(string tagNameContent)
        {
            tagName = sysGetNameOnly(tagNameContent);
        }

        private void fillAttr(string tagNameContent)
        {
            tagAttrList = new List<tagAttribute>();

            int endOFTagNameContent = tagNameContent.IndexOf('>');
            string onlyTagNameContent = tagNameContent.Substring(1, endOFTagNameContent - 1);

            //string tagName = "";
            string attName = "";
            string attValue = "";

            bool openQuotes = false;
            //bool nextAtt = true;   // признак того, что сейчас символ аттрибута
            //bool nextValue = false; // признак того, что сейчас символ значения аттрибута

            int tagNameEnd = onlyTagNameContent.IndexOf(" ");
            if (tagNameEnd < 0)
            {
                //tagName = onlyTagNameContent;
            }
            else
            {
                //tagName = onlyTagNameContent.Substring(0, tagNameEnd);

                string currentStr = "";
                foreach (char item in onlyTagNameContent.Substring(tagNameEnd + 1))
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
                            //nextValue = false;
                            //nextAtt = true;

                            attValue = currentStr;
                            currentStr = "";

                            tagAttrList.Add(new tagAttribute(attName, attValue));

                            continue;
                        }
                        else if (item == '=')
                        {
                            //nextAtt = false;
                            //nextValue = true;

                            attName = currentStr;
                            currentStr = "";

                            continue;
                        }
                    }
                    currentStr += item;
                }
                if (currentStr != "")           // если последнее значение аттрибута не завершилось кавычкой (опечатка), тогда добавить все, что получили в список
                    tagAttrList.Add(new tagAttribute(attName, currentStr));
            }

            /*
            int c;
            string attrName = "";
            string attrValue = "";
            //foreach (string item in tagNameContent.Split(' '))

            ////////////
            /// ПЕРЕРАБОТАТЬ:
            /// ////////////
            /// Работает некорректно для случая, когд
            /// onlyTagName = "ul class="menu__list menu__list--table width-full""
            /// потому что пробел содержится между кавычек
            foreach (string item in onlyTagName.Split(' '))
            {
                c = 0;
                foreach (string itemAtt in item.Split('='))
                {
                    switch (c)
                    {
                        case 0:
                            attrName = itemAtt;
                            break;
                        case 1:
                            attrValue = itemAtt;//.Replace('\"','');
                            break;
                        default:
                            break;
                    }
                    c++;
                }
                if ((c > 1) & (attrName.Length > 0) & (attrValue.Length > 0))
                    tagAttrList.Add(new tagAttribute(attrName, attrValue));
            }
            */
        }

        private void fillInnerTags(string tagValueContent)
        {
            innerTags = new List<_Tag>();
            string nextTag = sysGetNameOnly(tagValueContent);

            if (!tagValueContent.Contains("<") | nextTag == null)
                return;

            int startPosition = 0;
            int endPosition = 0;

            string workString;
            string endOfTag;
            /*  // работаем с полной строкой по частям
            while (endPosition < tagValueContent.Length)            
            {// это надо в цикл
                startPosition = endPosition + startPosition;
                workString = tagValueContent.Substring(startPosition);

                nextTag = sysGetNameOnly(workString);
                endOfTag = String.Format("</{0}>", nextTag);
                if (!workString.Contains(endOfTag))
                    endOfTag = "/>";

                int indOfEndTag = workString.IndexOf(endOfTag);
                endPosition = indOfEndTag + endOfTag.Length + startPosition;
                //workString = workString.Remove(endPosition);
                string resultStr = tagValueContent.Substring(startPosition, endPosition-startPosition);

                myHTMLParser Parser = new myHTMLParser();
                innerTags.AddRange(Parser.getTags(resultStr, nextTag));               
            }
            */
            string curContent = tagValueContent;
            while (curContent.Length > 0)
            {// это надо в цикл
                // оригинал до 2020.07.21
             
             startPosition = 0;

             workString = curContent;

             nextTag = sysGetNameOnly(workString);
             if (nextTag == null | nextTag == "")
                 break;
             endOfTag = String.Format("</{0}>", nextTag);
             if (!workString.Contains(endOfTag))
                 endOfTag = "/>";

             int indOfEndTag = workString.IndexOf(endOfTag);
             endPosition = indOfEndTag + endOfTag.Length + startPosition;
             //workString = workString.Remove(endPosition);
             string resultStr = workString.Substring(startPosition, endPosition - startPosition);

             myHTMLParser Parser = new myHTMLParser();
             innerTags.AddRange(Parser.getTags(resultStr, nextTag));

             curContent = curContent.Remove(startPosition, resultStr.Length);
             

            //  20200721
                // если вдруг я еще раз захочу переписать парсер, то надо начинать с RegExp, потому что иногда он дает неправильный результат из-за вложенных тегов. Т.е. Возвращает строку от первого открытия тега до ПЕРВОГО ЗАКРЫТИЯ ТЕГА, даже если он внутренний.
                /*
                startPosition = 0;

                workString = curContent;

                nextTag = sysGetNameOnly(workString);
                if (nextTag == null | nextTag == "")
                    break;

                // определяем закрывающий тэг. Перед этим нужна проверка на открывающийся такой же тег
                endOfTag = String.Format("</{0}>", nextTag);
                // если не нашли закрывающий тэг, то этот тэг самозакрывающийся
                if (!workString.Contains(endOfTag))
                {
                    endOfTag = "/>";
                    // если и этого не нашли, то тэг самодостаточный (не требующий закрытия, типа <br>)
                    if (!workString.Contains(endOfTag))
                        endOfTag = ">";
                }                

                // ищем Индекс следующего такого же тега
                int nextSameTag = 0; // = workString.IndexOf("<" + nextTag, startPosition + 1);
                // ищем Закрывающий индекс тэга
                int indOfEndTag = workString.IndexOf(endOfTag);
                                
                // если след индекс раньше закрывающего, то ищем следующий закырвающий. и так, пока не закончим (нужен цикл)
                while (nextSameTag < indOfEndTag)
                {
                    //int nextSameTag1 = workString.IndexOf("<" + nextTag + ">", workString.IndexOf("<") + 1);
                    //int nextSameTag2 = workString.IndexOf("<" + nextTag + " ", workString.IndexOf("<") + 1);

                    int nextSameTag1 = workString.IndexOf("<" + nextTag + ">", nextSameTag + 1);
                    int nextSameTag2 = workString.IndexOf("<" + nextTag + " ", nextSameTag + 1);

                    if (nextSameTag1 < 0)
                        nextSameTag1 = nextSameTag2;
                    if (nextSameTag2 < 0)
                        nextSameTag2 = nextSameTag1;
                    nextSameTag = Math.Min(nextSameTag1, nextSameTag2);
                    if (nextSameTag < 0)
                        break;

                    indOfEndTag = workString.IndexOf(endOfTag, indOfEndTag+1);
                }

                endPosition = indOfEndTag + endOfTag.Length + startPosition;
                //workString = workString.Remove(endPosition);
                string resultStr = workString.Substring(startPosition, endPosition - startPosition);

                myHTMLParser Parser = new myHTMLParser();
                innerTags.AddRange(Parser.getTags(resultStr, nextTag));

                curContent = curContent.Remove(startPosition, resultStr.Length);
                */
            }

            // здесь надо обрабатывать ситуацию, когда внутри родительского тега, могут открываться и закрываться несколько тегов.
            // без доработки, обрабатывается только первый тег
            //innerTags = Parser.getTags(tagValueContent, nextTag);
        }

        private void NewFillInnerTags(string inpStr)
        {

        }

        public string Name { get { return tagName; } }
        public string Value { get { return tagValue; } }
        public List<tagAttribute> Attributes { get { return tagAttrList; } }
        public List<_Tag> InnerTags { get { return innerTags; } }

        public bool HasInnerTags { get { return (innerTags.Count > 0); } }

        public override string ToString()
        {
            //return base.ToString();
            string result;

            if (InnerTags.Count > 0)
                result = String.Format("{0} [ {1} tags ]", tagName, innerTags.Count);
            else
                result = String.Format("{0} : {1}", tagName, tagValue);

            return result;
        }
    }

    public class tagAttribute
    {
        private string tagAttrName;
        private string tagAttrValue;
        public tagAttribute(string name, string value)
        {
            tagAttrName = name;
            tagAttrValue = value;
        }
        public string Name { get { return tagAttrName; } }
        public string Value { get { return tagAttrValue; } }

        public override string ToString()
        {
            //return base.ToString();

            return String.Format("{0} : {1}", tagAttrName, tagAttrValue);
        }
    }

    public class myHTMLParser
    {
        //const string table_pattern = "<table.*?>(.*?)</table>";
        const string table_pattern = "<table.*?>(?<tegData>.+?)</table>";

        const string tr_pattern = "<tr>(.*?)</tr>";

        const string td_pattern = "<td.*?>(.*?)</td>";

        const string a_pattern = "<a href=\"(.*?)\"></a>";

        const string b_pattern = "<b>(.*?)</b>";
        
        

        public List<_Tag> getTags(string inpHTML, string tag = "a")
        {
            //inpHTML = inpHTML.Replace('\r', ' ');
            //inpHTML = inpHTML.Replace('\t', ' ');
            //inpHTML = inpHTML.Replace('\n', ' ');

            List<_Tag> result = new List<_Tag>();
            string pattern = string.Format(@"\<{0}.*?\>(?<tegData>.+?)?\<\/{0}\>", tag.Trim());
            //string pattern = string.Format(@"\<{0}(.*?)\>(?<tegData>.+?)?\<\/{0}\>", tag.Trim());
            // \<{0}.*?\> - открывающий тег
            // \<\/{0}\> - закрывающий тег            
            // (?<tegData>.+?) - содержимое тега, записываем в группу tegData            

            //Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            //Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(inpHTML);

            foreach (Match matche in matches)
            {
                result.Add(new _Tag(matche.Value, matche.Groups["tegData"].Value));
            }
            return result;
        }

        public DataTable getTable(_Tag inpTag)
        {
            if (inpTag.Name != "table")
                return null;

            DataTable result = new DataTable();



            foreach (_Tag rowTag in inpTag.InnerTags)
            {
                result.Rows.Add(getOneRow(rowTag).ToArray());
            }

            return result;
        }

        private List<List<StringUri>> getProtoTable(_Tag inpTag)
        {
            if (inpTag.Name != "table")
                return null;

            List<List<StringUri>> myProtoTable = new List<List<StringUri>>();

            foreach (_Tag rowTag in inpTag.InnerTags)
                myProtoTable.Add(getOneRow(rowTag));

            return myProtoTable;
        }

        private List<List<StringUri>> NormalizeProtoTable(List<List<StringUri>> inpProtoTable)
        {
            List<List<StringUri>> resList = new List<List<StringUri>>();
            String myStr;
            foreach (List<StringUri> itemList in inpProtoTable)
            {
                List<StringUri> newList = new List<StringUri>();
                foreach (StringUri item in itemList)
                {
                    /*  // Refactor to NormalizeString()
                    myStr = item.ItemString.Replace("\n","");
                    myStr = myStr.Replace("\t", "");
                    myStr = myStr.Replace("\r", "");
                    myStr = myStr.Replace("&quot;", "\"");
                    myStr = myStr.Replace("&#160;", " ");
                    */

                    newList.Add(new StringUri { ItemString = NormalizeString(item.ItemString), ItemUri = NormalizeString(item.ItemUri) });
                }
                resList.Add(newList);
            }
            //resList = inpProtoTable;  //  заглушка
            return resList;
        }

        static public string NormalizeString(string inpStr)
        {
            if (inpStr == null)
                return "";

            string result;

            result = inpStr.Replace("\n", " ");
            result = result.Replace("\t", " ");
            result = result.Replace("\r", " ");            
            result = result.Replace("&quot;", "\"");
            result = result.Replace("&#160;", " ");
            result = result.Replace("&#8470;", "№");

            result = result.Replace("  ", " ");

            return result;
        }


        public List<List<StringUri>> getOutTable(_Tag inpTag)
        {
            return NormalizeProtoTable(getProtoTable(inpTag));
        }



        private StringUri EachRowRec(_Tag inpTag)
        {
            StringUri result = new StringUri { ItemString = "" };
            if (!inpTag.HasInnerTags)
            {
                result.ItemString = inpTag.Value;
                return result;
            }

            List<StringUri> outList = new List<StringUri>();
            foreach (_Tag inTag in inpTag.InnerTags)
            {
                if (inTag.HasInnerTags)
                    outList.Add(EachRowRec(inTag));
                //continue;
                outList.Add(new StringUri { ItemString = inTag.Value, ItemUri = GetUriFromHref(inTag) });
            }

            foreach (StringUri item in outList)
            {
                if (item.ItemString == "")
                    continue;
                result.ItemString += (item.ItemString);
                result.ItemUri += (item.ItemUri);
            }

            return result;
        }

        private string GetUriFromHref(_Tag inTag)
        {
            string result = "";

            foreach (tagAttribute itemAtt in inTag.Attributes)
            {
                if (itemAtt.Name == "href")
                {
                    result = itemAtt.Value;
                    break;
                }
            }

            return result;
        }

        private List<StringUri> getOneRow(_Tag inpTag)
        {
            //DataRow result = null;// = new DataRow();
            List<StringUri> outList = new List<StringUri>();
            foreach (_Tag inTag in inpTag.InnerTags)
            {
                outList.Add(EachRowRec(inTag));
            }
            //result.ItemArray = outList.ToArray();
            //return result;
            return outList;
        }

        private DataColumn getOneCol(_Tag inpTag)
        {
            DataColumn result = new DataColumn();

            return result;
        }



        static List<List<StringUri>> GetResultTableAsList(List<List<StringUri>> inpList)
        {
            List<List<StringUri>> resList = new List<List<StringUri>>();

            // 1. Calculate MAX columns count
            int colCount = 0;
            foreach (List<StringUri> itemList in inpList)
                colCount = Math.Max(colCount, itemList.Count);

            // 2. Fill result rows
            foreach (List<StringUri> itemListRows in inpList)
            {
                if (itemListRows.Count != colCount)     // Skip all rows that have not another count of columns instead MAX columns count
                    continue;
                resList.Add(itemListRows);
            }

            return resList;
        }

        static public DataTable GetTableAsDT(List<List<StringUri>> inpTable)
        {
            DataTable resDT = new DataTable();

            foreach (StringUri item in inpTable[0])
                resDT.Columns.Add(item.ItemString);

            for (int i = 1; i < inpTable.Count - 1; i++)
                resDT.Rows.Add(inpTable[i]);

            return resDT;
        }

        static public string GetNewRowsString(List<List<StringUri>> inpLT, string checkDate)
        {
            string outRows = "";
            for (int i = 1; i < inpLT.Count - 1; i++)   // первая строка - заголовки колонок
            {
                if (!HaveNewRecords(inpLT[i], checkDate))
                    break;
                foreach (StringUri item in inpLT[i])
                    outRows += "\t|\t" + item.ItemString;
                outRows += "\n";
            }
            //if (outRows.Length == 0)
            //    return null;
            return outRows;
        }
        static public bool HaveNewRecords(List<StringUri> checkRow, string lastKnownDate)
        {
            string checkDate = checkRow[6].ItemString;
            if (checkDate != lastKnownDate)
                return true;
            return false;
        }

        static void OutWholeTree(_Tag inpTag)
        {
            Console.WriteLine(inpTag.ToString());
            if (inpTag.HasInnerTags)
            {
                Console.Write("\n\t");
                foreach (_Tag innTag in inpTag.InnerTags)
                    OutWholeTree(innTag);
            }
        }
    }

}
