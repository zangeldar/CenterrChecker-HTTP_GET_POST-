using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2B
{
    public class B2BRequest : ATorgRequest
    {
        public override string Type => "B2B";

        public override string SiteName => "B2B центр";

        public override string ServiceURL => "https://www.b2b-center.ru/";

        public override string SearchString { get => MyParameters["f_keyword"]; set => MyParameters["f_keyword"] = value; }

        public override IResponse MakeResponse()
        {
            //throw new NotImplementedException();
            return new B2BResponse(this);
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

        protected override string myRawPostData()
        {
            throw new NotImplementedException();
        }
    }
}
