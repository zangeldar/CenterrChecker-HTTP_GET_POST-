using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace HTTP_GET_POST
{
    class Tag
    {
        private string tagName;
        private string tagValue;
        private List<tagAttribute> tagAttrList;
        private List<Tag> innerTags;

        public Tag(string tagNameContent, string tagValueContent)
        {
            fillName(tagNameContent);
            tagValue = tagValueContent;
            fillAttr(tagNameContent);
            fillInnerTags(tagValueContent);
        }

        private string sysGetNameOnly(string inputStr, bool lookSpace = true)
        {
            if (!inputStr.Contains('<'))
                return null;
            string result;
            int endTagName;            

            int startTag = inputStr.IndexOf('<');
            int endTag = inputStr.IndexOf('>',startTag+1);
            int endTagNameNextTag = inputStr.Substring(startTag + 1).IndexOf('<');
            int endTagNameSpace = inputStr.IndexOf(' ');
            endTagName = endTagNameSpace;
            if (endTagNameSpace < startTag)
                endTagName = endTagNameNextTag;

            int length = Math.Min(endTag, endTagName) - startTag - 1;

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

            int endOFTagName = tagNameContent.IndexOf('>');
            string onlyTagName = tagNameContent.Substring(1, endOFTagName - 1);

            int c;            
            string attrName = "";
            string attrValue = "";
            //foreach (string item in tagNameContent.Split(' '))
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
        }

        private void fillInnerTags(string tagValueContent)
        {            
            innerTags = new List<Tag>();
            string nextTag = sysGetNameOnly(tagValueContent);

            if (!tagValueContent.Contains('<') | nextTag == null)
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
            }

            // здесь надо обрабатывать ситуацию, когда внутри родительского тега, могут открываться и закрываться несколько тегов.
            // без доработки, обрабатывается только первый тег
            //innerTags = Parser.getTags(tagValueContent, nextTag);
        }

        public string Name { get { return tagName; } }
        public string Value { get { return tagValue; } }
        public List<tagAttribute> Attributes { get { return tagAttrList; } }
        public List<Tag> InnerTags { get { return innerTags; } }
        
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

    class tagAttribute
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

    class myHTMLParser
    {
        //const string table_pattern = "<table.*?>(.*?)</table>";
        const string table_pattern = "<table.*?>(?<tegData>.+?)</table>";

        const string tr_pattern = "<tr>(.*?)</tr>";

        const string td_pattern = "<td.*?>(.*?)</td>";

        const string a_pattern = "<a href=\"(.*?)\"></a>";

        const string b_pattern = "<b>(.*?)</b>";

        public List<Tag> getTags(string inpHTML, string tag = "a")
        {
            //inpHTML = inpHTML.Replace('\r', ' ');
            //inpHTML = inpHTML.Replace('\t', ' ');
            //inpHTML = inpHTML.Replace('\n', ' ');

            List<Tag> result = new List<Tag>();
            string pattern = string.Format(@"\<{0}.*?\>(?<tegData>.+?)?\<\/{0}\>", tag.Trim());
            // \<{0}.*?\> - открывающий тег
            // \<\/{0}\> - закрывающий тег            
            // (?<tegData>.+?) - содержимое тега, записываем в группу tegData            

            //Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(inpHTML);
            
            foreach (Match matche in matches)
            {
                result.Add(new Tag(matche.Value, matche.Groups["tegData"].Value));
            }
            return result;
        }

        public DataTable getTable(Tag inpTag)
        {
            if (inpTag.Name != "table")
                return null;

            DataTable result = new DataTable();

            

            foreach (Tag rowTag in inpTag.InnerTags)
            {
                result.Rows.Add(getOneRow(rowTag).ToArray());                
            }
            
            return result;
        }

        private List<List<StringUri>> getProtoTable(Tag inpTag)
        {
            if (inpTag.Name != "table")
                return null;

            List<List<StringUri>> myProtoTable = new List<List<StringUri>>();

            foreach (Tag rowTag in inpTag.InnerTags)              
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

        private string NormalizeString(string inpStr)
        {
            if (inpStr == null)
                return "";

            string result;

            result = inpStr.Replace("\n", "");
            result = result.Replace("\t", "");
            result = result.Replace("\r", "");
            result = result.Replace("&quot;", "\"");
            result = result.Replace("&#160;", " ");

            return result;
        }


        public List<List<StringUri>> getOutTable(Tag inpTag)
        {
            return NormalizeProtoTable(getProtoTable(inpTag));
        }



        private StringUri EachRowRec(Tag inpTag)
        {
            StringUri result = new StringUri { ItemString = "" };
            if (!inpTag.HasInnerTags)
            {
                result.ItemString = inpTag.Value;
                return result;
            }

            List<StringUri> outList = new List<StringUri>();
            foreach (Tag inTag in inpTag.InnerTags)
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

        private string GetUriFromHref(Tag inTag)
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

        private List<StringUri> getOneRow(Tag inpTag)
        {
            //DataRow result = null;// = new DataRow();
            List<StringUri> outList = new List<StringUri>();
            foreach (Tag inTag in inpTag.InnerTags)
            {
                outList.Add(EachRowRec(inTag));
            }
            //result.ItemArray = outList.ToArray();
            //return result;
            return outList;
        }

        private DataColumn getOneCol(Tag inpTag)
        {
            DataColumn result = new DataColumn();

            return result;
        }
    }
}
