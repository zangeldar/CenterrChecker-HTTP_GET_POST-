using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class LeaseLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://lease.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Лизинг";
        public LeaseLotOnlineRequest() : base() { }
        public LeaseLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
