using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASVorgRU
{
    [Serializable]
    public class ASVorgResponse : ATorgResponse
    {
        public override string SiteName => "АСВ сайт";

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
            FillListResponse();
        }
        public ASVorgResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is ASVorgRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public ASVorgResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            return SFileIO.LoadMyResponse(fileName);
        }

        public override bool SaveToXml(string fileName = "lastrequest.req")
        {
            return SFileIO.SaveMyResponse(this, fileName);
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

            foreach (ASVorg item in (List<ASVorg>)NewRecords)
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
    }
}
