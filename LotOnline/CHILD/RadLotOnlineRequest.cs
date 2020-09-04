using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class RadLotOnlineRequest:LotOnlineRequest
    {
        public override string ServiceURL => "https://rad.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн РАД";
        public RadLotOnlineRequest() : base() { }
        public RadLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
