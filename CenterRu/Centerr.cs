using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace CenterRu
{
    //[Serializable]
    public class Centerr : ATorg
    {
        public Centerr(List<StringUri> itemsList)
        {
            if (itemsList.Count != 11)
                return;

            TorgNumber = itemsList[0];
            TorgName = itemsList[1];
            LotNumber = itemsList[2];
            LotName = itemsList[3];
            PriceStart = itemsList[4].ItemString;
            Organizer = itemsList[5];
            DateAcceptFinish = itemsList[6].ItemString;
            DateAuctionStart = itemsList[7].ItemString;
            Status = itemsList[8].ItemString;
            Winner = itemsList[9];
            TorgType = itemsList[10].ItemString;
        }

        override public string internalID { get; protected set; }
        public StringUri TorgNumber { get; private set; }
        public StringUri TorgName { get; private set; }
        public StringUri LotNumber { get; private set; }
        override public string LotNumberStr { get { return LotNumber.ItemString; } protected set { } }
        public string LotNumberUrl { get { return LotNumber.ItemUri; } }
        public StringUri LotName { get; private set; }
        override public string LotNameStr { get { return LotName.ItemString; } protected set { } }
        override public string LotNameUrl { get { return LotName.ItemUri; } protected set { } }
        public StringUri Organizer { get; private set; }
        override public string PriceStart { get; protected set; }
        public string DateAcceptFinish { get; private set; }
        public string DateAuctionStart { get; private set; }
        public string Status { get; private set; }
        public StringUri Winner { get; private set; }
        public string TorgType { get; private set; }

        public override string ToString(bool html)
        {
            string baseUri = "http://bankrupt.centerr.ru";
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
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
                    //@"{16}"                          + "</td></tr></table>",
                    @"{16}" + "</td></tr>",
                    baseUri + TorgNumber.ItemUri.Replace("\'", "").Replace("class", ""), TorgNumber.ItemString,
                    baseUri + TorgName.ItemUri.Replace("\"", ""), TorgName.ItemString,
                    baseUri + LotNumber.ItemUri.Replace("\"", "").Replace("\'", ""), LotNumber.ItemString,
                    baseUri + LotName.ItemUri.Replace("\"", ""), LotName.ItemString,
                    PriceStart,
                    baseUri + Organizer.ItemUri.Replace("\"", ""), Organizer.ItemString,
                    DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    baseUri + Winner.ItemUri.Replace("\"", ""), Winner.ItemString,
                    TorgType
                    );
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
                    baseUri + TorgNumber.ItemUri.Replace("\'", "").Replace("class", ""), TorgNumber.ItemString,
                    baseUri + TorgName.ItemUri.Replace("\"", ""), TorgName.ItemString,
                    baseUri + LotNumber.ItemUri.Replace("\"", "").Replace("\'", ""), LotNumber.ItemString,
                    baseUri + LotName.ItemUri.Replace("\"", ""), LotName.ItemString,
                    PriceStart,
                    baseUri + Organizer.ItemUri.Replace("\"", ""), Organizer.ItemString,
                    DateAcceptFinish,
                    DateAuctionStart,
                    Status,
                    baseUri + Winner.ItemUri.Replace("\"", ""), Winner.ItemString,
                    TorgType
                    );

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
                this.Organizer.ItemString == curObj.Organizer.ItemString &
                this.Organizer.ItemUri == curObj.Organizer.ItemUri &
                this.PriceStart == curObj.PriceStart &
                this.Status == curObj.Status &
                this.TorgName.ItemString == curObj.TorgName.ItemString &
                this.TorgName.ItemUri == curObj.TorgName.ItemUri &
                this.TorgNumber.ItemString == curObj.TorgNumber.ItemString &
                this.TorgNumber.ItemUri == curObj.TorgNumber.ItemUri &
                this.TorgType == curObj.TorgType &
                this.Winner.ItemString == curObj.Winner.ItemString &
                this.Winner.ItemUri == curObj.Winner.ItemUri)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 661515800;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<StringUri>.Default.GetHashCode(TorgNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<StringUri>.Default.GetHashCode(TorgName);
            hashCode = hashCode * -1521134295 + EqualityComparer<StringUri>.Default.GetHashCode(LotNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<StringUri>.Default.GetHashCode(LotName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<StringUri>.Default.GetHashCode(Organizer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAuctionStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<StringUri>.Default.GetHashCode(Winner);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);

            internalID = hashCode.ToString();

            return hashCode;
        }
    }
}
