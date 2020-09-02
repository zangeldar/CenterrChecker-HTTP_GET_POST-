using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class ZalogLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://zalog.lot-online.ru/";
        public ZalogLotOnlineRequest() : base() { }
        public ZalogLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
