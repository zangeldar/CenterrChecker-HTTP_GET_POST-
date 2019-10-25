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
        protected abstract string PrepareMailBody(string inpTableStr, int cnt, bool html = true);
        public ATorgResponse(string searchStr)
        {
            // need to make an response
            //this.MyRequest = new ATorgRequest(searchStr);
            //FillListResponse();
        }
        public ATorgResponse(ATorgRequest myReq)
        {
            if (!(myReq is IRequest))
                return;
            this.MyRequest = myReq;
            FillListResponse();
        }
        public ATorgResponse(ATorgRequest myReq, List<IObject> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
            freshResponse = false;
        }
        private bool freshResponse = false;
        protected abstract void FillListResponse();

        /////////////////////////////
        ///часть реализации интерфейса
        ///

        private Exception lastError;
        public Exception LastError() { return lastError; }
        public IEnumerable<IObject> ListResponse { get; private set; }
        public IRequest MyRequest { get; private set; }
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
            return PrepareMailBody(itemsTable, NewRecords.Count(), html);
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
