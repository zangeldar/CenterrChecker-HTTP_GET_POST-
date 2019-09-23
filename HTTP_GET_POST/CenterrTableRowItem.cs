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
        public CenterrTableRowItem LastResponse { get; private set; }
        public CenterrRequestResponseObject(MyClass myReq, CenterrTableRowItem lastResp)
        {
            this.MyRequest = myReq;
            this.LastResponse = lastResp;
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
            string result = "";

            result = String.Format("\n" +
                @"{0}" + "\t|\t" +
                @"{1}" + "\t|\t" +
                @"{2}" + "\t|\t" +
                @"{3}" + "\t|\t" +
                @"{4}" + "\t|\t" +
                @"{5}" + "\t|\t" +
                @"{6}" + "\t|\t" +
                @"{7}" + "\t|\t" +
                @"{8}" + "\t|\t" +
                @"{9}" + "\t|\t" +
                @"{10}" + "\t|\t",
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

            result += String.Format("\n" +
                @"<a href =""{0}"">{1}</a>"      + "\t|\t" +
                @"<a href =""{2}"">{3}</a>"      + "\t|\t" +
                @"<a href =""{4}"">{5}</a>"      + "\t|\t" +
                @"<a href =""{6}"">{7}</a>"      + "\t|\t" +
                @"{8}"                           + "\t|\t" +
                @"<a href =""{9}"">{10}</a>"     + "\t|\t" +
                @"{11}"                          + "\t|\t" +
                @"{12}"                          + "\t|\t" +
                @"{13}"                          + "\t|\t" +
                @"<a href =""{14}"">{15}</a>"    + "\t|\t",
                TorgNumber.ItemUri, TorgNumber.ItemString,
                TorgName.ItemUri, TorgName.ItemString,
                LotNumber.ItemUri, LotNumber.ItemString,
                LotName.ItemUri, LotName.ItemString,
                PriceStart,
                Organizer.ItemUri, Organizer.ItemString,
                DateAcceptFinish,
                DateAuctionStart,
                Status,
                Winner.ItemUri, Winner.ItemString,
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
