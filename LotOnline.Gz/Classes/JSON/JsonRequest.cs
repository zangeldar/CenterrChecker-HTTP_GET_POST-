using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotOnline.Gz
{
    [Serializable]
    public partial class JsonRequest
    {
        public JsonRequest (string searchStr="")
        {
            Manager = "sphinx";
            Entity = "Procedure";
            Alias = "procedure";
                       
            Limit = 10;
            Offset = 0;

            Fields = new string[]
            {
                "procedure.id",
                "procedure.number",
                "procedure.lotId",
                "procedure.purchaseNumber",
                "procedure.purchaseObjectInfo",
                "procedure.status",
                "procedure.placerFullName",
                "procedure.placerGuid",
                "procedure.responsibleGuid",
                "procedure.responsibleFIO",
                "procedure.maxSum",
                "procedure.type",
                "procedure.isMB44330",
                "procedure.isMP44",
            };

            Rules = new string[]
            {
                "Procedure.Registry",
            };

            Conditions = new Conditions(searchStr);
            Sort = new Sort();
        }
        [JsonProperty("manager")]
        public string Manager { get; set; }

        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("fields")]
        public string[] Fields { get; set; }

        [JsonProperty("conditions")]
        public Conditions Conditions { get; set; }

        [JsonProperty("rules")]
        public string[] Rules { get; set; }

        [JsonProperty("sort")]
        public Sort Sort { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }

    [Serializable]
    public partial class Conditions
    {
        public Conditions(string searchStr="")
        {
            ProcedureId = new string[]
            {
                "gt",
                "0",
            };
            Asterisk = new string[]
            {
                "match",
                searchStr,
            };
        }

        [JsonProperty("procedure.id")]
        public string[] ProcedureId { get; set; }

        [JsonProperty("*")]
        public string[] Asterisk { get; set; }
    }

    [Serializable]
    public partial class Sort
    {
        public Sort()
        {
            ProcedurePublicationDateTime = "DESC";
        }
        [JsonProperty("procedure.publicationDateTime")]
        public string ProcedurePublicationDateTime { get; set; }
    }

}
