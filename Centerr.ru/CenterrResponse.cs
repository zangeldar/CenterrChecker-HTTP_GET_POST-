using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CenterrRu
{
    [Serializable]
    public class CenterrResponse : IResponse
    {        
        public string SiteName { get { return "Центр реализации"; } }
        public Exception LastError { get; private set; }
        bool freshResponse = false;
        //public CenterrRequest MyRequest { get; private set; }
        //public List<Centerr> ListResponse { get; private set; }
        public IRequest MyRequest { get; private set; }
        public IEnumerable<IObject> ListResponse { get; private set; }
        public IEnumerable<IObject> NewRecords { get; private set; }

        public CenterrResponse(string searchStr)
        {
            this.MyRequest = new CenterrRequest(searchStr);
            FillListResponse();
        }

        public CenterrResponse(CenterrRequest myReq)
        {
            this.MyRequest = myReq;
            FillListResponse();
        }

        public CenterrResponse(CenterrRequest myReq, List<Centerr> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
            freshResponse = false;
        }

        private void FillListResponse()
        {
            string myWorkAnswer = MyRequest.GetResponse;
            if (myWorkAnswer == null)
                return;

            //  Разбор результатов
            myWorkAnswer = myHTMLParser.NormalizeString(myWorkAnswer);
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!            
            this.ListResponse = GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));

            freshResponse = true;
        }

        public string[] GetNewData(IResponse checkData)
        {
            string[] result = null;



            return result;
        }

        static private List<Centerr> GetResultTableAsListOfMyObjects(List<List<StringUri>> inpList)
        {
            List<Centerr> resList = new List<Centerr>();            

            for (int i = 1; i < inpList.Count; i++)
                resList.Add(new Centerr(inpList[i]));

            return resList;
        }

        static private List<List<StringUri>> GetResultTableAsList(List<List<StringUri>> inpList)
        {
            List<List<StringUri>> resList = new List<List<StringUri>>();

            // 1. Calculate MAX columns count
            int colCount = 0;
            foreach (List<StringUri> itemList in inpList)
                colCount = Math.Max(colCount, itemList.Count);

            // 2. Fill result rows
            foreach (List<StringUri> itemListRows in inpList)
            {
                if (itemListRows.Count != colCount)     // Skip all rows that have not another count of columns instead MAX columns count
                    continue;
                resList.Add(itemListRows);
            }

            return resList;
        }

        /// <summary>
        /// ReFactor
        /// </summary>
        /// <param name="myRequestObj"></param>
        /// <param name="checkDate"></param>
        /// <returns></returns>
        //public string[] DoOneCheck(this.)

        /*
        static bool DoOneCheck(CenterrRequest myRequestObj, string checkDate)
        {
            string myWorkAnswer;
            string resultString;
            //string checkDate;

            myWorkAnswer = myRequestObj.GetResponse;

            //  Разбор результатов
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!
            List<List<StringUri>> myLT = GetResultTableAsList(myTable);
            List<Centerr> myCrObjects = GetResultTableAsListOfMyObjects(myLT);            
            //System.Data.DataTable myDT = GetTableAsDT(myLT);
            System.Data.DataTable myDT = myHTMLParser.GetTableAsDT(myLT);

            // Проверка на наличие новых сообщений и отправка почты            
            resultString = myHTMLParser.GetNewRowsString(myLT, checkDate);
            if (resultString.Length > 0)
                return SendMailRemind(PrepareMailBody(myRequestObj, resultString, 1));
                //return true;
            // Конец запроса

            return false;
        }
        */

        /*
        static bool DoOneCheck(CenterrRequest myRequestObj, CenterrResponse checkData = null)
        {
            CenterrResponse CurrentObj;

            string myWorkAnswer = myRequestObj.GetResponse;

            //  Разбор результатов
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!            
            //List<CenterrTableRowItem> myCrObjects = GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));
            List<Centerr> myCrObjects = GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));            

            List<Centerr> newItems = new List<Centerr>();

            CurrentObj = new CenterrResponse(myRequestObj, myCrObjects);

            if (checkData != null)
                //if (myRequestObj.MyParameters == checkData.MyRequest.MyParameters)
                if (Enumerable.SequenceEqual(myRequestObj.MyParameters, checkData.MyRequest.MyParameters))
                {
                    newItems = Centerr.GetListOfNewRecords((List<Centerr>)CurrentObj.ListResponse, (Centerr)checkData.ListResponse.ToArray()[0]);
                    if (newItems.Count < 1)
                        return false;
                }
                else
                    newItems = myCrObjects;
            else
                newItems = myCrObjects;

            // сохранить сериализованный CurrentObj для дальнейших проверок
            SaveMyCenterrObject(CurrentObj, CenterrRequest.GenerateFileName((CenterrRequest)CurrentObj.MyRequest));

            return SendMailRemind(PrepareMailBody(myRequestObj, Centerr.CreateTableForMailing(newItems), newItems.Count));
        }
        */





        /*
        static List<Centerr> DoOneCheck(IResponse newData, IResponse checkData = null)
        {
            if (checkData != null)
                if (Enumerable.SequenceEqual(newData.MyRequest.MyParameters, checkData.MyRequest.MyParameters))
                    return Centerr.GetListOfNewRecords((List<Centerr>)newData.ListResponse, (Centerr)checkData.ListResponse.ToList()[0]);

            return (List<Centerr>)newData.ListResponse;
        }
        */
        public bool SaveToXml (string fileName = "lastresponse.bcntr")
        {
            return SaveMyCenterrObject(this, fileName);
        }
        public IResponse LoadFromXml(string fileName = "lastresponse.bcntr")
        {
            return LoadMyCenterrObject(fileName);
        }
        static bool SaveMyCenterrObject(CenterrResponse curObj, string fileName = "lastresponse.bcntr")
        {
            bool result = false;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream output = File.OpenWrite(fileName))
                {
                    bf.Serialize(output, curObj);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                //throw;
            }

            return result;
        }

        static CenterrResponse LoadMyCenterrObject(string fileName = "lastresponse.bcntr")
        {
            CenterrResponse result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (CenterrResponse)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        private string PrepareMailBody(string inpRows, int rowsCount, bool html=true)
        {
            string messageBody="";

            string parSet = "";

            string newLine = "\n";
            newLine = Environment.NewLine;

            CenterrRequest myObject = (CenterrRequest)this.MyRequest;
            if (myObject != null)
                parSet = myObject.GetRequestStringPrintable();
            if (html) {
                newLine = "<br>";
                messageBody += String.Format(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.=w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">");
                messageBody += String.Format(@"<html xmlns=""http://www.w3.org/1999/xhtml"">");
                messageBody += String.Format(@"<head>");
                messageBody += String.Format(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-=1"" />");
                messageBody += String.Format(@"</head>");
                messageBody += String.Format(@"<body marginwidth=""0"" marginheight=""0"" leftmargin=""0"" topmargin=""0"" style=""width: 100 % !important"">");
            }
            messageBody += String.Format("По Вашему запросу: \"{0}\" были обнаружены новые записи:{2}{1}", parSet, inpRows, newLine);

            if (rowsCount >= 20)
                messageBody += newLine + newLine + " Возможно, есть и другие новые записи! Обязательно проверьте на сайте!!";
            if (html)
            {
                messageBody += String.Format(@"</body>");
                messageBody += "</html>";
            }                
            return messageBody;
        }

        private bool haveNewRecords;
        public bool HaveNewRecords(IResponse checkResponse)
        {
            haveNewRecords = false;
            if (!haveNewRecords)
            {
                NewRecords = DoOneCheck((CenterrResponse)checkResponse);
                if (NewRecords != null)
                    if (NewRecords.Count() > 0)
                        haveNewRecords = true;
            }
            return haveNewRecords;

            /*
            NewRecords = DoOneCheck((CenterrResponse)checkResponse);

            if (NewRecords != null)
                if (NewRecords.Count() > 0)
                    return true;

            return false;
            */
        }

        public string NewRecordsOutput(IResponse checkResponse=null, bool html=true)
        {
            if (!haveNewRecords)
            {
                if (!HaveNewRecords(checkResponse))
                {
                    if (MyRequest.LastError != null)
                        return "ERROR: " + MyRequest.LastError.Message;
                    else
                        return "";
                }
            }

            string itemsTable = CreateTableForMailing(html);
            return PrepareMailBody(itemsTable, NewRecords.Count(), html);
            /*
            if (HaveNewRecords(checkResponse))
            {
                string itemsTable = CreateTableForMailing(html);
                return PrepareMailBody(itemsTable, NewRecords.Count(), html);
            }
            return MyRequest.LastError.Message;
            */
        }

        private string CreateTableForMailing(bool html = true)
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
            

            result += String.Format(rowStart+rowSeparatorSt +
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

            foreach (Centerr item in (List<Centerr>)NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }


        private List<Centerr> DoOneCheck(CenterrResponse checkData = null, bool detail = false)
        {
            List<Centerr> curListResponse = (List<Centerr>)this.ListResponse;
            if (checkData != null)
                if (Enumerable.SequenceEqual(this.MyRequest.MyParameters, checkData.MyRequest.MyParameters)) // если запросы одинаковые, то
                    if (detail)                                                                     // если нужна детальная проверка, тогда
                        return GetListOfNewRecords((List<Centerr>)checkData.ListResponse); // получаем все строки, исключая строки из последнего сохраненного результата
                    else
                        return GetListOfNewRecords((Centerr)checkData.ListResponse.ToList()[0]); // получаем все строки, пока не наткнемся на первую из последнего сохраненного результата

            return curListResponse;
        }

        private List<Centerr> GetListOfNewRecords(Centerr checkRowItem) // проверка только по одной записи
        {
            List<Centerr> inpList = (List<Centerr>)this.ListResponse; 
            List<Centerr> result = new List<Centerr>();

            for (int i = 0; i < inpList.Count; i++)
            {
                //if (inpList[i].ToString() == checkRowItem.ToString())
                if (inpList[i].Equals(checkRowItem))
                    break;
                result.Add(inpList[i]);
            }

            return result;
        }

        private List<Centerr> GetListOfNewRecords(List<Centerr> checkRows)  // проверка по всем записям - ДОЛЬШЕ
        {
            List<Centerr> inpList = (List<Centerr>)this.ListResponse;
            List<Centerr> result = new List<Centerr>();

            for (int i = 0; i < inpList.Count; i++)
            {
                for (int j = 0; j < checkRows.Count; j++)
                {
                    //if (inpList[i].ToString() == checkRows[j].ToString())
                    if (inpList[i].Equals(checkRows[j]))
                        break;
                    result.Add(inpList[i]);
                }                             
            }

            return result;
        }
    }
}
