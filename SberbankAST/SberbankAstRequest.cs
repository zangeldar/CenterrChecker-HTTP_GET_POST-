using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SberbankAST
{
    class SberbankAstRequest : ATorgRequest
    {
        public SberbankAstRequest() : base() { }
        public SberbankAstRequest(string searchStr) : base(searchStr) { }

        public override string Type => "SberbankAST";

        public override string SiteName => "Сбербанк-АСТ";

        public override string ServiceURL => "https://www.sberbank-ast.ru/";

        public override string SearchString {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

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

        protected override string myRawPostData()
        {
            throw new NotImplementedException();
        }
    }
}
