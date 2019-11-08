using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAuction
{
    abstract public class ATorgResponse : IResponse
    {
        /// <summary>
        ////Часть интерфейса
        /// </summary>
        //public abstract IResponse MakeFreshResponse { get; }
        public abstract string SiteName { get; }
        //public abstract IRequest MyRequest { get; }
        //public abstract IEnumerable<IObject> ListResponse { get; private set; }
        //public abstract IEnumerable<IObject> NewRecords { get; }

        //public abstract bool HaveNewRecords(IResponse checkResponse);
        //public abstract Exception LastError();
        public abstract IResponse LoadFromXml(string fileName = "lastrequest.req");
        //public abstract string NewRecordsOutput(IResponse checkResponse, bool html);
        public abstract bool SaveToXml(string fileName = "lastrequest.req");

        //////////////////////
        ///часть абстрактного класса
        ///
        protected abstract string CreateTableForMailing(bool html = true);
        protected string PrepareMailBody(string inpTableStr, bool needCheckOnSite=false, bool html = true)
        {
            string messageBody = "";
            string parSet = "";
            string newLine = Environment.NewLine;
                        
            parSet = this.MyRequest.AllParametersInString(", ");

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
            messageBody += String.Format("По Вашему запросу: \"{0}\" были обнаружены новые записи:{2}{1}", parSet, inpTableStr, newLine);

            if (needCheckOnSite)
                messageBody += newLine + newLine + " Возможно, есть и другие новые записи! Обязательно проверьте на сайте!!";
            if (html)
            {
                messageBody += String.Format(@"</body>");
                messageBody += "</html>";
            }
            return messageBody;
        }
        /// <summary>
        /// Конструктор для получения Результата по новому Запросу из строки
        /// </summary>
        /// <param name="searchStr"></param>
        public ATorgResponse(string searchStr)
        {
            // need to make an response
            //this.MyRequest = new ATorgRequest(searchStr);
            FillListResponse();
        }
        /// <summary>
        /// Конструктор для получения нового Результата по имеющемуся Запросу
        /// </summary>
        /// <param name="myReq">Имеющийся Запрос</param>
        public ATorgResponse(ATorgRequest myReq)
        {
            if (!(myReq is IRequest))
                return;
            this.MyRequest = myReq;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        /// <summary>
        /// Конструктор для создания готового Результата из Запроса и списка Записей Результата
        /// </summary>
        /// <param name="myReq"></param>
        /// <param name="listResp"></param>
        public ATorgResponse(ATorgRequest myReq, List<IObject> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
            freshResponse = false;
        }
        protected bool freshResponse = false;
        protected abstract void FillListResponse();
        static protected List<List<StringUri>> GetResultTableAsList(List<List<StringUri>> inpList)
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

        /////////////////////////////
        ///часть реализации интерфейса
        ///

        private Exception lastError;
        public Exception LastError() { return lastError; }
        public IEnumerable<IObject> ListResponse { get; protected set; }
        public IRequest MyRequest { get; protected set; }
        public IEnumerable<IObject> NewRecords { get; private set; }                
        public IResponse MakeFreshResponse { get; protected set; }
        private bool haveNewRecords;
        public bool HaveNewRecords(IResponse checkResponse)
        {
            haveNewRecords = false;
            if (!haveNewRecords)
            {
                NewRecords = DoOneCheck((IResponse)checkResponse, true);
                if (NewRecords != null)
                    if (NewRecords.Count() > 0)
                        haveNewRecords = true;
            }
            return haveNewRecords;
        }
        public string NewRecordsOutput(IResponse checkResponse = null, bool html = true)
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
            return PrepareMailBody(itemsTable, (NewRecords.Count()>20), html);
        }
        private List<IObject> DoOneCheck(IResponse checkData = null, bool detail = false)
        {
            List<IObject> curListResponse = (List<IObject>)this.ListResponse;
            if (checkData != null)
                if (Enumerable.SequenceEqual(this.MyRequest.MyParameters, checkData.MyRequest.MyParameters)) // если запросы одинаковые, то
                    if (detail)                                                                     // если нужна детальная проверка, тогда
                        return GetListOfNewRecords((List<IObject>)checkData.ListResponse); // получаем все строки, исключая строки из последнего сохраненного результата
                    else
                        return GetListOfNewRecords((IObject)checkData.ListResponse.ToList()[0]); // получаем все строки, пока не наткнемся на первую из последнего сохраненного результата

            return curListResponse;
        }
        private List<IObject> GetListOfNewRecords(IObject checkRowItem) // проверка только по одной записи
        {
            List<IObject> inpList = (List<IObject>)this.ListResponse;
            List<IObject> result = new List<IObject>();

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
        private List<IObject> GetListOfNewRecords(List<IObject> checkRows)  // проверка по всем записям - ДОЛЬШЕ
        {
            List<IObject> inpList = (List<IObject>)this.ListResponse;
            List<IObject> result = new List<IObject>();
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
    }
}
