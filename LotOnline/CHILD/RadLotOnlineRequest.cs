using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class RadLotOnlineRequest:LotOnlineRequest
    {
        public override bool isBuy => false;
        public override string SiteURL => "https://rad.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн РАД";
        public RadLotOnlineRequest() : base() { }
        public RadLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
