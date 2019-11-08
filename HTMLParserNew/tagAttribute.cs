using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParserNew
{
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
}
