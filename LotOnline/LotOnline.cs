using IAuction;
using System;
using System.Collections.Generic;

namespace LotOnline
{
    [Serializable]
    public class LotOnline : ATorg
    {
        //public string baseUrl { get; private set; }
        public string Source { get; private set; }

        public string Address { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Organisator { get; private set; }
        public string DateStart { get; private set; }
        public LotOnline(JsonRow inpRow, IRequest myReq):base(myReq)
        {     
            /*
            if (myReq.ServiceURL != null)
                if (myReq.ServiceURL != "")
                    this.baseUrl = myReq.ServiceURL;
            */
            this.Source = myReq.SiteName;
            this.LotNumberStr = inpRow.Id;
            this.PriceStart = inpRow.Price;
            this.LotNameStr = inpRow.ShortDescription;

            this.Address = inpRow.Address;
            this.Code = inpRow.Code;
            this.Description = inpRow.FullDescription;
            this.Organisator = inpRow.Organization;
            this.DateStart = inpRow.PublicationDate;

            this.LotNameUrl = baseUrl + "viewfoundobject.html?id=" + LotNumberStr;

            Means = new string[]
            {
                LotNumberStr,
                LotNameStr,                
                PriceStart,
                Organisator,
                Address,
                DateStart,
                Description,
                Source
            };
            
            Urls = new string[Means.Length];
            Urls = new string[]
            {
                LotNameUrl,
                LotNameUrl,
                "",
                "",
                "",
                "",
                "",
                baseUrl
            };
        }

        private string[] Means;
        private string[] Urls;

        public override bool Equals(object obj)
        {
            if (!(obj is LotOnline))
                return false;
            LotOnline curObj = (LotOnline)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.Address == curObj.Address &
                this.Code == curObj.Code &
                this.Description == curObj.Description &
                this.Organisator == curObj.Organisator &
                this.DateStart == curObj.DateStart &
                this.LotNumberStr == curObj.LotNumberStr &
                this.PriceStart == curObj.PriceStart)
                return true;

            return false;
        }

        public override string ToString(bool html)
        {
            string result = "";
                       
            if (html)
            {
                result = "<tr>";
                for (int i = 0; i < Means.Length; i++)
                {
                    if (Urls[i] != null & Urls[i] != "")
                        result += @"<td><a href=""" + Urls[i] + @""">" + Means[i] + @"</a></td>";
                    else
                        result += @"<td>" + Means[i] + "</td>";
                }
                result += "</tr>";
            }
            else
            {
                foreach (string item in Means)
                    result += item + ";";
                result += Environment.NewLine;
                foreach (string item in Urls)
                    result += item + ";";
            }

            return result;
        }

        public override int GetHashCode()
        {
            var hashCode = -1042135788;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);

            foreach (string item in Means)
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(item);

            /*
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);

            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organisator);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateStart);
            */

            internalID = hashCode.ToString();

            return hashCode;
        }
    }
}
