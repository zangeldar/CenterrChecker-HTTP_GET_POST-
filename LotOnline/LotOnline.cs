using IAuction;
using System;

namespace LotOnline
{
    [Serializable]
    public class LotOnline : ATorg
    {
        public string baseUrl { get; private set; }

        public string Address { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Organisator { get; private set; }
        public string DateStart { get; private set; }
        public LotOnline(JsonRow inpRow, string baseUrl = "https://lot-online.ru/")
        {
            if (baseUrl != null)
                if (baseUrl != "")
                    this.baseUrl = baseUrl;
            this.LotNumberStr = inpRow.Id;
            this.PriceStart = inpRow.Price;
            this.LotNameStr = inpRow.ShortDescription;

            this.Address = inpRow.Address;
            this.Code = inpRow.Code;
            this.Description = inpRow.FullDescription;
            this.Organisator = inpRow.Organization;
            this.DateStart = inpRow.PublicationDate;

            this.LotNameUrl = baseUrl + "viewfoundobject.html?id=" + LotNumberStr;
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
