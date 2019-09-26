using System;
using System.Collections.Generic;

namespace HTTP_GET_POST
{
    [Serializable]
    class CenterrResponse
    {
        public CenterrRequest MyRequest { get; private set; }
        public List<CenterrTableRowItem> ListResponse { get; private set; }

        public CenterrResponse(CenterrRequest myReq)
        {
            this.MyRequest = myReq;
            FillListResponse();
        }
        public CenterrResponse(CenterrRequest myReq, List<CenterrTableRowItem> listResp)
        {
            this.MyRequest = myReq;
            this.ListResponse = listResp;
        }

        private void FillListResponse()
        {
            string myWorkAnswer = MyRequest.GetResponse;

            //  Разбор результатов
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!            
            this.ListResponse = CenterrResponse.GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));
        }


        static public List<CenterrTableRowItem> GetResultTableAsListOfMyObjects(List<List<StringUri>> inpList)
        {
            List<CenterrTableRowItem> resList = new List<CenterrTableRowItem>();

            for (int i = 1; i < inpList.Count; i++)
                resList.Add(new CenterrTableRowItem(inpList[i]));

            return resList;
        }

        static public List<List<StringUri>> GetResultTableAsList(List<List<StringUri>> inpList)
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
    }
}
