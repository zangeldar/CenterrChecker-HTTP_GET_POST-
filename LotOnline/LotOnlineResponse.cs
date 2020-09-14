using IAuction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class LotOnlineResponse : ATorgResponse
    {
        public string baseUrl { get; private set; }
        // Отключено, так как lot-online просто посредник и сам не отдает результатов. От Класса LotOnlineRequest тольок наследуют, поэтому он абстрактный
        /*
        public LotOnlineResponse(string searchStr, string baseUrl= "https://lot-online.ru/") : base(searchStr)
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    this.baseUrl = baseUrl;
            this.MyRequest = new LotOnlineRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
        }
        */
        public LotOnlineResponse(IRequest myReq) : base(myReq)
        {
            this.baseUrl = myReq.ServiceURL;            
        }
        public LotOnlineResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp)
        {
            this.baseUrl = myReq.ServiceURL;
        }
        /*
        public static LotOnlineResponse FactoryMethod(string searchStr, string baseUrl = "https://lot-online.ru/")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    return new LotOnlineResponse(searchStr, baseUrl);
            return new LotOnlineResponse(searchStr);
        }

        public static LotOnlineResponse FactoryMethod(IRequest myReq, string baseUrl = "https://lot-online.ru/")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    return new LotOnlineResponse(myReq, baseUrl);
            return new LotOnlineResponse(myReq);
        }

        public static LotOnlineResponse FactoryMethod(ATorgRequest myReq, List<IObject> listResp, string baseUrl = "https://lot-online.ru/")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    return new LotOnlineResponse(myReq, listResp, baseUrl);
            return new LotOnlineResponse(myReq, listResp);
        }
        */

        //public override string SiteName => "Лот-Онлайн";        

        public override IResponse MakeFreshResponse => new LotOnlineResponse(this.MyRequest);

        public override int MaxItemsOnPage => 10;

        protected override string CreateTableForMailing(bool html = true)
        {
            string result;
            string rowStart;
            string rowEnd;
            string rowSeparatorSt;
            string rowSeparatorEn;
            if (html)
            {
                rowStart = @"<tr>";
                rowEnd = @"</tr>";
                rowSeparatorSt = @"<th>";
                rowSeparatorEn = @"</th>";
                result = @"<table border=""1"">";
            }
            else
            {
                rowStart = @"";
                rowEnd = "\n";
                rowSeparatorSt = @"";
                rowSeparatorEn = @";";
                result = "";
            }

            result += rowStart;
            foreach (string item in tableSrc)
                result += rowSeparatorSt + item + rowSeparatorEn;

            result += rowSeparatorEn;

            foreach (LotOnline item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }

        private string[] tableSrc = new string[]
        {
            "№",
            "Наименование",
            "Цена",
            "Организатор",
            "Адрес",
            "Дата начала",            
            "Описание",
            "Источник"
        };
        
        

        protected override bool FillListResponse()
        {
            if (!base.FillListResponse())
                return false;

            List<LotOnline> workList = new List<LotOnline>();
            JsonResults myResp = JsonConvert.DeserializeObject<JsonResults>(lastAnswer);

            foreach (JsonRow item in myResp.Rows)
                workList.Add(new LotOnline(item, MyRequest));

            this.ListResponse = workList;
            return true;
        }
    }
}
