using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace TekTorg
{
    [Serializable]
    public class TekTorgResponse : ATorgResponse
    {
        public override string SiteName => "ТЭК-Торг";

        public override IResponse MakeFreshResponse => new TekTorgResponse(this.MyRequest);

        public TekTorgResponse (string searchStr) : base(searchStr)
        {
            this.MyRequest = new TekTorgRequest(searchStr);
            FillListResponse();
        }

        public TekTorgResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is TekTorgRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }

        public TekTorgResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            return SFileIO.LoadMyResponse(fileName);
        }
                
        public override bool SaveToXml(string fileName = "lastrequest.req", bool overwrite = false)
        {
            return SFileIO.SaveMyResponse(this, fileName, overwrite);
        }
        
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
                @"{9}" + rowSeparatorEn + rowSeparatorSt +
                @"{10}" + rowSeparatorEn + rowSeparatorSt +                
                @"{11}" + rowSeparatorEn + rowEnd,
                "Секция",
                "Тип торга",
                "№ торга",
                "Описание торга",
                "Организатор",
                "Цена",
                "Статус",
                "Дата публикации",
                "Дата окончания приема заявок",
                "Дата проведения аукциона",
                "Дата подведения итогов",
                "Заметки"
                );
                        
            foreach (TekTorg item in NewRecords)
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
                    SearchResult.AddRange(item.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "section-procurement__item")));
            }

            List<TekTorg> workList = new List<TekTorg>();

            foreach (Tag item in SearchResult)
                workList.Add(new TekTorg(item));

            this.ListResponse = workList;

            return;

            throw new NotImplementedException();
        }
    }
}
