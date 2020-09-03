using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Gz
{
    [Serializable]
    public partial class JsonResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    [Serializable]
    public partial class Data
    {
        [JsonProperty("entities")]
        public Entity[] Entities { get; set; }
    }

    [Serializable]
    public partial class Entity
    {
        [JsonProperty("procedure")]
        public Procedure Procedure { get; set; }
    }

    [Serializable]
    public partial class Procedure
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("lotId")]
        public string LotId { get; set; }

        [JsonProperty("purchaseNumber")]
        public string PurchaseNumber { get; set; }

        [JsonProperty("purchaseObjectInfo")]
        public string PurchaseObjectInfo { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("placerFullName")]
        public string PlacerFullName { get; set; }

        [JsonProperty("placerGuid")]
        public string PlacerGuid { get; set; }

        [JsonProperty("responsibleGuid")]
        public string ResponsibleGuid { get; set; }

        [JsonProperty("responsibleFIO")]
        public string ResponsibleFio { get; set; }

        [JsonProperty("maxSum")]
        public string MaxSum { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isMB44330")]
        public bool IsMb44330 { get; set; }

        [JsonProperty("isMP44")]
        public bool IsMp44 { get; set; }
    }

}
