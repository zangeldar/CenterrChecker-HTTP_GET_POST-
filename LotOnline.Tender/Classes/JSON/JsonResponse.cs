using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Tender
{
    [Serializable]
    public partial class JsonResponse
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("list")]
        public List[] List { get; set; }
    }

    [Serializable]
    public partial class List
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("demandsCount")]
        public long DemandsCount { get; set; }

        [JsonProperty("okdp2")]
        public string[] Okdp2 { get; set; }

        [JsonProperty("gdStartDate")]
        public string GdStartDate { get; set; }

        [JsonProperty("lotNumber")]
        public long LotNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("viewDemandDate")]
        public string ViewDemandDate { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("gdEndDate")]
        public string GdEndDate { get; set; }

        [JsonProperty("participant")]
        public bool Participant { get; set; }

        [JsonProperty("summationDate")]
        public string SummationDate { get; set; }

        [JsonProperty("features")]
        public string[] Features { get; set; }

        [JsonProperty("placementDate")]
        public string PlacementDate { get; set; }

        [JsonProperty("offerLink")]
        public string OfferLink { get; set; }

        [JsonProperty("organizer")]
        public Organizer Organizer { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("placementType")]
        public string PlacementType { get; set; }

        [JsonProperty("winnerPrice")]
        public string WinnerPrice { get; set; }

        [JsonProperty("deposit")]
        public string Deposit { get; set; }

        [JsonProperty("lotLink")]
        public string LotLink { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("customer")]
        public Organizer[] Customer { get; set; }

        [JsonProperty("regionCodes", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RegionCodes { get; set; }
    }

    [Serializable]
    public partial class Organizer
    {
        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            return "[" + Inn + "] " + Title;
        }

    }

    [Serializable]
    public partial class State
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }    
}
