using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace RosElTorg
{
    [Serializable]
    public class RosElTorgResponse : ATorgResponse
    {
        //public override string SiteName => "РосЭлТорг";
        public override int MaxItemsOnPage => 5;
        //public string Type => "RosElTorg";

        public override IResponse MakeFreshResponse => new RosElTorgResponse(this.MyRequest);
        
        public RosElTorgResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new RosElTorgRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }
        public RosElTorgResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is RosElTorgRequest))
                return;
            // Lines below already exist in base class
            /*
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
            */
        }
        public RosElTorgResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

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
                "Описание",
                "Цена",                
                "Организатор",
                "Регион",
                "Дата окончания приема заявок",
                "Статус",
                "Тип торга",
                "Секция",
                "Заметки"
                );

            foreach (RosElTorg item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }

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
                    //SearchResult.AddRange(item.LookForChildTag("ul", true, new KeyValuePair<string, string>("class", "component-list lot-catalog__list")));
                    SearchResult.AddRange(item.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "search-results__item")));
            }

            List<RosElTorg> workList = new List<RosElTorg>();

            foreach (Tag item in SearchResult)
                workList.Add(new RosElTorg(item, MyRequest));

            this.ListResponse = workList;

            return true;
        }
    }
}
