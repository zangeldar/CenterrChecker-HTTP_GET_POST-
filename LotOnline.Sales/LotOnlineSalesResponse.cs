using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Sales
{
    [Serializable]
    public class LotOnlineSalesResponse : ATorgResponse
    {
        public LotOnlineSalesResponse(string searchStr) : base(searchStr) { }
        public LotOnlineSalesResponse(IRequest myReq) : base(myReq) { }
        public LotOnlineSalesResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override string SiteName => "Банкротство Лот-Онлайн";

        public override IResponse MakeFreshResponse => throw new NotImplementedException();

        protected override string CreateTableForMailing(bool html = true)
        {
            throw new NotImplementedException();
        }

        protected override void FillListResponse()
        {
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;
              
            List<Tag> SearchResult = new List<Tag>();

            List<Tag> HTMLDoc = HTMLParser.Parse(myWorkAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)
                    SearchResult.AddRange(item.LookForChildTag("span", true, new KeyValuePair<string, string>("class", "teaser teaser-product")));
            }
            //

            List<LotOnlineSales> workList = new List<LotOnlineSales>();

            foreach (Tag item in SearchResult)
                workList.Add(new LotOnlineSales(item));

            this.ListResponse = workList;
        }
    }
}
