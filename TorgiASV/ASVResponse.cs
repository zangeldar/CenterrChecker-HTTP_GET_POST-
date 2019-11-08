using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace TorgiASV
{
    //[Serializable]
    public class ASVResponse : ATorgResponse
    {
        public override string SiteName => "Торги АСВ";

        public ASVResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new ASVRequest(searchStr);
            FillListResponse();
        }
        public ASVResponse(ATorgRequest myReq) : base(myReq)
        {
            if (!(myReq is ASVRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public ASVResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

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
                "ID",
                "Номер Лота",
                "Лот",
                "Описание",
                "Банк",
                "Регион",
                "Нач. цена"
                );

            foreach (ASV item in (List<ASV>)NewRecords)
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

            List<ASV> curList = new List<ASV>();

            myHTMLParser myParser = new myHTMLParser();
            List<Tag> myList = myParser.getTags(myWorkAnswer, "ul");
            List<Tag> resList = new List<Tag>();

            bool found = false;     // вычленяем из всех списков на странице только нужный
            foreach (Tag item in myList)
            {
                foreach (tagAttribute atItem in item.Attributes)
                {
                    if (atItem.Name == "class" & atItem.Value == "component-list lot-catalog__list")
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    resList.Add(item);
                    break;
                }
            }

            foreach (Tag item in resList[0].InnerTags)    // заполняем результаты по списку
            {
                curList.Add(new ASV(item.InnerTags));
            }
            this.ListResponse = curList;
        }

        ////////////////
        ///
        ///////////////////

        
    }
}
