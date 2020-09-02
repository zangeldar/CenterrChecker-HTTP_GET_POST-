using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class LeaseLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://lease.lot-online.ru/";
        public LeaseLotOnlineRequest() : base() { }
        public LeaseLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
