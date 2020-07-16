using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SberbankAST
{
    class SberbankAstResponse : ATorgResponse
    {
        public override string SiteName => "Сбербанк-АСТ";
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

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new SberbankAstResponse(this.MyRequest);
            }
        }

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            return SFileIO.LoadMyResponse(fileName);
            //throw new NotImplementedException();
        }

        public override bool SaveToXml(string fileName = "lastrequest.req")
        {
            return SFileIO.SaveMyResponse(this, fileName);
            //throw new NotImplementedException();
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
