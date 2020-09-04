using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class FishLotOnlineRequest : LotOnlineRequest
    {
        public override string ServiceURL => "https://fish.lot-online.ru/";
        public override string SiteName => "Лот-Онлайн Рыба";
        public FishLotOnlineRequest() : base() { }
        public FishLotOnlineRequest(string searchStr) : base(searchStr) { }
    }
}
