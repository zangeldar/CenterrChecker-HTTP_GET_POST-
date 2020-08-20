﻿using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZakupkiGov
{
    [Serializable]
    public class ZakupkiGovResponse : ATorgResponse
    {
        public override string SiteName => "ГосЗакупки";
        public override int MaxItemsOnPage => 10;
        public override IResponse MakeFreshResponse => new ZakupkiGovResponse(this.MyRequest);

        public ZakupkiGovResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new ZakupkiGovRequest(searchStr);
            FillListResponse();
        }

        public ZakupkiGovResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is ZakupkiGovRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }

        public ZakupkiGovResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            return SFileIO.LoadMyResponse(fileName);
        }

        public override bool SaveToXml(string fileName = "lastrequest.req", bool overwrite=false)
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
                @"{9}" + rowSeparatorEn + rowEnd,
                "Номер",
                "Наименование",
                "Оганизатор", 
                "Цена",
                "Статус",
                "Дата окончания приема заявок",
                "Дата обновления",
                "Дата публикации",
                "Тип торга",
                "Секция"
                );

            //foreach (ZakupkiGov item in (List<ZakupkiGov>)NewRecords)
            //foreach (ZakupkiGov item in (List<IObject>)NewRecords)
            foreach (ZakupkiGov item in NewRecords)
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

            //            
            List<Tag> SearchResult = new List<Tag>();

            List<Tag> HTMLDoc = HTMLParser.Parse(myWorkAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)                    
                    SearchResult.AddRange(item.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "row no-gutters registry-entry__form mr-0")));
            }
            //

            List<ZakupkiGov> workList = new List<ZakupkiGov>();

            foreach (Tag item in SearchResult)
                workList.Add(new ZakupkiGov(item));

            this.ListResponse = workList;

            return;
        }
    }
}
