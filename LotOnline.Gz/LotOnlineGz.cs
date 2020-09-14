using IAuction;
using System;
using System.Collections.Generic;

namespace LotOnline.Gz
{
    [Serializable]
    public class LotOnlineGz : ATorg
    {
        //public string baseUrl { get; private set; }
        public string Organisator { get; private set; }
        public string Status { get; private set; }
        public string Type { get; private set; }
        public string Note { get; private set; }
        //public LotOnlineGz(Procedure inpItem, string baseUrl = "https://gz.lot-online.ru")
        public LotOnlineGz(Procedure inpItem, IRequest myReq) : base(myReq)
        {
            /*
            if (baseUrl != null)
                if (baseUrl != "")
                    this.baseUrl = baseUrl;
            */
            LotNumberStr = inpItem.PurchaseNumber;
            LotNameStr = inpItem.PurchaseObjectInfo;
            LotNameUrl = this.baseUrl + "etp_front/procedure/view/procedure/common/" + LotNumberStr;
            PriceStart = inpItem.MaxSum;

            Organisator = inpItem.PlacerFullName;
            
            Status = inpItem.Status;
            switch (Status)
            {
                case "procedure.published":
                    Status = "Прием заявок";
                    break;
                case "procedure.contract":
                    Status = "Заключение контракта";
                    break;
                case "procedure.failed":
                    Status = "Не состоялась";
                    break;
                case "procedure.finished":
                    Status = "Завершена";
                    break;
                default:
                    break;
            }

            Type = inpItem.Type;
            switch (Type)
            {
                case "EA44":
                    Type = "44ФЗ";
                    break;
                default:
                    break;
            }

            Note = "";
            string sep = " | ";
            if (inpItem.IsMb44330)
                Note += "Только СМП/СОНО" + sep;
            if (inpItem.IsMp44)
                Note += "Преимущество СМП/СОНО" + sep;
            Note = Note.Remove(Note.LastIndexOf(sep));

            TableRowMeans = new string[]
            {
                LotNumberStr,
                LotNameStr,
                Organisator,
                PriceStart,
                Status,
                Type,
                Note
            };
            TableRowUrls = new string[TableRowMeans.Length];
            TableRowUrls = new string[]
            {
                "",
                LotNameUrl,
                "",
                "",
                "",
                "",
                ""
            };
        }

        /*
        protected string[] Means;
        protected string[] Urls;
        */

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            if (!(obj is LotOnlineGz))
                return false;
            LotOnlineGz curObj = (LotOnlineGz)obj;

            for (int i = 0; i < TableRowMeans.Length; i++)
                if (TableRowMeans[i] != curObj.TableRowMeans[i])
                    return false;
            for (int i = 0; i < TableRowUrls.Length; i++)
                if (TableRowUrls[i] != curObj.TableRowUrls[i])
                    return false;

            return true;
        }

        /*
        public override string ToString(bool html)
        {
            //throw new NotImplementedException();
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
        */

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
        }
    }
}
