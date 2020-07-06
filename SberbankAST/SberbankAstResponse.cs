using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SberbankAST
{
    class SberbankAstResponse : ATorgResponse
    {
        public SberbankAstResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new SberbankAstRequest(searchStr);
            FillListResponse();
        }
        
        public SberbankAstResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is SberbankAstRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public SberbankAstResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

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
