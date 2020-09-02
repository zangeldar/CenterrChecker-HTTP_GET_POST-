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
        public LotOnlineResponse(string searchStr, string baseUrl= "https://lot-online.ru/5") : base(searchStr)
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    this.baseUrl = baseUrl;
        }
        public LotOnlineResponse(IRequest myReq) : base(myReq)
        {
            this.baseUrl = myReq.ServiceURL;            
        }
        public LotOnlineResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp)
        {
            this.baseUrl = myReq.ServiceURL;
        }
        /*
        public static LotOnlineResponse FactoryMethod(string searchStr, string baseUrl = "https://lot-online.ru/5")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    return new LotOnlineResponse(searchStr, baseUrl);
            return new LotOnlineResponse(searchStr);
        }

        public static LotOnlineResponse FactoryMethod(IRequest myReq, string baseUrl = "https://lot-online.ru/4")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    return new LotOnlineResponse(myReq, baseUrl);
            return new LotOnlineResponse(myReq);
        }

        public static LotOnlineResponse FactoryMethod(ATorgRequest myReq, List<IObject> listResp, string baseUrl = "https://lot-online.ru/3")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    return new LotOnlineResponse(myReq, listResp, baseUrl);
            return new LotOnlineResponse(myReq, listResp);
        }
        */

        public override string SiteName => "Лот-Онлайн";

        public override IResponse MakeFreshResponse => throw new NotImplementedException();

        protected override string CreateTableForMailing(bool html = true)
        {
            throw new NotImplementedException();
        }

        protected override void FillListResponse()
        {
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;

            List<LotOnline> workList = new List<LotOnline>();
            JsonResults myResp = JsonConvert.DeserializeObject<JsonResults>(myWorkAnswer);

            foreach (JsonRow item in myResp.Rows)
                workList.Add(new LotOnline(item, MyRequest.ServiceURL));

            this.ListResponse = workList;
        }
    }
}
