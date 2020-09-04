using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class PrivatizationLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://privatization.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Приватизация";
        public PrivatizationLotOnlineRequest() : base() { }
        public PrivatizationLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
