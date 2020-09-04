using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class ArrestedLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://arrested.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Арест";
        public ArrestedLotOnlineRequest() : base() { }
        public ArrestedLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
