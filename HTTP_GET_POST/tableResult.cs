using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP_GET_POST
{
    public struct tableResult
    {
        public string numTorg;
        public string numLot;
        public string nameLotStr;
        public string nameTorgStr;
        public string initialPriceStr;
        public string organizerStr;
        public string dateBid;
        public string dateStart;
        public string stateStr;
        public string winStr;
        public string typeStr;
        public override string ToString()
        {
            return String.Format("{0} | {1} | {2}", nameTorgStr, numLot, nameLotStr);
        }
    }

    static public class TableResult
    {
        static string parseOneItem(KeyValuePair<string, string> inpItem, ref tableResult curTable)
        {
            string myRes = "";

          

            return myRes;
        }

        static tableResult parseOneRow(string inpStr)
        {
            tableResult myResult = new tableResult();



            return myResult;
        }

        public static List<tableResult> parseAll(Dictionary<string, string> inpHtml)
        {
            List<tableResult> myResult = new List<tableResult>();
            tableResult myTmpTable = new tableResult();
            bool flagNew = false;

            foreach (var item in inpHtml)
            {
                if (item.Key.Contains("tip-purchase"))
                {
                    flagNew = true;
                    myTmpTable = new tableResult();
                    myTmpTable.nameTorgStr = item.Value;
                }
                else if (item.Key.Contains("tip-party"))
                {
                    myTmpTable.organizerStr = item.Value;
                    myResult.Add(myTmpTable);
                    flagNew = false;
                    //continue;
                }
                else if (flagNew)
                {
                    myTmpTable.numLot = item.Value;
                }                
            }

            return myResult;
        }

        
    }
}
