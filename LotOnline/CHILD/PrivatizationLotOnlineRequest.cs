using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class PrivatizationLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://privatization.lot-online.ru/";
        public PrivatizationLotOnlineRequest() : base() { }
        public PrivatizationLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
