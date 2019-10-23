using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TorgiASV
{
    [Serializable]
    public class ASVResponse : IResponse
    {
        private Exception lastError;
        public Exception LastError() { return lastError; }
        public string SiteName { get { return "Торги АСВ"; } }
        public IRequest MyRequest { get; private set; }
        public IEnumerable<IObject> ListResponse { get; private set; }

        public IEnumerable<IObject> NewRecords { get; private set; }

        public ASVResponse(string searchStr)
        {
            this.MyRequest = new ASVRequest(searchStr);
            FillListResponse();
        }

        public ASVResponse(IRequest myReq)
        {
            if (!(myReq is ASVRequest))
                return;
            this.MyRequest = myReq;
            FillListResponse();
        }

        public ASVResponse(ASVRequest myReq, List<ASV> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
        }

        private void FillListResponse()
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

        public IResponse MakeFreshResponse
        {
            get
            {
                return new ASVResponse(this.MyRequest);
            }
        }

        public bool SaveToXml(string fileName = "lastresponse.tasv")
        {
            return SaveMyASVResponseObject(this, fileName);
        }

        public IResponse LoadFromXml(string fileName = "lastresponse.tasv")
        {
            return LoadMyASVResponseObject(fileName);
        }

        static bool SaveMyASVResponseObject(ASVResponse curObj, string fileName = "lastresponse.tasv")
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

        static ASVResponse LoadMyASVResponseObject(string fileName = "lastresponse.tasv")
        {
            ASVResponse result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (ASVResponse)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        public bool HaveNewRecords(IResponse checkResponse)
        {
            haveNewRecords = false;
            if (!haveNewRecords)
            {
                NewRecords = DoOneCheck((ASVResponse)checkResponse, true);
                if (NewRecords != null)
                    if (NewRecords.Count() > 0)
                        haveNewRecords = true;
            }
            return haveNewRecords;
        }

        private bool haveNewRecords = false;

        public string NewRecordsOutput(IResponse checkResponse=null, bool html = true)
        {
            if (!haveNewRecords) 
            {
                if (!HaveNewRecords(checkResponse))
                {
                    if (MyRequest.LastError() != null)
                        return "ERROR: " + MyRequest.LastError().Message;
                    else
                        return "";
                }
            }

            string itemsTable = CreateTableForMailing(html);
            return PrepareMailBody(itemsTable, NewRecords.Count(), html);
        }

        private List<ASV> DoOneCheck(ASVResponse checkData = null, bool detail = false)
        {
            List<ASV> curListResponse = (List<ASV>)this.ListResponse;
            if (checkData != null)
                if (Enumerable.SequenceEqual(this.MyRequest.MyParameters, checkData.MyRequest.MyParameters)) // если запросы одинаковые, то
                    if (detail)                                                                     // если нужна детальная проверка, тогда
                        return GetListOfNewRecords((List<ASV>)checkData.ListResponse); // получаем все строки, исключая строки из последнего сохраненного результата
                    else
                        return GetListOfNewRecords((ASV)checkData.ListResponse.ToList()[0]); // получаем все строки, пока не наткнемся на первую из последнего сохраненного результата

            return curListResponse;
        }

        private List<ASV> GetListOfNewRecords(ASV checkRowItem) // проверка только по одной записи
        {
            List<ASV> inpList = (List<ASV>)this.ListResponse;
            List<ASV> result = new List<ASV>();

            if (inpList == null)
            {
                lastError = MyRequest.LastError();
                return result;
            }

            for (int i = 0; i < inpList.Count; i++)
            {
                //if (inpList[i].ToString() == checkRowItem.ToString()) 
                if (inpList[i].Equals(checkRowItem))
                    break;
                                   
                result.Add(inpList[i]);
            }

            return result;
        }

        private List<ASV> GetListOfNewRecords(List<ASV> checkRows)  // проверка по всем записям - ДОЛЬШЕ
        {
            List<ASV> inpList = (List<ASV>)this.ListResponse;
            List<ASV> result = new List<ASV>();

            if (inpList == null)
            {
                lastError = MyRequest.LastError();
                return result;
            }

            bool needBreak = false;
            for (int i = 0; i < inpList.Count; i++)
            {
                for (int j = 0; j < checkRows.Count; j++)
                {
                    //if (inpList[i].ToString() == checkRows[j].ToString())
                    if (inpList[i].Equals(checkRows[j]))
                    {
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak)
                    continue;
                result.Add(inpList[i]);
            }

            return result;
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

        private string PrepareMailBody(string inpRows, int rowsCount, bool html = true)
        {
            string messageBody = "";

            string parSet = "";

            string newLine = "\n";

            ASVRequest myObject = (ASVRequest)this.MyRequest;
            if (myObject != null)
                parSet = myObject.GetRequestStringPrintable();
            if (html)
            {
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
    }
}
