using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace TorgiASV
{
    [Serializable]
    public class TorgASV : ATorg
    {
        public TorgASV(List<Tag> itemsList)
        {
            LotName = new StringUri
            {
                ItemString = itemsList[2].InnerTags[0].InnerTags[0].Value.Replace("  ", "").Replace("\n", ""),
                ItemUri = "https://torgiasv.ru" + itemsList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "")
            };

            //
            //AsvID = .Substring();
            string urlID = itemsList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "");
            int startID = urlID.Substring(0, urlID.Length - 1).LastIndexOf('/');
            urlID = urlID.Substring(startID).Replace("/", "");
            //AsvID = int.Parse(urlID);
            internalID = urlID;
            //
            LotDesc = itemsList[3].Value;
            TorgBank = itemsList[4].Value;
            TorgRegion = itemsList[5].Value;
            PriceStart = itemsList[7].InnerTags[0].Value.Replace("<span class=\"text-muted\">", "");
            LotNumberStr = itemsList[9].Value;
        }

        override public string internalID { get; protected set; }
        public StringUri LotName { get; private set; }
        override public string LotNameStr { get { return LotName.ItemString; } protected set { } }
        override public string LotNameUrl { get { return LotName.ItemUri; } protected set { } }
        public String LotDesc { get; private set; }
        override public String LotNumberStr { get; protected set; }
        public String TorgBank { get; private set; }
        public String TorgRegion { get; private set; }
        override public String PriceStart { get; protected set; }

        public override bool Equals(Object obj)
        {
            if (!(obj is TorgASV))
                return false;
            TorgASV curObj = (TorgASV)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.LotDesc == curObj.LotDesc &
                this.LotNumberStr == curObj.LotNumberStr &
                this.TorgBank == curObj.TorgBank &
                this.TorgRegion == curObj.TorgRegion &
                this.PriceStart == curObj.PriceStart)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();            
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);

            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
                    @"{0}" + "</td><td>" +
                    @"{1}" + "</td><td>" +
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +
                    @"{4}" + "</td><td>" +
                    @"{5}" + "</td><td>" +
                    @"{6}" + "</td><td>" +
                    @"{7}" + "</td></tr>",
                    internalID,
                    LotNumberStr,
                    LotName.ItemUri, LotName.ItemString,
                    LotDesc,
                    TorgBank,
                    TorgRegion,
                    PriceStart
                    );
            else
                result += String.Format(
                    @"{0}" + ";" +
                    @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"" + ";" +
                    @"" + ";" +
                    @"{2}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + Environment.NewLine,
                    internalID,
                    LotNumberStr,
                    LotName.ItemUri, LotName.ItemString,
                    LotDesc,
                    TorgBank,
                    TorgRegion,
                    PriceStart
                    );

            return result;
        }
    }
}
