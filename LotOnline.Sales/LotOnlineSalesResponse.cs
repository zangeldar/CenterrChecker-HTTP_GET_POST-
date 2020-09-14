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
        public LotOnlineSalesResponse(string searchStr) : base(searchStr)
        {
            MyRequest = new LotOnlineSalesRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }
        public LotOnlineSalesResponse(IRequest myReq) : base(myReq) { }
        public LotOnlineSalesResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        //public override string SiteName => "Банкротство Лот-Онлайн";        

        public override IResponse MakeFreshResponse => new LotOnlineSalesResponse(MyRequest);

        public override int MaxItemsOnPage => 9;

        /*
        protected override string CreateTableForMailing(bool html = true)
        {
            //throw new NotImplementedException();
            return base.CreateTableForMailing(html);
        }
        */

        protected override bool FillListResponse()
        {
            /*
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;
            */
            if (!base.FillListResponse())
                return false;

            List<Tag> SearchResult = new List<Tag>();

            List<Tag> HTMLDoc = HTMLParser.Parse(lastAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)
                    SearchResult.AddRange(item.LookForChildTag("span", true, new KeyValuePair<string, string>("class", "teaser teaser-product")));
            }
            //

            List<LotOnlineSales> workList = new List<LotOnlineSales>();

            foreach (Tag item in SearchResult)
                workList.Add(new LotOnlineSales(item, MyRequest));

            this.ListResponse = workList;

            tableHead = new string[]
            {
                "№",
                "Наименование",
                "Цена"
            };

            return true;
        }
    }
}
