using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SberbankAST
{
    /*
    public partial class JsonResponseData
    {
        [JsonProperty("took")]
        public long Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("_shards")]
        public Shards Shards { get; set; }

        [JsonProperty("hits")]
        public Hits Hits { get; set; }

        [JsonProperty("aggregations")]
        public Aggregations Aggregations { get; set; }
    }

    public partial class Aggregations
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

    public partial class Branch
    {
        [JsonProperty("doc_count_error_upper_bound")]
        public long DocCountErrorUpperBound { get; set; }

        [JsonProperty("sum_other_doc_count")]
        public long SumOtherDocCount { get; set; }

        [JsonProperty("buckets")]
        public Bucket[] Buckets { get; set; }
    }

    public partial class Bucket
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("doc_count")]
        public long DocCount { get; set; }

        [JsonProperty("price_sums")]
        public DistinctOrgs PriceSums { get; set; }
    }

    public partial class DistinctOrgs
    {
        [JsonProperty("value")]
        public double Value { get; set; }
    }
    */

        /*
    public partial class Hits
    {
        [JsonProperty("total")]
        public Total Total { get; set; }

        [JsonProperty("max_score")]
        public double MaxScore { get; set; }

        [JsonProperty("hits")]
        public Hit[] HitsHits { get; set; }
    }

    public partial class Hit
    {
        [JsonProperty("_index")]
        public Index Index { get; set; }

        [JsonProperty("_type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("_score")]
        public double Score { get; set; }

        [JsonProperty("_source")]
        public Source Source { get; set; }

        [JsonProperty("highlight")]
        public Highlight Highlight { get; set; }
    }

    public partial class Highlight
    {
        [JsonProperty("purchName")]
        public string[] PurchName { get; set; }

        [JsonProperty("BidName")]
        public string[] BidName { get; set; }

        [JsonProperty("OrgName", NullValueHandling = NullValueHandling.Ignore)]
        public string[] OrgName { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("BranchNameNew")]
        public BranchNameNew? BranchNameNew { get; set; }

        [JsonProperty("purchStateName")]
        public Purch PurchStateName { get; set; }

        [JsonProperty("RequestDate")]
        public string RequestDate { get; set; }

        [JsonProperty("auResProcessedMoney")]
        public bool? AuResProcessedMoney { get; set; }

        [JsonProperty("Need2Part4Cust")]
        public bool Need2Part4Cust { get; set; }

        [JsonProperty("ToAS")]
        public long ToAs { get; set; }

        [JsonProperty("purchVersion")]
        public long PurchVersion { get; set; }

        [JsonProperty("purchIsExplained")]
        public bool PurchIsExplained { get; set; }

        [JsonProperty("products")]
        public object[] Products { get; set; }

        [JsonProperty("purchType")]
        public PurchType PurchType { get; set; }

        [JsonProperty("PurchaseStageTerm")]
        public Purch PurchaseStageTerm { get; set; }

        [JsonProperty("ProtocolCount")]
        public long ProtocolCount { get; set; }

        [JsonProperty("purchCoverAmount")]
        public double PurchCoverAmount { get; set; }

        [JsonProperty("purchAmountRUB")]
        public double PurchAmountRub { get; set; }

        [JsonProperty("OrgKpp", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SourceOrgKpp { get; set; }

        [JsonProperty("IsSMP")]
        public bool IsSmp { get; set; }

        [JsonProperty("PublicMonthTerm")]
        public string PublicMonthTerm { get; set; }

        [JsonProperty("prot1ProcessedMoney")]
        public bool? Prot1ProcessedMoney { get; set; }

        [JsonProperty("ContactPersonTerm")]
        public string ContactPersonTerm { get; set; }

        [JsonProperty("AuctResultsNumber")]
        public string AuctResultsNumber { get; set; }

        [JsonProperty("AuctionEndDate")]
        public string AuctionEndDate { get; set; }

        [JsonProperty("AuctionBeginDate")]
        public string AuctionBeginDate { get; set; }

        [JsonProperty("isPurchCostDetails")]
        public bool IsPurchCostDetails { get; set; }

        [JsonProperty("BranchPath")]
        public BranchFullIdEnum BranchPath { get; set; }

        [JsonProperty("purchPriceStepMax")]
        public double PurchPriceStepMax { get; set; }

        [JsonProperty("TradeSectionId")]
        public long TradeSectionId { get; set; }

        [JsonProperty("purchUpdateAt")]
        public string PurchUpdateAt { get; set; }

        [JsonProperty("objectHrefTerm")]
        public Uri ObjectHrefTerm { get; set; }

        [JsonProperty("purchLastState")]
        public string PurchLastState { get; set; }

        [JsonProperty("purchCurrency")]
        public PurchCurrency PurchCurrency { get; set; }

        [JsonProperty("totalRequestCount")]
        public long TotalRequestCount { get; set; }

        [JsonProperty("orgContacts")]
        public string OrgContacts { get; set; }

        [JsonProperty("OfferHistoryCnt")]
        public long OfferHistoryCnt { get; set; }

        [JsonProperty("OrgNickName")]
        public string OrgNickName { get; set; }

        [JsonProperty("PublicDateText")]
        public string PublicDateText { get; set; }

        [JsonProperty("purchCreateAt")]
        public string PurchCreateAt { get; set; }

        [JsonProperty("CustomerInnKppHash")]
        public string[] CustomerInnKppHash { get; set; }

        [JsonProperty("PurchaseWayTerm")]
        public PurchaseWayTerm PurchaseWayTerm { get; set; }

        [JsonProperty("needPayment")]
        public long NeedPayment { get; set; }

        [JsonProperty("purchState")]
        public PurchState PurchState { get; set; }

        [JsonProperty("OrgInnKpp")]
        public string OrgInnKpp { get; set; }

        [JsonProperty("CustomerFullName")]
        public string[] CustomerFullName { get; set; }

        [JsonProperty("prot2Date")]
        public string Prot2Date { get; set; }

        [JsonProperty("auctionItems")]
        public object AuctionItems { get; set; }

        [JsonProperty("purchIsByPreferenced")]
        public bool PurchIsByPreferenced { get; set; }

        [JsonProperty("OrgName")]
        public string OrgName { get; set; }

        [JsonProperty("prot2Count")]
        public long? Prot2Count { get; set; }

        [JsonProperty("PublicQuoterTerm")]
        public string PublicQuoterTerm { get; set; }

        [JsonProperty("purchCode")]
        public string PurchCode { get; set; }

        [JsonProperty("AnswerCount")]
        public long AnswerCount { get; set; }

        [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Version { get; set; }

        [JsonProperty("isSecurity")]
        public bool IsSecurity { get; set; }

        [JsonProperty("purchIsChanged")]
        public bool PurchIsChanged { get; set; }

        [JsonProperty("EndDate")]
        public string EndDate { get; set; }

        [JsonProperty("productDictionary")]
        public ProductDictionary[] ProductDictionary { get; set; }

        [JsonProperty("OrgBuID")]
        public long OrgBuId { get; set; }

        [JsonProperty("BranchNameSourceId")]
        public long BranchNameSourceId { get; set; }

        [JsonProperty("Prot1Number")]
        public string Prot1Number { get; set; }

        [JsonProperty("BranchName")]
        public BranchName BranchName { get; set; }

        [JsonProperty("CustomerKpp")]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public long[] CustomerKpp { get; set; }

        [JsonProperty("CustomerNickName")]
        public string[] CustomerNickName { get; set; }

        [JsonProperty("PurchaseTypeNameTerm")]
        public Purchase PurchaseTypeNameTerm { get; set; }

        [JsonProperty("purchStateChangedReason")]
        public object PurchStateChangedReason { get; set; }

        [JsonProperty("BranchNameSource")]
        public BranchName BranchNameSource { get; set; }

        [JsonProperty("IsInArchive")]
        public long IsInArchive { get; set; }

        [JsonProperty("RegionId")]
        public long RegionId { get; set; }

        [JsonProperty("Prot2Number")]
        public string Prot2Number { get; set; }

        [JsonProperty("purchIsRuPreferenced")]
        public bool PurchIsRuPreferenced { get; set; }

        [JsonProperty("prot2ProcessedMoney")]
        public bool? Prot2ProcessedMoney { get; set; }

        [JsonProperty("IsSMPTerm")]
        public IsSmpTerm IsSmpTerm { get; set; }

        [JsonProperty("@timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("RequestStartDate")]
        public string RequestStartDate { get; set; }

        [JsonProperty("ContrDetails")]
        public ContrDetails ContrDetails { get; set; }

        [JsonProperty("BranchDictionaryNew")]
        public BranchDictionaryNew BranchDictionaryNew { get; set; }

        [JsonProperty("purchCoverAmountInPercent")]
        public long PurchCoverAmountInPercent { get; set; }

        [JsonProperty("productNames")]
        public string[] ProductNames { get; set; }

        [JsonProperty("IsCancelByFas")]
        public bool IsCancelByFas { get; set; }

        [JsonProperty("purchBranchId")]
        public long PurchBranchId { get; set; }

        [JsonProperty("OrgInn")]
        public string OrgInn { get; set; }

        [JsonProperty("auctionProducts")]
        public object AuctionProducts { get; set; }

        [JsonProperty("OrgInnKppHash")]
        public string OrgInnKppHash { get; set; }

        [JsonProperty("purchStateNameTerm")]
        public Purch PurchStateNameTerm { get; set; }

        [JsonProperty("productId")]
        public string[] ProductId { get; set; }

        [JsonProperty("AuctionSource")]
        public long AuctionSource { get; set; }

        [JsonProperty("BranchCodeNew")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? BranchCodeNew { get; set; }

        [JsonProperty("purchCodeTerm")]
        public string PurchCodeTerm { get; set; }

        [JsonProperty("CustomerInn")]
        public string[] CustomerInn { get; set; }

        [JsonProperty("lastChange")]
        public string LastChange { get; set; }

        [JsonProperty("purchCreateBy")]
        public long PurchCreateBy { get; set; }

        [JsonProperty("SourceTerm")]
        public SourceTerm SourceTerm { get; set; }

        [JsonProperty("prot1Date")]
        public string Prot1Date { get; set; }

        [JsonProperty("purchDescription")]
        public string PurchDescription { get; set; }

        [JsonProperty("CreateRequestHrefTerm")]
        public Uri CreateRequestHrefTerm { get; set; }

        [JsonProperty("purchAddonExist")]
        public bool PurchAddonExist { get; set; }

        [JsonProperty("ASID")]
        public string Asid { get; set; }

        [JsonProperty("isShared")]
        public bool IsShared { get; set; }

        [JsonProperty("purchOKDP")]
        public string PurchOkdp { get; set; }

        [JsonProperty("requestCount")]
        public long RequestCount { get; set; }

        [JsonProperty("purchPreference")]
        public long PurchPreference { get; set; }

        [JsonProperty("CreateRequestAlowed")]
        public long CreateRequestAlowed { get; set; }

        [JsonProperty("BranchNameTerm")]
        public BranchName BranchNameTerm { get; set; }

        [JsonProperty("contrcoveramount")]
        public long Contrcoveramount { get; set; }

        [JsonProperty("PurchaseTypeName")]
        public Purchase PurchaseTypeName { get; set; }

        [JsonProperty("OrgFullName")]
        public string OrgFullName { get; set; }

        [JsonProperty("purchquantity")]
        public string Purchquantity { get; set; }

        [JsonProperty("purchPriceStepMin")]
        public double PurchPriceStepMin { get; set; }

        [JsonProperty("CustBuId")]
        public long[] SourceCustBuId { get; set; }

        [JsonProperty("purchStateChangedAt")]
        public object PurchStateChangedAt { get; set; }

        [JsonProperty("CustomerInnKpp")]
        public string[] CustomerInnKpp { get; set; }

        [JsonProperty("ES_TimeStamp")]
        public long EsTimeStamp { get; set; }

        [JsonProperty("StopType")]
        public object StopType { get; set; }

        [JsonProperty("productPath")]
        public string[] ProductPath { get; set; }

        [JsonProperty("productCodes")]
        public string[] ProductCodes { get; set; }

        [JsonProperty("IsBlocked")]
        public bool? IsBlocked { get; set; }

        [JsonProperty("RegionName")]
        public string RegionName { get; set; }

        [JsonProperty("PublicDate")]
        public string PublicDate { get; set; }

        [JsonProperty("prot1Count")]
        public long? Prot1Count { get; set; }

        [JsonProperty("Prot2PlanDate")]
        public string Prot2PlanDate { get; set; }

        [JsonProperty("BranchFullId")]
        public BranchFullIdEnum BranchFullId { get; set; }

        [JsonProperty("RegionNameTerm")]
        public string RegionNameTerm { get; set; }

        [JsonProperty("purchStateInt")]
        public long PurchStateInt { get; set; }

        [JsonProperty("SourceHrefTerm")]
        public Uri SourceHrefTerm { get; set; }

        [JsonProperty("BranchNameSourceTerm")]
        public BranchName BranchNameSourceTerm { get; set; }

        [JsonProperty("purchAmount")]
        public double PurchAmount { get; set; }

        [JsonProperty("isIncrease")]
        public bool IsIncrease { get; set; }

        [JsonProperty("OOSHref")]
        public Uri OosHref { get; set; }

        [JsonProperty("contrcoveramountinpercent")]
        public long Contrcoveramountinpercent { get; set; }

        [JsonProperty("OfferCnt")]
        public long OfferCnt { get; set; }

        [JsonProperty("PurchaseGroupTerm")]
        public Purchase PurchaseGroupTerm { get; set; }

        [JsonProperty("ContrNeed")]
        public bool ContrNeed { get; set; }

        [JsonProperty("ForBuId")]
        public long? ForBuId { get; set; }

        [JsonProperty("purchName")]
        public string PurchName { get; set; }

        [JsonProperty("BidName")]
        public string BidName { get; set; }

        [JsonProperty("auctResultDate")]
        public string AuctResultDate { get; set; }

        [JsonProperty("purchID")]
        public long PurchId { get; set; }

        [JsonProperty("LastActionDt")]
        public string LastActionDt { get; set; }

        [JsonProperty("RequestAcceptDate")]
        public string RequestAcceptDate { get; set; }

        [JsonProperty("CustBuID", NullValueHandling = NullValueHandling.Ignore)]
        public long[] CustBuId { get; set; }

        [JsonProperty("OrgKPP", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? OrgKpp { get; set; }

        [JsonProperty("auctResultsdocID", NullValueHandling = NullValueHandling.Ignore)]
        public long? AuctResultsdocId { get; set; }

        [JsonProperty("isExistsAuctResult", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExistsAuctResult { get; set; }

        [JsonProperty("isExistsProt1", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExistsProt1 { get; set; }

        [JsonProperty("isExistsProt2", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExistsProt2 { get; set; }

        [JsonProperty("offerHistoryCnt", NullValueHandling = NullValueHandling.Ignore)]
        public long? SourceOfferHistoryCnt { get; set; }

        [JsonProperty("prot1docID", NullValueHandling = NullValueHandling.Ignore)]
        public long? Prot1DocId { get; set; }

        [JsonProperty("prot2docID", NullValueHandling = NullValueHandling.Ignore)]
        public long? Prot2DocId { get; set; }

        [JsonProperty("purchReq1docID", NullValueHandling = NullValueHandling.Ignore)]
        public long? PurchReq1DocId { get; set; }

        [JsonProperty("purchReq2docID", NullValueHandling = NullValueHandling.Ignore)]
        public long? PurchReq2DocId { get; set; }
    }

    public partial class Total
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("relation")]
        public string Relation { get; set; }
    }
    */
    /*
    public partial class Shards
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
    */
    /*
    public enum Index { Purchgos20200207 };

    public enum BranchDictionaryNew { Окпд2 };

    public enum BranchFullIdEnum { The0_208_94256, The0_208_94265, The0_208_94271, The0_208_94277 };

    public enum BranchName { ГорючеСмазочныеМатериалыЭнергоносители, ПродукцияХимическихПроизводств, РазныеПромышленныеИПотребительскиеТовары, УслугиВНепроизводственнойСфере };

    public enum BranchNameNew { ВеществаХимическиеИПродуктыХимические, КоксИНефтепродукты };

    public enum ContrDetails { Empty, КонтрактЗаключен };

    public enum IsSmpTerm { Да, Нет };

    public enum ProductDictionary { Okpd, Okpd2 };

    public enum PurchCurrency { Rub };

    public enum PurchState { Canceled, Closed, NotHeld };

    public enum Purch { Завершено, НеСостоялась, Отменено };

    public enum PurchType { Ef44 };

    public enum Purchase { ЭлектронныйАукцион };

    public enum PurchaseWayTerm { Закупка };

    public enum SourceTerm { ГосзакупкиПо44Фз };

    public enum TypeEnum { Doc };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                IndexConverter.Singleton,
                BranchDictionaryNewConverter.Singleton,
                BranchFullIdEnumConverter.Singleton,
                BranchNameConverter.Singleton,
                BranchNameNewConverter.Singleton,
                ContrDetailsConverter.Singleton,
                IsSmpTermConverter.Singleton,
                PurchaseConverter.Singleton,
                PurchConverter.Singleton,
                PurchaseWayTermConverter.Singleton,
                SourceTermConverter.Singleton,
                ProductDictionaryConverter.Singleton,
                PurchCurrencyConverter.Singleton,
                PurchStateConverter.Singleton,
                PurchTypeConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class IndexConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Index) || t == typeof(Index?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "purchgos20200207")
            {
                return Index.Purchgos20200207;
            }
            throw new Exception("Cannot unmarshal type Index");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Index)untypedValue;
            if (value == Index.Purchgos20200207)
            {
                serializer.Serialize(writer, "purchgos20200207");
                return;
            }
            throw new Exception("Cannot marshal type Index");
        }

        public static readonly IndexConverter Singleton = new IndexConverter();
    }

    internal class BranchDictionaryNewConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BranchDictionaryNew) || t == typeof(BranchDictionaryNew?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "ОКПД2")
            {
                return BranchDictionaryNew.Окпд2;
            }
            throw new Exception("Cannot unmarshal type BranchDictionaryNew");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BranchDictionaryNew)untypedValue;
            if (value == BranchDictionaryNew.Окпд2)
            {
                serializer.Serialize(writer, "ОКПД2");
                return;
            }
            throw new Exception("Cannot marshal type BranchDictionaryNew");
        }

        public static readonly BranchDictionaryNewConverter Singleton = new BranchDictionaryNewConverter();
    }

    internal class BranchFullIdEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BranchFullIdEnum) || t == typeof(BranchFullIdEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "0_208_94256":
                    return BranchFullIdEnum.The0_208_94256;
                case "0_208_94265":
                    return BranchFullIdEnum.The0_208_94265;
                case "0_208_94271":
                    return BranchFullIdEnum.The0_208_94271;
                case "0_208_94277":
                    return BranchFullIdEnum.The0_208_94277;
            }
            throw new Exception("Cannot unmarshal type BranchFullIdEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BranchFullIdEnum)untypedValue;
            switch (value)
            {
                case BranchFullIdEnum.The0_208_94256:
                    serializer.Serialize(writer, "0_208_94256");
                    return;
                case BranchFullIdEnum.The0_208_94265:
                    serializer.Serialize(writer, "0_208_94265");
                    return;
                case BranchFullIdEnum.The0_208_94271:
                    serializer.Serialize(writer, "0_208_94271");
                    return;
                case BranchFullIdEnum.The0_208_94277:
                    serializer.Serialize(writer, "0_208_94277");
                    return;
            }
            throw new Exception("Cannot marshal type BranchFullIdEnum");
        }

        public static readonly BranchFullIdEnumConverter Singleton = new BranchFullIdEnumConverter();
    }

    internal class BranchNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BranchName) || t == typeof(BranchName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Горюче-смазочные материалы, энергоносители":
                    return BranchName.ГорючеСмазочныеМатериалыЭнергоносители;
                case "Продукция химических производств":
                    return BranchName.ПродукцияХимическихПроизводств;
                case "Разные промышленные и потребительские товары":
                    return BranchName.РазныеПромышленныеИПотребительскиеТовары;
                case "Услуги в непроизводственной сфере":
                    return BranchName.УслугиВНепроизводственнойСфере;
            }
            throw new Exception("Cannot unmarshal type BranchName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BranchName)untypedValue;
            switch (value)
            {
                case BranchName.ГорючеСмазочныеМатериалыЭнергоносители:
                    serializer.Serialize(writer, "Горюче-смазочные материалы, энергоносители");
                    return;
                case BranchName.ПродукцияХимическихПроизводств:
                    serializer.Serialize(writer, "Продукция химических производств");
                    return;
                case BranchName.РазныеПромышленныеИПотребительскиеТовары:
                    serializer.Serialize(writer, "Разные промышленные и потребительские товары");
                    return;
                case BranchName.УслугиВНепроизводственнойСфере:
                    serializer.Serialize(writer, "Услуги в непроизводственной сфере");
                    return;
            }
            throw new Exception("Cannot marshal type BranchName");
        }

        public static readonly BranchNameConverter Singleton = new BranchNameConverter();
    }

    internal class BranchNameNewConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BranchNameNew) || t == typeof(BranchNameNew?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Вещества химические и продукты химические":
                    return BranchNameNew.ВеществаХимическиеИПродуктыХимические;
                case "Кокс и нефтепродукты":
                    return BranchNameNew.КоксИНефтепродукты;
            }
            throw new Exception("Cannot unmarshal type BranchNameNew");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BranchNameNew)untypedValue;
            switch (value)
            {
                case BranchNameNew.ВеществаХимическиеИПродуктыХимические:
                    serializer.Serialize(writer, "Вещества химические и продукты химические");
                    return;
                case BranchNameNew.КоксИНефтепродукты:
                    serializer.Serialize(writer, "Кокс и нефтепродукты");
                    return;
            }
            throw new Exception("Cannot marshal type BranchNameNew");
        }

        public static readonly BranchNameNewConverter Singleton = new BranchNameNewConverter();
    }

    internal class ContrDetailsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContrDetails) || t == typeof(ContrDetails?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "-":
                    return ContrDetails.Empty;
                case "Контракт заключен":
                    return ContrDetails.КонтрактЗаключен;
            }
            throw new Exception("Cannot unmarshal type ContrDetails");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContrDetails)untypedValue;
            switch (value)
            {
                case ContrDetails.Empty:
                    serializer.Serialize(writer, "-");
                    return;
                case ContrDetails.КонтрактЗаключен:
                    serializer.Serialize(writer, "Контракт заключен");
                    return;
            }
            throw new Exception("Cannot marshal type ContrDetails");
        }

        public static readonly ContrDetailsConverter Singleton = new ContrDetailsConverter();
    }

    internal class DecodeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long[]);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (long[])untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
    }

    internal class IsSmpTermConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(IsSmpTerm) || t == typeof(IsSmpTerm?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Да":
                    return IsSmpTerm.Да;
                case "Нет":
                    return IsSmpTerm.Нет;
            }
            throw new Exception("Cannot unmarshal type IsSmpTerm");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (IsSmpTerm)untypedValue;
            switch (value)
            {
                case IsSmpTerm.Да:
                    serializer.Serialize(writer, "Да");
                    return;
                case IsSmpTerm.Нет:
                    serializer.Serialize(writer, "Нет");
                    return;
            }
            throw new Exception("Cannot marshal type IsSmpTerm");
        }

        public static readonly IsSmpTermConverter Singleton = new IsSmpTermConverter();
    }

    internal class PurchaseConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Purchase) || t == typeof(Purchase?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Электронный аукцион")
            {
                return Purchase.ЭлектронныйАукцион;
            }
            throw new Exception("Cannot unmarshal type Purchase");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Purchase)untypedValue;
            if (value == Purchase.ЭлектронныйАукцион)
            {
                serializer.Serialize(writer, "Электронный аукцион");
                return;
            }
            throw new Exception("Cannot marshal type Purchase");
        }

        public static readonly PurchaseConverter Singleton = new PurchaseConverter();
    }

    internal class PurchConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Purch) || t == typeof(Purch?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Завершено":
                    return Purch.Завершено;
                case "Не состоялась":
                    return Purch.НеСостоялась;
                case "Отменено":
                    return Purch.Отменено;
            }
            throw new Exception("Cannot unmarshal type Purch");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Purch)untypedValue;
            switch (value)
            {
                case Purch.Завершено:
                    serializer.Serialize(writer, "Завершено");
                    return;
                case Purch.НеСостоялась:
                    serializer.Serialize(writer, "Не состоялась");
                    return;
                case Purch.Отменено:
                    serializer.Serialize(writer, "Отменено");
                    return;
            }
            throw new Exception("Cannot marshal type Purch");
        }

        public static readonly PurchConverter Singleton = new PurchConverter();
    }

    internal class PurchaseWayTermConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurchaseWayTerm) || t == typeof(PurchaseWayTerm?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Закупка")
            {
                return PurchaseWayTerm.Закупка;
            }
            throw new Exception("Cannot unmarshal type PurchaseWayTerm");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurchaseWayTerm)untypedValue;
            if (value == PurchaseWayTerm.Закупка)
            {
                serializer.Serialize(writer, "Закупка");
                return;
            }
            throw new Exception("Cannot marshal type PurchaseWayTerm");
        }

        public static readonly PurchaseWayTermConverter Singleton = new PurchaseWayTermConverter();
    }

    internal class SourceTermConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SourceTerm) || t == typeof(SourceTerm?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Госзакупки по 44-ФЗ")
            {
                return SourceTerm.ГосзакупкиПо44Фз;
            }
            throw new Exception("Cannot unmarshal type SourceTerm");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SourceTerm)untypedValue;
            if (value == SourceTerm.ГосзакупкиПо44Фз)
            {
                serializer.Serialize(writer, "Госзакупки по 44-ФЗ");
                return;
            }
            throw new Exception("Cannot marshal type SourceTerm");
        }

        public static readonly SourceTermConverter Singleton = new SourceTermConverter();
    }

    internal class ProductDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProductDictionary) || t == typeof(ProductDictionary?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "OKPD":
                    return ProductDictionary.Okpd;
                case "OKPD2":
                    return ProductDictionary.Okpd2;
            }
            throw new Exception("Cannot unmarshal type ProductDictionary");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProductDictionary)untypedValue;
            switch (value)
            {
                case ProductDictionary.Okpd:
                    serializer.Serialize(writer, "OKPD");
                    return;
                case ProductDictionary.Okpd2:
                    serializer.Serialize(writer, "OKPD2");
                    return;
            }
            throw new Exception("Cannot marshal type ProductDictionary");
        }

        public static readonly ProductDictionaryConverter Singleton = new ProductDictionaryConverter();
    }

    internal class PurchCurrencyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurchCurrency) || t == typeof(PurchCurrency?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "RUB")
            {
                return PurchCurrency.Rub;
            }
            throw new Exception("Cannot unmarshal type PurchCurrency");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurchCurrency)untypedValue;
            if (value == PurchCurrency.Rub)
            {
                serializer.Serialize(writer, "RUB");
                return;
            }
            throw new Exception("Cannot marshal type PurchCurrency");
        }

        public static readonly PurchCurrencyConverter Singleton = new PurchCurrencyConverter();
    }

    internal class PurchStateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurchState) || t == typeof(PurchState?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "canceled":
                    return PurchState.Canceled;
                case "closed":
                    return PurchState.Closed;
                case "notHeld":
                    return PurchState.NotHeld;
            }
            throw new Exception("Cannot unmarshal type PurchState");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurchState)untypedValue;
            switch (value)
            {
                case PurchState.Canceled:
                    serializer.Serialize(writer, "canceled");
                    return;
                case PurchState.Closed:
                    serializer.Serialize(writer, "closed");
                    return;
                case PurchState.NotHeld:
                    serializer.Serialize(writer, "notHeld");
                    return;
            }
            throw new Exception("Cannot marshal type PurchState");
        }

        public static readonly PurchStateConverter Singleton = new PurchStateConverter();
    }

    internal class PurchTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurchType) || t == typeof(PurchType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "EF-44")
            {
                return PurchType.Ef44;
            }
            throw new Exception("Cannot unmarshal type PurchType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurchType)untypedValue;
            if (value == PurchType.Ef44)
            {
                serializer.Serialize(writer, "EF-44");
                return;
            }
            throw new Exception("Cannot marshal type PurchType");
        }

        public static readonly PurchTypeConverter Singleton = new PurchTypeConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "_doc")
            {
                return TypeEnum.Doc;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.Doc)
            {
                serializer.Serialize(writer, "_doc");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }    
    */
}
