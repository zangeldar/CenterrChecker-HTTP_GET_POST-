using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class LotOnlineResponse : ATorgResponse
    {
        public LotOnlineResponse(string searchStr) : base(searchStr) { }
        public LotOnlineResponse(IRequest myReq) : base(myReq) { }
        public LotOnlineResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override string SiteName => "Лот-Онлайн (РАД)";

        public override IResponse MakeFreshResponse => throw new NotImplementedException();

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
