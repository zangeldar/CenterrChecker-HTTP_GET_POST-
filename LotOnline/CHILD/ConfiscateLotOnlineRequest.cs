using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class ConfiscateLotOnlineRequest:LotOnlineRequest
    {
        public override bool isBuy => false;
        public override string SiteURL => "https://confiscate.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Конфискат";
        public ConfiscateLotOnlineRequest() : base() { }
        public ConfiscateLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
