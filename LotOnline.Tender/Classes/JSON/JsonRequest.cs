using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Tender
{
    [Serializable]
    public partial class JsonRequest
    {
        public JsonRequest(string searchStr = "")
        {
            this.Query = new Query(searchStr);
            this.Filter = new Filter();
            this.Sort = new Sort();
        }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("filter")]
        public Filter Filter { get; set; }

        [JsonProperty("sort")]
        public Sort Sort { get; set; }

        [JsonProperty("limit")]
        public Limit Limit { get; set; }
    }

    [Serializable]
    public partial class Filter
    {
        public Filter()
        {
            State = new string[]
            {
                "ALL",
            };
        }

        [JsonProperty("state")]
        public string[] State { get; set; }
    }

    [Serializable]
    public partial class Query
    {
        public Query(string searchStr = "")
        {
            this.Title = searchStr;
            this.Types = new string[]
            {
                "BUYING",
                "SALE",
                "RFI",
                "SMALL_PURCHASE",
            };
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    [Serializable]
    public partial class Sort
    {
        public Sort()
        {
            this.PlacementDate = false;
        }

        [JsonProperty("placementDate")]
        public bool PlacementDate { get; set; }
    }

    [Serializable]
    public partial class Limit
    {
        public Limit()
        {
            this.Min = 0;
            this.Max = 20;
            this.UpdateTotalCount = true;
        }

        [JsonProperty("min")]
        public int Min { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("updateTotalCount")]
        public bool UpdateTotalCount { get; set; }
    }
}
