using System;
using System.Collections.Generic;

namespace HTMLParserNew
{
    public class Tag
    {
        private string tagName;
        private string tagValue;
        private List<tagAttribute> tagAttrList;
        private List<Tag> innerTags;
        private string rawHtml;

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

        /*
        public Tag(string tagNameContent, string tagValueContent)
        {
            fillName(tagNameContent);
            tagValue = tagValueContent;
            fillAttr(tagNameContent);
            fillInnerTags(tagValueContent);
        }
        */
        
        public Tag (string innerHTML)
        {
            rawHtml = innerHTML;
            fillTag(innerHTML);
        }

        private void fillTag(string innerHtml)
        {
            bool hasAttributes = false;
            bool hasInnerTags = false;
            ////
            /// Выедляем имя первого тега из сырого HTML

            int startTag = innerHtml.IndexOf('<');
            int endOfTagName_1 = innerHtml.Substring(startTag).IndexOf('>');
            int endOfTagName_2 = innerHtml.Substring(startTag).IndexOf(' ');
            int endOfTagName_3 = innerHtml.Substring(startTag).IndexOf("/>");

            hasInnerTags = (endOfTagName_1 - endOfTagName_3 != 1);            

            int endOfTagName;
            if (endOfTagName_2 > 0)
            {
                hasAttributes = (endOfTagName_1 - endOfTagName_2 > 2);
                endOfTagName = Math.Min(endOfTagName_1, endOfTagName_2);
            }
            else
            {
                hasAttributes = false;
                endOfTagName = endOfTagName_1;
            }            
            
            int lengthOfTagName = endOfTagName - startTag;

            string tagName = innerHtml.Substring(startTag+1, lengthOfTagName+startTag-1);
            //проверить на br и подобные самотеги
            int endOfTag = endOfTagName_1;
            ////

            ////
            ///
                        
        }
    }
}
