using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace CenterRu
{
    [Serializable]
    public class Centerr : ATorg
    {
        public Centerr(Tag inpTag)
        {
            
            if (inpTag.ChildTags.Count != 11)
                return;
            // class = "purchase-type-public-offer"
            TorgNumberStr = inpTag.ChildTags[0].ChildTags[0].ChildTags[0].Value.Trim();
            TorgNumberUrl = inpTag.ChildTags[0].ChildTags[0].Attributes["href"].Trim();
            // class = "tip-purchase"
            TorgNameStr = inpTag.ChildTags[1].ChildTags[0].ChildTags[0].Value.Trim();
            TorgNameUrl = inpTag.ChildTags[1].ChildTags[0].Attributes["href"].Trim();
            // 
            LotNumberStr = inpTag.ChildTags[2].ChildTags[0].ChildTags[0].Value.Trim();
            LotNumberUrl = inpTag.ChildTags[2].ChildTags[0].Attributes["href"].Trim();
            // class = "tip-lot"
            LotNameStr = inpTag.ChildTags[3].ChildTags[0].ChildTags[0].Value.Trim();
            LotNameUrl = inpTag.ChildTags[3].ChildTags[0].Attributes["href"].Trim();
            // 
            PriceStart = inpTag.ChildTags[4].ChildTags[0].Value.Trim();
            // class = "tip-party"
            OrganizerStr = inpTag.ChildTags[5].ChildTags[0].ChildTags[0].Value.Trim();
            OrganizerUrl = inpTag.ChildTags[5].ChildTags[0].Attributes["href"].Trim();
            //
            DateAcceptFinish = inpTag.ChildTags[6].ChildTags[0].Value.Trim();
            DateAuctionStart = inpTag.ChildTags[7].ChildTags[0].Value.Trim();
            //
            Status = inpTag.ChildTags[8].ChildTags[0].Value.Trim();
            // class = "tip-purchase"
            TorgType = inpTag.ChildTags[10].ChildTags[0].Value.Trim();
            //
            try
            {
                WinnerStr = inpTag.ChildTags[9].ChildTags[0].ChildTags[0].Value.Trim();
                WinnerUrl = inpTag.ChildTags[9].ChildTags[0].Attributes["href"].Trim();
            }
            catch (Exception e)
            {

                //throw;
            }
        }
        /*
        public Centerr(List<StringUri> itemsList)
        {
            if (itemsList.Count != 11)
                return;

            TorgNumberStr = itemsList[0].ItemString;
            TorgNumberUrl = itemsList[0].ItemUri;
            TorgNameStr = itemsList[1].ItemString;
            TorgNameUrl = itemsList[1].ItemUri;
            LotNumberStr = itemsList[2].ItemString;
            LotNumberUrl = itemsList[2].ItemUri;
            LotNameStr = itemsList[3].ItemString;
            LotNameUrl = itemsList[3].ItemUri;
            PriceStart = itemsList[4].ItemString;
            OrganizerStr = itemsList[5].ItemString;
            OrganizerUrl = itemsList[5].ItemUri;
            DateAcceptFinish = itemsList[6].ItemString;
            DateAuctionStart = itemsList[7].ItemString; 
            Status = itemsList[8].ItemString; 
            WinnerStr = itemsList[9].ItemString;
            WinnerUrl = itemsList[9].ItemString;
            TorgType = itemsList[10].ItemString;
        }
        */
        private string baseUrl = "http://bankrupt.centerr.ru";
        override public string internalID { get; protected set; }
        //public StringUri TorgNumber { get; private set; }
        public string TorgNumberStr { get; private set; }
        public string TorgNumberUrl { get; private set; }
        //public StringUri TorgName { get; private set; }
        public string TorgNameStr { get; private set; }
        public string TorgNameUrl { get; private set; }
        //public StringUri LotNumber { get; private set; }
        override public string LotNumberStr { get; protected set; }
        public string LotNumberUrl { get; private set; }
        //public StringUri LotName { get; private set; }
        override public string LotNameStr { get; protected set; }
        override public string LotNameUrl { get; protected set; }
        public string OrganizerStr { get; private set; }
        public string OrganizerUrl { get; private set; }
        override public string PriceStart { get; protected set; }
        public string DateAcceptFinish { get; private set; }
        public string DateAuctionStart { get; private set; }
        public string Status { get; private set; }
        //public StringUri Winner { get; private set; }
        public string WinnerStr { get; private set; }
        public string WinnerUrl { get; private set; }
        public string TorgType { get; private set; } 

        public override string ToString(bool html)
        {            
            string result = "";
            string formatStr = @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + ";" +
                    @"{12}" + ";" +
                    @"{13}" + ";" +
                    @"{15}" + ";" +
                    @"{16}" + Environment.NewLine +
                    // строка таблицы с ссылками
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"{4}" + ";" +
                    @"{6}" + ";" +
                    @"" + ";" +
                    @"{9}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{14}" + ";" +
                    @"" + Environment.NewLine;

            if (html)
                formatStr = "<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +
                    @"<a href =""{4}"">{5}</a>" + "</td><td>" +
                    @"<a href =""{6}"">{7}</a>" + "</td><td>" +
                    @"{8}" + "</td><td>" +
                    @"<a href =""{9}"">{10}</a>" + "</td><td>" +
                    @"{11}" + "</td><td>" +
                    @"{12}" + "</td><td>" +
                    @"{13}" + "</td><td>" +
                    @"<a href =""{14}"">{15}</a>" + "</td><td>" +                    
                    @"{16}" + "</td></tr>";
            
                result += String.Format(formatStr,
                    baseUrl + TorgNumberUrl, HTMLParser.ClearHtml(TorgNumberStr, html),
                    baseUrl + TorgNameUrl, HTMLParser.ClearHtml(TorgNameStr, html),
                    baseUrl + LotNumberUrl, HTMLParser.ClearHtml(LotNumberStr, html),
                    baseUrl + LotNameUrl, HTMLParser.ClearHtml(LotNameStr, html),
                    HTMLParser.ClearHtml(PriceStart, html),
                    baseUrl + OrganizerUrl, HTMLParser.ClearHtml(OrganizerStr, html),
                    DateAcceptFinish,
                    DateAuctionStart,
                    HTMLParser.ClearHtml(Status, html),
                    baseUrl + WinnerUrl, HTMLParser.ClearHtml(WinnerStr, html),
                    HTMLParser.ClearHtml(TorgType, html)
                    );
            /*                    
            else
                result += String.Format(
                    @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + ";" +
                    @"{12}" + ";" +
                    @"{13}" + ";" +
                    @"{15}" + ";" +
                    @"{16}" + Environment.NewLine +
                    // строка таблицы с ссылками
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"{4}" + ";" +
                    @"{6}" + ";" +
                    @"" + ";" +
                    @"{9}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{14}" + ";" +
                    @"" + Environment.NewLine,
                    baseUrl + TorgNumberUrl.Replace("\'", "").Replace("class", ""), TorgNumberStr,
                    baseUrl + TorgNameUrl.Replace("\"", ""), TorgNameStr,
                    baseUrl + LotNumberUrl.Replace("\"", "").Replace("\'", ""), LotNumberStr,
                    baseUrl + LotNameUrl.Replace("\"", ""), LotNameStr,
                    PriceStart,
                    baseUrl + OrganizerUrl.Replace("\"", ""), OrganizerStr,
                    DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    baseUrl + WinnerUrl.Replace("\"", ""), WinnerStr,
                    TorgType
                    );
                    */

            return result;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Centerr))
                return false;
            Centerr curObj = (Centerr)obj;

            if (this.DateAcceptFinish == curObj.DateAcceptFinish &
                this.DateAuctionStart == curObj.DateAuctionStart &
                this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.LotNumberStr == curObj.LotNumberStr &
                this.LotNumberUrl == curObj.LotNumberUrl &
                this.OrganizerStr == curObj.OrganizerStr &
                this.OrganizerUrl == curObj.OrganizerUrl &
                this.PriceStart == curObj.PriceStart &
                this.Status == curObj.Status &
                this.TorgNameStr == curObj.TorgNameStr &
                this.TorgNameUrl == curObj.TorgNameUrl &
                this.TorgNumberStr == curObj.TorgNumberStr &
                this.TorgNumberUrl == curObj.TorgNumberUrl &
                this.TorgType == curObj.TorgType &
                this.WinnerStr == curObj.WinnerStr &
                this.WinnerUrl == curObj.WinnerUrl)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 661515800;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNumberUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberUrl);            
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);            
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAuctionStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WinnerStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WinnerUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);

            internalID = hashCode.ToString();

            return hashCode;
        }
    }
}
