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
            if (myReq.SiteURL != null)
                if (myReq.SiteURL != "")
                    this.baseUrl = myReq.SiteURL;
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
            if (LastError != null)
                return LastError.Message;
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
        public override bool Equals(object obj)
        {
            if (!(obj is ATorg))
                return false;
            ATorg curObj = (ATorg)obj;

            for (int i = 0; i < TableRowMeans.Length; i++)
                if (TableRowMeans[i] != curObj.TableRowMeans[i])
                    return false;
            for (int i = 0; i < TableRowUrls.Length; i++)
                if (TableRowUrls[i] != curObj.TableRowUrls[i])
                    return false;

            return true;
        }
        public override int GetHashCode()
        {
            var hashCode = 198564345;
            hashCode = hashCode * -1521134295 + base.GetHashCode();

            foreach (string item in TableRowMeans)
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(item);

            foreach (string item in TableRowUrls)
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(item);

            /*
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organisator);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);
            */
            return hashCode;

            //return base.GetHashCode();
        }
    }
}
