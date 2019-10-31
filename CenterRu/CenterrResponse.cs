using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CenterRu
{
    class CenterrResponse : ATorgResponse
    {
        public CenterrResponse(string searchStr) : base(searchStr)
        {
            base.MyRequest = new CenterrRequest(searchStr);
        }

        public CenterrResponse(CenterrRequest myReq) : base(myReq)
        {
            base.MyRequest = myReq;
        }

        public CenterrResponse(CenterrRequest myReq, List<IObject> listResp) : base(myReq, listResp)
        {
            base.MyRequest = myReq;
            base.ListResponse = listResp;
        }

        public override string SiteName { get { return "Торги АСВ"; } }
            

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

        protected override string PrepareMailBody(string inpTableStr, int cnt, bool html = true)
        {
            throw new NotImplementedException();
        }
    }
}
