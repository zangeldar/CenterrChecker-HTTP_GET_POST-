using IAuction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Gz
{
    [Serializable]
    public class LotOnlineGzResponse : ATorgResponse
    {
        public LotOnlineGzResponse (string searchStr) : base(searchStr)
        {            
            MyRequest = new LotOnlineGzRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();            
        }

        public LotOnlineGzResponse(IRequest myReq) : base(myReq) { }
        public LotOnlineGzResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        //public override string SiteName => "РАД Закупки";        

        public override IResponse MakeFreshResponse => throw new NotImplementedException();

        public override int MaxItemsOnPage => 10;

        protected override string CreateTableForMailing(bool html = true)
        {
            throw new NotImplementedException();
        }

        protected override bool FillListResponse()
        {
            if (!base.FillListResponse())
                return false;
            //throw new NotImplementedException();

            List<LotOnlineGz> curlist = new List<LotOnlineGz>();
            JsonResponse myResp;

            try
            {
                myResp = JsonConvert.DeserializeObject<JsonResponse>(lastAnswer);
            }
            catch (Exception e)
            {
                lastError = e;
                return false;
                //throw;
            }
            if(myResp != null)
                if (myResp.Status != null)
                    if (myResp.Status == "0")
                        if (myResp.Data != null)
                            if (myResp.Data.Entities != null)
                                foreach (Entity item in myResp.Data.Entities)
                                    if (item != null)
                                        if (item.Procedure != null)
                                            curlist.Add(new LotOnlineGz(item.Procedure, MyRequest.ServiceURL));

            ListResponse = curlist;

            return true;
        }
    }
}
