using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace TorgiASV
{
    [Serializable]
    public class TorgASVResponse : ATorgResponse
    {
        public override string SiteName { get { return "Торги АСВ"; } }

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new TorgASVResponse(this.MyRequest);
            }
        }

        public TorgASVResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new TorgASVRequest(searchStr);
            FillListResponse();
        }
        public TorgASVResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is TorgASVRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public TorgASVResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

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
                @"{6}" + rowSeparatorEn + rowEnd,
                "Секция",
                "Наименование",
                "Описание",
                "Регион",
                "Организатор",
                "Цена",
                "Лоты"
                );

            //foreach (TorgASV item in (List<TorgASV>)NewRecords)
            //foreach (TorgASV item in (List<IObject>)NewRecords)
            foreach (TorgASV item in NewRecords)
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
                    //SearchResult.AddRange(item.LookForChildTag("ul", true, new KeyValuePair<string, string>("class", "component-list lot-catalog__list")));
                    SearchResult.AddRange(item.LookForChildTag("li", true, new KeyValuePair<string, string>("class", "component-list__item lot-catalog__list-item")));
            }
            //

            List<TorgASV> workList = new List<TorgASV>();

            foreach (Tag item in SearchResult)
                workList.Add(new TorgASV(item));

            this.ListResponse = workList;

            return;
            /*
            //  По старому парсеру, на основе RegEx
            List<TorgASV> curList = new List<TorgASV>();

            myHTMLParser myParser = new myHTMLParser();
            List<_Tag> myList = myParser.getTags(myWorkAnswer, "ul");
            List<_Tag> resList = new List<_Tag>();

            bool found = false;     // вычленяем из всех списков на странице только нужный
            foreach (_Tag item in myList)
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

            this.ListResponse = curList;
            //if (!found)
            if (resList.Count == 0) // если результатов нет
                return;         // тогда возврат
            foreach (_Tag item in resList[0].InnerTags)    // заполняем результаты по списку
            {
                curList.Add(new TorgASV(item.InnerTags));
            }
            this.ListResponse = curList;
            //
            */
        }

        ////////////////
        ///
        ///////////////////

        
    }
}
