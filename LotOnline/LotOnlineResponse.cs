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
        public LotOnlineResponse(string searchStr, string baseUrl= "https://lot-online.ru/") : base(searchStr)
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    this.baseUrl = baseUrl;
            this.MyRequest = new LotOnlineRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
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

        public override int MaxItemsOnPage => 5;

        protected override string CreateTableForMailing(bool html = true)
        {
            throw new NotImplementedException();
        }

        protected override void FillListResponse()
        {
            /*
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;
                */

            base.FillListResponse();

            List<LotOnline> workList = new List<LotOnline>();
            JsonResults myResp = JsonConvert.DeserializeObject<JsonResults>(lastAnswer);

            foreach (JsonRow item in myResp.Rows)
                workList.Add(new LotOnline(item, MyRequest.ServiceURL));

            this.ListResponse = workList;
        }
    }
}
