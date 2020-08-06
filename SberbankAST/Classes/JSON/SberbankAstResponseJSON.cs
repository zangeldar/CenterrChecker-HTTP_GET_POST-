using System;
using System.Collections.Generic;
using System.Text;

namespace SberbankAST
{
    [Serializable]
    public class JsonResponse
    {
        public string result { get; set; }
        public string data { get; set; }
    }

    [Serializable]
    public class JsonRoot
    {
        public string tableXml { get; set; }
        public string statisticXml { get; set; }
        public string pagerTotal { get; set; }
        public string data { get; set; }
    }

}
