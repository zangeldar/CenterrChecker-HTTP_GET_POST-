using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class ZalogLotOnlineRequest : LotOnlineRequest
    {
        public override bool isBuy => false;
        public override string SiteURL => "https://zalog.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Залог";

        //public override string ServURL => throw new NotImplementedException();

        public ZalogLotOnlineRequest() : base() { }
        public ZalogLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
