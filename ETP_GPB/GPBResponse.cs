using IAuction;
using HtmlParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETP_GPB
{
    [Serializable]
    public class GPBResponse : ATorgResponse
    {
        public override string SiteName => "ЭТП ГПБ";
        public override int MaxItemsOnPage => 30;
        public GPBResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new GPBRequest(searchStr);
            FillListResponse();
        }
        public GPBResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is GPBRequest))
                return;
            // Lines below already exist in base class
            /*
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();            
            FillListResponse();
            */
        }
        public GPBResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new GPBResponse(this.MyRequest);
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
                @"{2}" + rowSeparatorEn + rowSeparatorSt +
                @"{3}" + rowSeparatorEn + rowSeparatorSt + 
                @"{4}" + rowSeparatorEn + rowSeparatorSt +
                @"{5}" + rowSeparatorEn + rowSeparatorSt +
                @"{6}" + rowSeparatorEn + rowSeparatorSt +
                @"{7}" + rowSeparatorEn + rowSeparatorSt +
                @"{8}" + rowSeparatorEn + rowSeparatorSt +
                @"{9}" + rowSeparatorEn + rowSeparatorSt +
                @"{10}" + rowSeparatorEn + rowEnd,
                "Номер лота",                                       //  0  LotNumberStr    
                "Лот",                                              //  1  LotNameStr + LotNameUrl
                "Описание",                                         //  2  TorgName
                "Регион",                                           //  3   Region
                "Организатор",                                      //  4   OrganizerStr
                "Цена",                                             //  5   PriceStart
                "Секция",                                           //  6   Section
                "Тип торга",                                        //  7   TorgType
                "Свойства",                                         //  8   Props
                "Статус",                                           //  9   Status
                "Дата оконания приема заявок"                       //  10  DateAcceptFinish
                );

            //foreach (GPB item in (List<GPB>)NewRecords)
            //foreach (GPB item in (List<IObject>)NewRecords)
            foreach (GPB item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }

        private string ListToString (List<string> inpList, string separator = null)
        {
            string result = "";
            if (separator == null)
                separator = Environment.NewLine;

            foreach (string item in inpList)
                result += item + separator;

            return result;
        }

        protected override void FillListResponse()
        {
            //throw new NotImplementedException();
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;

            List<Tag> SearchResult = new List<Tag>();
            List<GPB> workList = new List<GPB>();
            
            List<Tag> HTMLDoc = HTMLParser.Parse(myWorkAnswer);
            foreach (Tag item in HTMLDoc)
            {
                if (!item.IsProto)
                    //SearchResult.AddRange(item.LookForChildTag("table", true));                
                    SearchResult.AddRange(item.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "procedure__data")));
            }
            
            if (SearchResult.Count < 1)
            {
                if (myWorkAnswer.Contains("emptyResultsBlock"))
                {
                    lastError = new Exception("Поиск не дал результатов");
                    this.ListResponse = workList;
                    return;
                }
                lastError = new Exception("Ответ сервера не содержит данных (ожидались результаты с тегом \"div\" и классом \"procedure__data\"):" + Environment.NewLine + myWorkAnswer);
                this.ListResponse = workList;
                return;
            }           

            foreach (Tag item in SearchResult)
            {
                workList.Add(new GPB(item));
            }

            this.ListResponse = workList;
            return;
        }
    }
}
