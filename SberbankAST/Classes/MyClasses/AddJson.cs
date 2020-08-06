using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SberbankAST
{
    [Serializable]
    public class JsonResponseData
    {
        [JsonProperty("took")]
        public long Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("_shards")]
        public Shards Shards { get; set; }

        [JsonProperty("hits")]
        public MyDataRow Hits { get; set; }

        [JsonProperty("aggregations")]
        public Aggregations Aggregations { get; set; }
    }

    [Serializable]
    public class Aggregations
    {
        [JsonProperty("DistinctOrgs")]
        public DistinctOrgs DistinctOrgs { get; set; }

        [JsonProperty("Branch")]
        public Branch Branch { get; set; }

        [JsonProperty("TotalSum")]
        public DistinctOrgs TotalSum { get; set; }

        [JsonProperty("Times")]
        public Branch Times { get; set; }

        [JsonProperty("Region")]
        public Branch Region { get; set; }

        [JsonProperty("Stage")]
        public Branch Stage { get; set; }

        [JsonProperty("Sources")]
        public Branch Sources { get; set; }
    }

    [Serializable]
    public class Branch
    {
        [JsonProperty("doc_count_error_upper_bound")]
        public long DocCountErrorUpperBound { get; set; }

        [JsonProperty("sum_other_doc_count")]
        public long SumOtherDocCount { get; set; }

        [JsonProperty("buckets")]
        public Bucket[] Buckets { get; set; }
    }

    [Serializable]
    public class Bucket
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("doc_count")]
        public long DocCount { get; set; }

        [JsonProperty("price_sums")]
        public DistinctOrgs PriceSums { get; set; }
    }

    [Serializable]
    public class DistinctOrgs
    {
        [JsonProperty("value")]
        public double Value { get; set; }
    }

    [Serializable]
    public class Shards
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("successful")]
        public long Successful { get; set; }

        [JsonProperty("skipped")]
        public long Skipped { get; set; }

        [JsonProperty("failed")]
        public long Failed { get; set; }
    }
}
