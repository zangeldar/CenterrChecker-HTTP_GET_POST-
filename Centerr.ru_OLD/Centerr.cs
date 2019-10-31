using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CenterrRu
{
    [Serializable]
    public class Centerr : IObject
    {
        public string internalID { get; private set; }
        public StringUri TorgNumber { get; private set; }
        public StringUri TorgName { get; private set; }
        public StringUri LotNumber { get; private set; }
        public string LotNumberStr { get { return LotNumber.ItemString; } }
        public string LotNumberUrl { get { return LotNumber.ItemUri; } }
        public StringUri LotName { get; private set; }
        public string LotNameStr { get { return LotName.ItemString; } }
        public string LotNameUrl { get { return LotName.ItemUri; } }
        public StringUri Organizer { get; private set; }
        public string PriceStart { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string DateAuctionStart { get; private set; }
        public string Status { get; private set; }
        public StringUri Winner { get; private set; }
        public string TorgType { get; private set; }

        public Centerr(List<StringUri> itemsList)
        {
            if (itemsList.Count != 11)
                return;
            
            TorgNumber          = itemsList[0];
            TorgName            = itemsList[1];
            LotNumber           = itemsList[2];
            LotName             = itemsList[3];
            PriceStart          = itemsList[4].ItemString;
            Organizer           = itemsList[5];
            DateAcceptFinish    = itemsList[6].ItemString;
            DateAuctionStart    = itemsList[7].ItemString;
            Status              = itemsList[8].ItemString;
            Winner              = itemsList[9];
            TorgType            = itemsList[10].ItemString;
            
        }
        /*public override string ToString()
        {
            string baseUri = "http://bankrupt.centerr.ru";
            string result = "";

            /*
            //result = String.Format(@"<table border=""1""><tr><th>" +
            result = String.Format(@"<tr><th>" +
                @"{0}" + "</th><th>" +
                @"{1}" + "</th><th>" +
                @"{2}" + "</th><th>" +
                @"{3}" + "</th><th>" +
                @"{4}" + "</th><th>" +
                @"{5}" + "</th><th>" +
                @"{6}" + "</th><th>" +
                @"{7}" + "</th><th>" +
                @"{8}" + "</th><th>" +
                @"{9}" + "</th><th>" +
                @"{10}" + "</th><tr>",
                "№",
                "Торги",
                "№ лота",
                "Лот",
                "Нач.цена",
                "Организатор",
                "Дата приема заявок",
                "Дата проведения",
                "Статус",
                "Победитель",
                "Тип торга"
                );
                */
/*
            result += String.Format("<tr><td>" +
                @"<a href =""{0}"">{1}</a>"      + "</td><td>" +
                @"<a href =""{2}"">{3}</a>"      + "</td><td>" +
                @"<a href =""{4}"">{5}</a>"      + "</td><td>" +
                @"<a href =""{6}"">{7}</a>"      + "</td><td>" +
                @"{8}"                           + "</td><td>" +
                @"<a href =""{9}"">{10}</a>"     + "</td><td>" +
                @"{11}"                          + "</td><td>" +
                @"{12}"                          + "</td><td>" +
                @"{13}"                          + "</td><td>" +
                @"<a href =""{14}"">{15}</a>"    + "</td><td>" +
                //@"{16}"                          + "</td></tr></table>",
                @"{16}" + "</td></tr>",
                baseUri + TorgNumber.ItemUri.Replace("\'","").Replace("class",""), TorgNumber.ItemString,
                baseUri + TorgName.ItemUri.Replace("\"",""), TorgName.ItemString,
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
        */
        public string ToString(bool html = true)
        {
            string baseUri = "http://bankrupt.centerr.ru";
            string result="";

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

        public override bool Equals(Object obj)
        {
            //return base.Equals(obj);

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
            return base.GetHashCode();
        }

        /*
        static public List<Centerr> GetResultTableAsListOfMyObjects(List<List<StringUri>> inpList)
        {
            List<Centerr> resList = new List<Centerr>();

            for (int i = 1; i < inpList.Count; i++)
                resList.Add(new Centerr(inpList[i]));

            return resList;
        }
        */
    }
}
