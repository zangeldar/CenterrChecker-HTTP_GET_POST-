using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETP_GPB
{
    public class GPBResponse : ATorgResponse
    {
        public GPBResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new GPBRequest(searchStr);
            FillListResponse();
        }
        public GPBResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is GPBRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public GPBResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override string SiteName => throw new NotImplementedException();

        public override IResponse MakeFreshResponse => throw new NotImplementedException();

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            throw new NotImplementedException();
        }

        public override bool SaveToXml(string fileName = "lastrequest.req")
        {
            throw new NotImplementedException();
        }

        protected override string CreateTableForMailing(bool html = true)
        {
            throw new NotImplementedException();
        }

        protected override void FillListResponse()
        {
            throw new NotImplementedException();
        }
    }
}
