using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class TradeLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://trade.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Торги";
        public TradeLotOnlineRequest() : base() { }
        public TradeLotOnlineRequest(string searchStr) : base(searchStr) { }
    }    
}
