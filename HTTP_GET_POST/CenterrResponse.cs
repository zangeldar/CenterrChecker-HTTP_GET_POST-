using System;
using System.Collections.Generic;

namespace HTTP_GET_POST
{
    [Serializable]
    class CenterrResponse
    {
        public CenterrRequest MyRequest { get; private set; }
        public List<CenterrTableRowItem> ListResponse { get; private set; }
        public CenterrResponse(CenterrRequest myReq, List<CenterrTableRowItem> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
        }
    }
}
