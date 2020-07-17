using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2B
{
    public class B2BResponse : ATorgResponse
    {
        public B2BResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is B2BRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }

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
