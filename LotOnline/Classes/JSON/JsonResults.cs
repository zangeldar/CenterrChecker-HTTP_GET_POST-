using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public class JsonResults
    {
        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("records")]        
        public string Records { get; set; }

        [JsonProperty("rows")]
        public JsonRow[] Rows { get; set; }
    }

    [Serializable]
    public class JsonRow
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("publicationDate")]
        public string PublicationDate { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("fullDescription")]
        public string FullDescription { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("organizationLogo")]
        public object OrganizationLogo { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("photoUrl")]
        public string PhotoUrl { get; set; }
    }
}
