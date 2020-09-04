using CenterRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace UTender
{
    [Serializable]
    public class UTenderResponse : CenterrResponse
    {
        public override string SiteName { get { return "Ю-Тендер"; } }

        public UTenderResponse(string searchStr) : base(searchStr)
        {
            //this.SiteName = this.MyRequest.SiteName;
        }

        public UTenderResponse(IRequest myReq) : base(myReq)
        {
        }

        public UTenderResponse(UTenderRequest myReq, List<IObject> listResp) : base(myReq, listResp)
        {
        }

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new UTenderResponse(this.MyRequest);
            }
        }
    }
}
