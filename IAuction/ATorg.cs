//using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    [Serializable]
    public abstract class ATorg : IObject
    {
        //
        // Часть интерфейса
        //
        public ATorg()
        {
            InitializeParams();
        }
        public ATorg(IRequest myReq)
        {
            if (myReq.ServiceURL != null)
                if (myReq.ServiceURL != "")
                    this.baseUrl = myReq.ServiceURL;
            InitializeParams();
        }
        private void InitializeParams()
        {
            TableRowMeans = new string[]
            {
                LotNumberStr,
                LotNameStr,
                PriceStart,
            };
            TableRowUrls = new string[TableRowMeans.Length];
            TableRowUrls = new string[]
            {
                "",
                LotNameUrl,
                ""
            };
        }
        public string baseUrl { get; protected set; }
        public string[] TableRowMeans { get; protected set; }
        public string[] TableRowUrls { get; protected set; }

        public Exception LastError { get; protected set; }
        public virtual string internalID { get; protected set; }
        public virtual string LotNameStr { get; protected set; }
        public virtual string LotNameUrl { get; protected set; }
        public virtual string PriceStart { get; protected set; }
        public virtual string LotNumberStr { get; protected set; }
        public virtual string ToString(bool html)
        {
            string result = "";

            if (html)
            {
                result = "<tr>";
                for (int i = 0; i < TableRowMeans.Length; i++)
                {
                    if (TableRowUrls[i] != null & TableRowUrls[i] != "")
                        result += @"<td><a href=""" + TableRowUrls[i] + @""">" + TableRowMeans[i] + @"</a></td>";
                    else
                        result += @"<td>" + TableRowMeans[i] + "</td>";
                }
                result += "</tr>";
            }
            else
            {
                foreach (string item in TableRowMeans)
                    result += item + ";";
                result += Environment.NewLine;
                foreach (string item in TableRowUrls)
                    result += item + ";";
            }

            return result;
        }

        /////////////////
        ///Часть абстрактного класса
        ///
        abstract public override bool Equals(object obj);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
