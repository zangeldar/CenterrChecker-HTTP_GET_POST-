using IAuction;
using HtmlParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASVorgRU
{
    [Serializable]
    public class ASVorgResponse : ATorgResponse
    {
        //public override string SiteName => "АСВ сайт";
        public override int MaxItemsOnPage => 55;

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new ASVorgResponse(this.MyRequest);
            }
        }

        public ASVorgResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new ASVorgRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }
        public ASVorgResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is ASVorgRequest))
                return;
            // Lines below already exist in base class
            /*
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
            */
        }
        public ASVorgResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

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
                rowEnd = Environment.NewLine;
                rowSeparatorSt = @"";
                rowSeparatorEn = @";";
                result = "";
            }


            result += String.Format(rowStart + rowSeparatorSt +
                @"{0}" + rowSeparatorEn + rowSeparatorSt +
                @"{1}" + rowSeparatorEn + rowSeparatorSt +
                @"{2}" + rowSeparatorEn + rowEnd,
                "Раздел",
                "Заголовок",
                "Содержание"
                );

            //foreach (ASVorg item in (List<ASVorg>)NewRecords)
            //foreach (ASVorg item in (List<IObject>)NewRecords)            
            foreach (ASVorg item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;            
        }

        /*
        protected void _FillListResponse()
        {
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;
            
            List<ASVorg> curList = new List<ASVorg>();

            myHTMLParser myParser = new myHTMLParser();
            List<_Tag> myListCaption = myParser.getTags(myWorkAnswer, "h3");
            
            List<_Tag> myList = myParser.getTags(myWorkAnswer, "ol");
            List<_Tag> resList = new List<_Tag>();

            int k = 0;
            string sectionName;
            foreach (_Tag itemSection in myList) // первый уровень - разделы (страхование, ликвидация и т.п.)
            {
                sectionName = "";
                if (k < myListCaption.Count)
                    sectionName = myListCaption[k].Value;
                
                foreach (_Tag item in itemSection.InnerTags) // второй уровень - записи
                    curList.Add(new ASVorg(item, sectionName));                
                k++;
            }
            
            this.ListResponse = curList;
            
        }
        */

        protected override void FillListResponse()
        {
            /*
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;
            */

            base.FillListResponse();

            //
            List<Tag> SearchResultTmp = new List<Tag>();
            List<ASVorg> workList = new List<ASVorg>();

            List<Tag> HTMLDoc = HTMLParser.Parse(lastAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)
                    SearchResultTmp.AddRange(item.LookForChildTag("ol", true));                
            }
            List<Tag> SearchResult = new List<Tag>();
            foreach(Tag item in SearchResultTmp)
            {
                SearchResult.AddRange(item.LookForChildTag("li", true));
            }

            if (SearchResult.Count < 1)
            {
                if (lastAnswer.Contains("emptyResultsBlock"))
                {
                    lastError = new Exception("Поиск не дал результатов");
                    this.ListResponse = workList;
                    return;
                }
                lastError = new Exception("Ответ сервера не содержит данных (ожидались результаты с тегом \"div\" и классом \"procedure__data\"):" + Environment.NewLine + lastAnswer);
                this.ListResponse = workList;
                return;
            }

            foreach (Tag item in SearchResult)
            {
                workList.Add(new ASVorg(item));
            }

            this.ListResponse = workList;
            return;

            //            
            /*
            List<ASVorg> curList = new List<ASVorg>();

            myHTMLParser myParser = new myHTMLParser();
            List<_Tag> myListCaption = myParser.getTags(myWorkAnswer, "h3");

            List<_Tag> myList = myParser.getTags(myWorkAnswer, "ol");
            List<_Tag> resList = new List<_Tag>();

            int k = 0;
            string sectionName;
            foreach (_Tag itemSection in myList) // первый уровень - разделы (страхование, ликвидация и т.п.)
            {
                sectionName = "";
                if (k < myListCaption.Count)
                    sectionName = myListCaption[k].Value;

                foreach (_Tag item in itemSection.InnerTags) // второй уровень - записи
                    curList.Add(new ASVorg(item, sectionName));
                k++;
            }
            
            this.ListResponse = curList;
            */

        }
    }
}
