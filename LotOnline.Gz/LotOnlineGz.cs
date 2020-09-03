using IAuction;
using System;

namespace LotOnline.Gz
{
    [Serializable]
    public class LotOnlineGz : ATorg
    {
        public string baseUrl { get; private set; }
        public string Organisator { get; private set; }
        public string Status { get; private set; }
        public string Type { get; private set; }
        public string Note { get; private set; }
        public LotOnlineGz(Procedure inpItem, string baseUrl = "https://gz.lot-online.ru")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    this.baseUrl = baseUrl;
            LotNumberStr = inpItem.PurchaseNumber;
            LotNameStr = inpItem.PurchaseObjectInfo;
            LotNameUrl = "etp_front/procedure/view/procedure/common/" + LotNumberStr;
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
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString(bool html)
        {
            throw new NotImplementedException();
        }
    }
}
