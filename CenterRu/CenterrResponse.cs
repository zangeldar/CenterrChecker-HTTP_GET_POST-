﻿using IAuction;
using HtmlParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace CenterRu
{
    [Serializable]
    public class CenterrResponse : ATorgResponse
    {
        //public override string SiteName => "Центр Реализации";
        public override int MaxItemsOnPage => 20;

        public CenterrResponse(string searchStr) : base(searchStr)
        {
            base.MyRequest = new CenterrRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }

        public CenterrResponse(IRequest myReq) : base(myReq)
        {
            //base.MyRequest = myReq;
            if (!(myReq is CenterrRequest))
                return;
        }

        public CenterrResponse(CenterrRequest myReq, List<IObject> listResp) : base(myReq, listResp)
        {
            base.MyRequest = myReq;
            base.ListResponse = listResp;
        }

        

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new CenterrResponse(this.MyRequest);
            }
        }

        

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
                rowEnd = Environment.NewLine;
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
                @"{10}" + rowSeparatorEn + rowEnd,
                "№",
                "Торги",
                "№ лота",
                "Лот",
                "Нач.цена",
                "Организатор",
                "Дата приема заявок",
                "Дата проведения",
                "Статус",
                "Победитель",
                "Тип торга"
                );

            //foreach (Centerr item in (List<Centerr>)NewRecords)
            //foreach (Centerr item in (List<IObject>)NewRecords)
            foreach (Centerr item in NewRecords)
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
            //            
            List<Tag> SearchResult = new List<Tag>();

            List<Tag> HTMLDoc = HTMLParser.Parse(lastAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)
                    SearchResult.AddRange(item.LookForChildTag("tr", true, new KeyValuePair<string, string>("class", "gridRow")));
            }

            List<Centerr> workList = new List<Centerr>();

            if (SearchResult.Count < 1)
            {
                Exception e = new Exception("Nothing found!");
                this.ListResponse = workList;
                return false;
            }

            foreach (Tag item in SearchResult)
            {
                workList.Add(new Centerr(item, MyRequest));
            }

            this.ListResponse = workList;
            freshResponse = true;
            return true;

            //return;
            //

            // Старый парсер
             /*
            //  Разбор результатов
            //myWorkAnswer = myHTMLParser.NormalizeString(myWorkAnswer);
            //myHTMLParser myHtmlParser = new myHTMLParser();
            List<_Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!            
            this.ListResponse = GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));

            freshResponse = true;
             */
            // 
        }
        /*
        static private List<Centerr> GetResultTableAsListOfMyObjects(List<List<StringUri>> inpList)
        {
            List<Centerr> resList = new List<Centerr>();

            for (int i = 1; i < inpList.Count; i++)
                resList.Add(new Centerr(inpList[i]));
            return resList;
        }
        */
    }
}
