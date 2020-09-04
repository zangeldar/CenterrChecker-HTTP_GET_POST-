using IAuction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Tender
{
    [Serializable]
    public class LotOnlineTenderResponse : ATorgResponse
    {
        public LotOnlineTenderResponse(string searchStr) : base(searchStr)
        {
            MyRequest = new LotOnlineTenderRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }
        public LotOnlineTenderResponse(IRequest myReq) : base(myReq) { }
        public LotOnlineTenderResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        //public override string SiteName => "РАД Тендер";

        public override IResponse MakeFreshResponse => throw new NotImplementedException();

        public override int MaxItemsOnPage => 20;

        protected override string CreateTableForMailing(bool html = true)
        {
            throw new NotImplementedException();
        }

        protected override void FillListResponse()
        {
            base.FillListResponse();

            List<LotOnlineTender> curlist = new List<LotOnlineTender>();
            JsonResponse myResp;

            try
            {
                myResp = JsonConvert.DeserializeObject<JsonResponse>(lastAnswer);
            }
            catch (Exception e)
            {
                lastError = e;
                return;
                //throw;
            }
            
            if (myResp != null)
                if (myResp.List != null)
                    foreach (List item in myResp.List)
                        curlist.Add(new LotOnlineTender(item, MyRequest.ServiceURL));                        

            ListResponse = curlist;
        }
    }
}
