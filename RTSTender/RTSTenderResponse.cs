using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTSTender
{
    [Serializable]
    public class RTSTenderResponse : ATorgResponse
    {
        public RTSTenderResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new RTSTenderRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }
        public RTSTenderResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is RTSTenderRequest))
                return;
            // below already exist in base class
            /*
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
            */
        }
        public RTSTenderResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        //public override string SiteName => "РТС-тендер";
        public override int MaxItemsOnPage => 10;

        public override IResponse MakeFreshResponse => new RTSTenderResponse(this.MyRequest);

        protected override string CreateTableForMailing(bool html = true)
        {
            string result;
            string rowStart;
            string rowEnd;
            string rowSeparatorSt;
            string rowSeparatorEn;
            if (html)
            {
                rowStart = @"<tr>";
                rowEnd = @"</tr>";
                rowSeparatorSt = @"<th>";
                rowSeparatorEn = @"</th>";
                result = @"<table border=""1"">";
            }
            else
            {
                rowStart = @"";
                rowEnd = "\n";
                rowSeparatorSt = @"";
                rowSeparatorEn = @";";
                result = "";
            }

            result += String.Format(rowStart + rowSeparatorSt +
                @"{0}" + rowSeparatorEn + rowSeparatorSt +
                @"{1}" + rowSeparatorEn + rowSeparatorSt +
                @"{2}" + rowSeparatorEn + rowSeparatorSt +
                @"{3}" + rowSeparatorEn + rowSeparatorSt +
                @"{4}" + rowSeparatorEn + rowSeparatorSt +
                @"{5}" + rowSeparatorEn + rowSeparatorSt +
                @"{6}" + rowSeparatorEn + rowSeparatorSt +
                @"{7}" + rowSeparatorEn + rowSeparatorSt +
                @"{8}" + rowSeparatorEn + rowSeparatorSt +
                @"{9}" + rowSeparatorEn + rowEnd,
                "№ торга", 
                "Торг",                
                "Организатор",
                "Заказчик",
                "Адрес поставки",
                "Цена",                
                "Дата публикации",
                "Дата окончания приема заявок",
                "Параметры торга",
                "Подробности"
                );

            foreach (RTSTender item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
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
                    SearchResult.AddRange(item.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "card-item")));
                    /*
                    if (item.Name == "div")
                        if (item.Attributes.ContainsKey("class"))
                            if (item.Attributes["class"] == "cards")
                                SearchResult.Add(item);
                                */
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
