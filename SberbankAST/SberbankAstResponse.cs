using IAuction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SberbankAST
{
    public class SberbankAstResponse : ATorgResponse
    {
        public override string SiteName => "Сбербанк-АСТ";
        public SberbankAstResponse(string searchStr) : base(searchStr)
        {
            this.MyRequest = new SberbankAstRequest(searchStr);
            FillListResponse();
        }
        
        public SberbankAstResponse(IRequest myReq) : base(myReq)
        {
            if (!(myReq is SberbankAstRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        public SberbankAstResponse(ATorgRequest myReq, List<IObject> listResp) : base(myReq, listResp) { }

        public override IResponse MakeFreshResponse
        {
            get
            {
                return new SberbankAstResponse(this.MyRequest);
            }
        }

        public override IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            return SFileIO.LoadMyResponse(fileName);
            //throw new NotImplementedException();
        }

        public override bool SaveToXml(string fileName = "lastrequest.req")
        {
            return SFileIO.SaveMyResponse(this, fileName);
            //throw new NotImplementedException();
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
                "Номер Лота",
                "Торг",
                "Лот",
                "Нач. цена",
                "Организатор",
                "Период подачи заявок",
                "Дата аукциона",
                "Статус",
                "Тип торга",
                "Секция"
                );

            foreach (SberbankAst item in (List<SberbankAst>)NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }

        protected override void FillListResponse()
        {
            //throw new NotImplementedException();

            List<SberbankAst> curList = new List<SberbankAst>();
            JsonResponseData dataJson;

            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;

            try
            {
                // парсим первый уровень ответа (JSON)
                JsonResponse myResp = JsonConvert.DeserializeObject<JsonResponse>(myWorkAnswer);

                // парсим второй уровень ответа (JSON)
                JsonRoot myRoot = JsonConvert.DeserializeObject<JsonRoot>(myResp.data);

                // парсим третий уровень (JSON)
                dataJson = JsonConvert.DeserializeObject<JsonResponseData>(myRoot.data);

                // ИЛИ
                /*
                // парсим третий уровень (XML)
                XmlSerializer ser = new XmlSerializer(typeof(MyDataRow));
                MyDataRow myDataRow = (MyDataRow)ser.Deserialize(new StringReader(myRoot.tableXml));
                */
            }
            catch (Exception e)
            {
                lastError = e;
                return;
                //throw;
            }            

            foreach (Hit hit in dataJson.Hits.Hits)
                curList.Add(new SberbankAst(hit));

            ListResponse = curList;
        }
    }
}
