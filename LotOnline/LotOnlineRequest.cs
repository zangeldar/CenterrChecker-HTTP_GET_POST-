using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class LotOnlineRequest : ATorgRequest
    {
        public override string Type => "LotOnline";

        public override string SiteName => "Лот-Онлайн (РАД)";

        public override string ServiceURL => "https://lot-online.ru/";

        public override string SearchString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override IResponse MakeResponse()
        {
            throw new NotImplementedException();
        }

        protected override string getBlankResponse()
        {
            throw new NotImplementedException();
        }

        protected override void InitialiseParameters()
        {
            throw new NotImplementedException();
        }

        protected override bool Initialize()
        {
            throw new NotImplementedException();
        }

        protected override string MakePost(string postData = "")
        {
            throw new NotImplementedException();
        }
    }
}
