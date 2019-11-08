using System;
using System.Collections.Generic;
using System.Text;

namespace MyHTMLParser
{
    [Serializable]
    public struct StringUri
    {
        public string ItemString;
        public string ItemUri;
        public override string ToString()
        {
            //return base.ToString();
            return ItemString;
        }

        public override bool Equals(object obj)
        {
            bool test = base.Equals(obj);
            if (!(obj is StringUri))
                return false;
            StringUri curObj = (StringUri)obj;

            if (this.ItemString == curObj.ItemString &
                this.ItemUri == curObj.ItemUri)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
