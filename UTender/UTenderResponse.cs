using CenterRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace UTender
{
    public class UTenderResponse : CenterrResponse
    {
        public UTenderResponse(string searchStr) : base(searchStr)
        {
        }

        public UTenderResponse(IRequest myReq) : base(myReq)
        {
        }

        public UTenderResponse(CenterrRequest myReq, List<IObject> listResp) : base(myReq, listResp)
        {
        }
    }
}
