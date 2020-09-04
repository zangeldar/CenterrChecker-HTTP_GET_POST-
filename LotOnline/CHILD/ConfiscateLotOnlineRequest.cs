using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class ConfiscateLotOnlineRequest:LotOnlineRequest
    {
        public override string ServiceURL => "https://confiscate.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Конфискат";
        public ConfiscateLotOnlineRequest() : base() { }
        public ConfiscateLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
