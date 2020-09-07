using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2B
{
    [Serializable]
    public class B2BResponse : ATorgResponse
    {
        //public override string SiteName => "B2B-центр";
        public override int MaxItemsOnPage => 20;
        public B2BResponse (string searchStr) : base(searchStr)
        {
            this.MyRequest = new B2BRequest(searchStr);
            this.SiteName = this.MyRequest.SiteName;
            FillListResponse();
        }
        public B2BResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is B2BRequest))
                return;
            // Lines below already exist in base class
            /*
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
            */
        }

        public B2BResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new B2BResponse(this.MyRequest);
            }
        }

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            //throw new NotImplementedException();
            return SFileIO.LoadMyResponse(fileName);
        }

        public override bool SaveToXml(string fileName = "lastrequest.req", bool overwrite = false)
        {
            //throw new NotImplementedException();
            return SFileIO.SaveMyResponse(this, fileName, overwrite);
        }
        
        protected override bool FillListResponse()
        {
            //throw new NotImplementedException();
            /*
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;
            */
            if (!base.FillListResponse())
                return false;

            //
            // /*
            List<Tag> SearchResult = new List<Tag>();

            List<Tag> HTMLDoc = HTMLParser.Parse(lastAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)
                    //SearchResult.AddRange(item.LookForChildTag("table", true));                
                    SearchResult.AddRange(item.LookForChildTag("tbody", true));
            }

            List<B2B> workList = new List<B2B>();

            if (SearchResult.Count < 1)
            {
                if (lastAnswer.Contains("search-results empty_results"))
                {
                    lastError = new Exception("Поиск не дал результатов");
                    this.ListResponse = workList;
                    return false;
                }
                lastError = new Exception("Ответ сервера не содержит данных (ожидалась 1 таблица с тегом \"tbody\"):" + Environment.NewLine + lastAnswer);
                this.ListResponse = workList;
                return false;
            }
            else if (SearchResult.Count > 1)
            {
                lastError = new Exception("Ответ сервера содержит неожиданную структуру (ожидалась 1 таблица с тегом \"tbody\"):" + Environment.NewLine + lastAnswer);
                this.ListResponse = workList;
                return false;
            }

            foreach (Tag item in SearchResult[0].ChildTags)
            {
                workList.Add(new B2B(item));
            }

            this.ListResponse = workList;
            return true;
            // */
            //            

            /*
            // По старому парсеру

            List<B2B> curList = new List<B2B>();

            myHTMLParser myParser = new myHTMLParser();
                        
            List<_Tag> myListCaption = myParser.getTags(myWorkAnswer, "table");

            //
            //List<Tag> myList = myParser.getTags(myWorkAnswer, "ol");
            //List<Tag> resList = new List<Tag>();
            //

            if (myListCaption.Count < 1)
            {
                if (myWorkAnswer.Contains("search-results empty_results"))
                {
                    lastError = new Exception("Поиск не дал результатов");
                    this.ListResponse = curList;
                    return;
                }
                    
                lastError = new Exception("Ответ сервера не содержит данных (ожидалась 1 таблица с тегом \"table\"):" + Environment.NewLine + myWorkAnswer);
                return;
            }
            else if (myListCaption.Count != 1)   // какой-то неожиданный ответ
            {
                lastError = new Exception("Ответ сервера содержит неожиданную структуру (ожидалась 1 таблица с тегом \"table\"):" + Environment.NewLine + myWorkAnswer);
                return;
            }                        

            if (myListCaption[0].InnerTags.Count < 1)
            {
                lastError = new Exception("Таблица не содержит данных (ожидалось 2 раздела - заголовки и данные):" + Environment.NewLine + myWorkAnswer);
                return;
            }
            else if (myListCaption[0].InnerTags.Count != 2)
            {
                lastError = new Exception("Таблица содержит неожиданную структуру (ожидалось 2 раздела - заголовки и данные):" + Environment.NewLine + myWorkAnswer);
                return;
            }

            if (!myListCaption[0].InnerTags[1].HasInnerTags)
            {
                lastError = new Exception("Похоже, что не нашлось результатов поиска:" + Environment.NewLine + myWorkAnswer);
                return;
            }

            foreach (_Tag item in myListCaption[0].InnerTags[1].InnerTags)
            {
                if (item.InnerTags.Count == 4)
                    if ((item.InnerTags[0].InnerTags.Count == 1) & (item.InnerTags[1].InnerTags.Count == 1))
                        curList.Add(new B2B(item));
            }  

            this.ListResponse = curList;
            //
            */
        }

        protected override string CreateTableForMailing(bool html = true)
        {
            //throw new NotImplementedException();

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
                @"{2}" + rowSeparatorEn + rowSeparatorSt +
                @"{3}" + rowSeparatorEn + rowSeparatorSt +
                @"{4}" + rowSeparatorEn + rowSeparatorSt +
                @"{5}" + rowSeparatorEn + rowSeparatorSt +
                @"{6}" + rowSeparatorEn + rowEnd,
                "Номер лота",
                "Лот",
                "Совпадения",
                "Организатор",
                "Тип торга",
                "Дата публикации",
                "Дата оконания приема заявок"
                );

            //foreach (B2B item in (List<B2B>)NewRecords)
            //foreach (B2B item in (List<IObject>)NewRecords)
            foreach (B2B item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }
    }
}
