﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParserNew

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
    }
}
