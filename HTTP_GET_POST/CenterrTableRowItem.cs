using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_GET_POST
{
    [Serializable]
    class CenterrRequestResponseObject
    {
        public MyClass MyRequest { get; private set; }
        public List<CenterrTableRowItem> ListResponse { get; private set; }
        public CenterrRequestResponseObject(MyClass myReq, List<CenterrTableRowItem> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
        }
    }
    [Serializable]
    class CenterrTableRowItem
    {
        public StringUri TorgNumber { get; private set; }
        public StringUri TorgName { get; private set; }
        public StringUri LotNumber { get; private set; }
        public StringUri LotName { get; private set; }
        public StringUri Organizer { get; private set; }
        public string PriceStart { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string DateAuctionStart { get; private set; }
        public string Status { get; private set; }
        public StringUri Winner { get; private set; }
        public string TorgType { get; private set; }

        public CenterrTableRowItem(List<StringUri> itemsList)
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
        public override string ToString()
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
    }

    [Serializable]
    struct StringUri
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
