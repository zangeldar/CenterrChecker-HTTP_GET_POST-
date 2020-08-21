using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTSTender
{
    public class RTSTenderResponse : ATorgResponse
    {
        public RTSTenderResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new RTSTenderRequest(searchStr);
            FillListResponse();
        }
        public RTSTenderResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is RTSTenderRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public RTSTenderResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override string SiteName => throw new NotImplementedException();
        public override int MaxItemsOnPage => 10;

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
                    //SearchResult.AddRange(item.LookForChildTag("ul", true, new KeyValuePair<string, string>("class", "component-list lot-catalog__list")));
                    SearchResult.AddRange(item.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "section-procurement__item")));
            }

            List<RTSTender> workList = new List<RTSTender>();

            foreach (Tag item in SearchResult)
                workList.Add(new RTSTender(item));

            this.ListResponse = workList;

            return;

            throw new NotImplementedException();
            //throw new NotImplementedException();
        }
    }
}
