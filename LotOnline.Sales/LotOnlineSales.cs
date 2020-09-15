using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;

namespace LotOnline.Sales
{
    [Serializable]
    public class LotOnlineSales : ATorg
    {
        //public string baseUrl { get; private set; }
        public LotOnlineSales(Tag inpTag, IRequest myReq):base(myReq)
        {            
            this.baseUrl = this.baseUrl.Replace("e-auction", "");
            this.baseUrl += "e-auction/";
            this.baseUrl = this.baseUrl.Replace("//", "/");

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("id", "new-field-title")))
            {
                foreach (Tag inItem in item.LookForChildTag("a", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "filed filed-title")))
                {
                    if (inItem.Attributes.ContainsKey("href"))
                        LotNameUrl = inItem.Attributes["href"];
                    string val = "";
                    foreach (Tag inInItem in item.LookForChildTag(null))
                        if (!inInItem.IsComment)
                            val += inInItem.Value;
                    LotNameStr = val;
                    if (LotNameUrl != null & LotNameUrl != "" & LotNameStr != null & LotNameStr != "")                        
                        break;
                }
            }
            string tmpStr = "";
            if (LotNameUrl.Contains(";jsessionid="))
                tmpStr = LotNameUrl.Substring(0, LotNameUrl.IndexOf(";jsessionid="));
            if (LotNameUrl.Contains("?parm"))
                LotNameUrl = tmpStr + LotNameUrl.Substring(LotNameUrl.IndexOf("?parm"));
            LotNameUrl = this.baseUrl + LotNameUrl;

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("id", "new-field-lot")))
            {
                string val = "";
                foreach (Tag inItem in item.LookForChildTag(null))
                    if (!inItem.IsComment)
                        val += inItem.Value;
                LotNumberStr = val;
                if (LotNumberStr != null & LotNumberStr != "")
                    break;
            }

            foreach (Tag item in inpTag.LookForChildTag("span", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "price")))
            {
                string val = "";
                foreach (Tag inItem in item.LookForChildTag(null))
                    if (!inItem.IsComment)
                        val += inItem.Value;
                PriceStart = val;
                if (PriceStart != null & PriceStart != "")
                    break;
            }

            TableRowMeans = new string[]
            {
                LotNumberStr,
                LotNameStr,
                PriceStart
            };
            TableRowUrls = new string[TableRowMeans.Length];
            TableRowUrls = new string[]
            {
                "",
                LotNameUrl,
                ""
            };
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is LotOnlineSales))
                return false;
            return base.Equals(obj);
        }
        /*
        public override string ToString(bool html)
        {
            throw new NotImplementedException();
        }                

        public override int GetHashCode()
        {
            var hashCode = 71509727;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(baseUrl);
            return hashCode;
        }
        */        
    }
}
