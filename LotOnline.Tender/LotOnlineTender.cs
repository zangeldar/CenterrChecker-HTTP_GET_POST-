using IAuction;
using System;
using System.Collections.Generic;

namespace LotOnline.Tender
{
    [Serializable]
    public class LotOnlineTender:ATorg
    {
        //public string baseUrl { get; private set; }

        public string TorgNumberStr { get; private set; }
        public string TorgNumberUrl { get; private set; }
        public string Organizer { get; private set; }
        public string DateStart { get; private set; }
        public string DateFinish { get; private set; }
        public string DateSummation { get; private set; }
        public string DateDemand { get; private set; }
        public string DatePlacement { get; private set; }
        public string Section { get; private set; }
        public string Type { get; private set; }
        public string Status { get; private set; }

        public string DemandCounts { get; private set; }
        public string Deposit { get; private set; }
        public string Participant { get; private set; }
        public string UUID { get; private set; }
        public string WinnerPrice { get; private set; }

        public string Customer { get; private set; }
        public string Note { get; private set; }
        public string Okdp2 { get; private set; }
        public string RegionCodes { get; private set; }        

        public LotOnlineTender(List inpItem, IRequest myReq) : base(myReq)
        {
            //this.baseUrl = baseUrl;

            LotNameStr = inpItem.Title;
            LotNameUrl = inpItem.LotLink;
            PriceStart = inpItem.Price;
            /*
            {
                PriceStart = PriceStart.Replace("<br/>", " ");
                PriceStart = PriceStart.Replace("<br/>", " ");
                PriceStart = PriceStart.Replace("<br/>", " ");

            }*/

            LotNumberStr = inpItem.LotNumber.ToString();
            TorgNumberStr = inpItem.Identifier;
            TorgNumberUrl = inpItem.OfferLink;
            Organizer = inpItem.Organizer.Title + " [" + inpItem.Organizer.Inn + "]";
            Organizer = inpItem.Organizer.ToString();
            DateStart = inpItem.GdStartDate;
            DateFinish = inpItem.GdEndDate;
            DateSummation = inpItem.SummationDate;
            DateDemand = inpItem.ViewDemandDate;
            DatePlacement = inpItem.PlacementDate;
            Type = inpItem.PlacementType;
            Section = inpItem.Type;                                             //  BUYING ; 
            Status = inpItem.State.Title + " [" + inpItem.State.Code + "]";

            DemandCounts = inpItem.DemandsCount.ToString();
            Deposit = inpItem.Deposit;
            Participant = inpItem.Participant.ToString();
            UUID = inpItem.Uuid;
            WinnerPrice = inpItem.WinnerPrice;

            if (inpItem.Customer.Length > 0)
            {
                List<string> tmpList = new List<string>();
                foreach (Organizer item in inpItem.Customer)
                    tmpList.Add(item.ToString());

                Customer = SFileIO.ArrayToString(tmpList, " | ");
            }

            if (inpItem.Features.Length > 0)
            {
                Note = SFileIO.ArrayToString(inpItem.Features, " | ");
            }

            if (inpItem.Okdp2.Length > 0)
            {
                Okdp2 = SFileIO.ArrayToString(inpItem.Okdp2, " | ");
            }

            if (RegionCodes != null)
                if (RegionCodes.Length > 0)
                {
                    RegionCodes = SFileIO.ArrayToString(inpItem.RegionCodes, " | ");
                }

            TableRowMeans = new string[]
            {
                TorgNumberStr,
                LotNameStr,
                LotNumberStr,
                Organizer,
                Customer,
                RegionCodes,
                PriceStart,
                Deposit,
                DatePlacement,
                DateDemand,
                DateStart,
                DateFinish,
                DateSummation,
                Status,
                Type,
                Section,
                Note,
                Okdp2,
                DemandCounts,
                Participant,
                WinnerPrice,
                UUID
            };

            TableRowUrls = new string[]
            {
                TorgNumberUrl,
                LotNameUrl,
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            };
        }

        
        public override bool Equals(object obj)
        {
            if (!(obj is LotOnlineTender))
                return false;

            return base.Equals(obj);
        }

        /*
        public override string ToString(bool html)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            var hashCode = -1478811981;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgNumberUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organizer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateSummation);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateDemand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DatePlacement);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DemandCounts);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Deposit);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Participant);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UUID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WinnerPrice);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Customer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Okdp2);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RegionCodes);
            return hashCode;
        }
        */
    }
}
