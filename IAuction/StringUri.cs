using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    [Serializable]
    public struct AStringUri
    {
        public string ItemString;
        public string ItemUri;
        public override string ToString()
        {
            //return base.ToString();
            return ItemString;
        }
    }
}
