using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SberbankAST
{    
    [XmlRoot(ElementName = "hits")]
    public class Hit
    {
        [JsonProperty("_index")]
        [XmlElement(ElementName = "_index")]
        public string _index;

        [JsonProperty("_type")]
        [XmlElement(ElementName = "_type")]
        public string _type;
        
        [JsonProperty("_id")]
        [XmlElement(ElementName = "_id")]
        public string _id;

        [JsonProperty("_score")]
        [XmlElement(ElementName = "_score")]
        public string _score;

        [JsonProperty("_source")]
        [XmlElement(ElementName = "_source")]
        public Source Source;

        [JsonProperty("highlight")]
        [XmlElement(ElementName = "highlight")]
        public Highlight Highlight;
    }
    

    [XmlRoot(ElementName = "_source")]
    public class Source
    {       
        public Source()
        {
            Products = new List<object>();
            CustomerInnKppHash = new List<string>();
            CustomerFullNames = new List<string>();
            ProductDictionary = new List<string>();
            CustomerKpp = new List<string>();
            CustomerNickName = new List<string>();
            ProductNames = new List<string>();
            ProductId = new List<string>();
            CustomerInn = new List<string>();
            SourceCustBuId = new List<long>();
            CustomerInnKpp = new List<string>();
            ProductPath = new List<string>();
            ProductCodes = new List<string>();
            CustBuId = new List<long>();

            /*
            Products.Add(null);
            CustomerInnKppHash.Add("");
            CustomerFullNames.Add("");
            ProductDictionary.Add("");
            CustomerKpp.Add("");
            CustomerNickName.Add("");
            ProductNames.Add("");
            ProductId.Add("");
            CustomerInn.Add("");
            SourceCustBuId = new List<long>();
            CustomerInnKpp.Add("");
            ProductPath.Add("");
            ProductCodes.Add("");
            CustBuId.Add(0);
            */
        }

        [XmlElement(ElementName = "BranchNameNew")]
        [JsonProperty("BranchNameNew")]
        public string BranchNameNew { get; set; }

        [XmlElement(ElementName = "purchStateName")]
        [JsonProperty("purchStateName")]
        public string PurchStateName { get; set; }  //enum

        [XmlElement(ElementName = "RequestDate")]
        [JsonProperty("RequestDate")]
        public string RequestDate { get; set; } //date

        [XmlElement(ElementName = "auResProcessedMoney", IsNullable = true)]
        [JsonProperty("auResProcessedMoney", NullValueHandling = NullValueHandling.Ignore)]
        public string AuResProcessedMoney { get; set; }
        //public bool? AuResProcessedMoney { get; set; }
        // was bool?

        [XmlElement(ElementName = "Need2Part4Cust")]
        [JsonProperty("Need2Part4Cust")]
        public bool Need2Part4Cust { get; set; }

        [XmlElement(ElementName = "ToAS")]
        [JsonProperty("ToAS")]
        public int ToAs { get; set; }

        [XmlElement(ElementName = "purchVersion")]
        [JsonProperty("purchVersion")]
        public int PurchVersion { get; set; }

        [XmlElement(ElementName = "purchIsExplained")]
        [JsonProperty("purchIsExplained")]
        public bool PurchIsExplained { get; set; }

        [XmlElement(ElementName = "products")]
        public object Product { get { if (Products.Count > 0) return Products[0]; else return ""; } set{ if (Products.Count > 0) Products[0] = value; else Products.Add(value); } }  //null
        [JsonProperty("products")]
        //public object[] Products { get; set; }  //null
        public List<object> Products { get; set; }  //null
        
        [XmlElement(ElementName = "purchType")]
        [JsonProperty("purchType")]
        public string PurchType { get; set; }

        [XmlElement(ElementName = "PurchaseStageTerm")]
        [JsonProperty("PurchaseStageTerm")]
        public string PurchaseStageTerm { get; set; }

        [XmlElement(ElementName = "ProtocolCount")]
        [JsonProperty("ProtocolCount")]
        public int ProtocolCount { get; set; }

        [XmlElement(ElementName = "purchCoverAmount")]
        [JsonProperty("purchCoverAmount")]
        public double PurchCoverAmount { get; set; }

        [XmlElement(ElementName = "purchAmountRUB")]
        [JsonProperty("purchAmountRUB")]
        public double PurchAmountRub { get; set; }

        [XmlElement(ElementName = "OrgKpp")]
        [JsonProperty("OrgKpp")]       
        public string SourceOrgKpp { get; set; }

        [XmlElement(ElementName = "IsSMP")]
        [JsonProperty("IsSMP")]
        public bool IsSmp { get; set; }

        [XmlElement(ElementName = "PublicMonthTerm")]
        [JsonProperty("PublicMonthTerm")]
        public string PublicMonthTerm { get; set; }

        [XmlElement(ElementName = "prot1ProcessedMoney", IsNullable = true)]
        [JsonProperty("prot1ProcessedMoney", NullValueHandling = NullValueHandling.Ignore)]
        public string Prot1ProcessedMoney { get; set; }
        //public bool? Prot1ProcessedMoney { get; set; }
        // was bool?

        [XmlElement(ElementName = "ContactPersonTerm")]
        [JsonProperty("ContactPersonTerm")]
        public string ContactPersonTerm { get; set; }

        [XmlElement(ElementName = "AuctResultsNumber")]
        [JsonProperty("AuctResultsNumber")]
        public string AuctResultsNumber { get; set; }

        [XmlElement(ElementName = "AuctionEndDate")]
        [JsonProperty("AuctionEndDate")]
        public string AuctionEndDate { get; set; }

        [XmlElement(ElementName = "AuctionBeginDate")]
        [JsonProperty("AuctionBeginDate")]
        public string AuctionBeginDate { get; set; }

        [XmlElement(ElementName = "isPurchCostDetails")]
        [JsonProperty("isPurchCostDetails")]
        public bool IsPurchCostDetails { get; set; }

        [XmlElement(ElementName = "BranchPath")]
        [JsonProperty("BranchPath")]
        public string BranchPath { get; set; }

        [XmlElement(ElementName = "purchPriceStepMax")]
        [JsonProperty("purchPriceStepMax")]
        public double PurchPriceStepMax { get; set; }

        [XmlElement(ElementName = "TradeSectionId")]
        [JsonProperty("TradeSectionId")]
        public int TradeSectionId { get; set; }

        [XmlElement(ElementName = "purchUpdateAt")]
        [JsonProperty("purchUpdateAt")]
        public string PurchUpdateAt { get; set; }

        [XmlElement(ElementName = "objectHrefTerm")]
        [JsonProperty("objectHrefTerm")]
        public string ObjectHrefTerm { get; set; }

        [XmlElement(ElementName = "purchLastState")]
        [JsonProperty("purchLastState")]
        public string PurchLastState { get; set; }

        [XmlElement(ElementName = "purchCurrency")]
        [JsonProperty("purchCurrency")]
        public string PurchCurrency { get; set; }

        [XmlElement(ElementName = "totalRequestCount")]
        [JsonProperty("totalRequestCount")]
        public long TotalRequestCount { get; set; }

        [XmlElement(ElementName = "orgContacts")]
        [JsonProperty("orgContacts")]
        public string OrgContacts { get; set; }

        [XmlElement(ElementName = "OfferHistoryCnt")]
        [JsonProperty("OfferHistoryCnt")]
        public long OfferHistoryCnt { get; set; }

        [XmlElement(ElementName = "OrgNickName")]
        [JsonProperty("OrgNickName")]
        public string OrgNickName { get; set; }

        [XmlElement(ElementName = "PublicDateText")]
        [JsonProperty("PublicDateText")]
        public string PublicDateText { get; set; }

        [XmlElement(ElementName = "purchCreateAt")]
        [JsonProperty("purchCreateAt")]
        public string PurchCreateAt { get; set; }

        [XmlElement(ElementName = "CustomerInnKppHash")]
        [JsonProperty("CustomerInnKppHash")]
        //public string[] CustomerInnKppHash { get; set; }
        public List<string> CustomerInnKppHash { get; set; }

        [XmlElement(ElementName = "PurchaseWayTerm")]
        [JsonProperty("PurchaseWayTerm")]
        public string PurchaseWayTerm { get; set; }

        [XmlElement(ElementName = "needPayment")]
        [JsonProperty("needPayment")]
        public long NeedPayment { get; set; }

        [XmlElement(ElementName = "purchState")]
        [JsonProperty("purchState")]
        public string PurchState { get; set; }

        [XmlElement(ElementName = "OrgInnKpp")]
        [JsonProperty("OrgInnKpp")]
        public string OrgInnKpp { get; set; }

        [XmlElement(ElementName = "CustomerFullName")]
        [JsonProperty("CustomerFullName")]
        public List<string> CustomerFullNames { get; set; }

        [XmlElement(ElementName = "prot2Date")]
        [JsonProperty("prot2Date")]
        public string Prot2Date { get; set; }

        [XmlElement(ElementName = "auctionItems")]
        [JsonProperty("auctionItems")]
        public object AuctionItems { get; set; }

        [XmlElement(ElementName = "purchIsByPreferenced")]
        [JsonProperty("purchIsByPreferenced")]
        public bool PurchIsByPreferenced { get; set; }

        [XmlElement(ElementName = "OrgName")]
        [JsonProperty("OrgName")]
        public string OrgName { get; set; }

        [XmlElement(ElementName = "prot2Count", IsNullable = false)]
        [JsonProperty("prot2Count", NullValueHandling = NullValueHandling.Ignore)]
        public string Prot2Count { get; set; }
        //public long Prot2Count { get; set; }
        //was long?

        [XmlElement(ElementName = "PublicQuoterTerm")]
        [JsonProperty("PublicQuoterTerm")]
        public string PublicQuoterTerm { get; set; }

        [XmlElement(ElementName = "purchCode")]
        [JsonProperty("purchCode")]
        public string PurchCode { get; set; }

        [XmlElement(ElementName = "AnswerCount")]
        [JsonProperty("AnswerCount")]
        public long AnswerCount { get; set; }

        [XmlElement(ElementName = "@version")]
        [JsonProperty("@version")]       
        public string Version { get; set; }

        [XmlElement(ElementName = "isSecurity")]
        [JsonProperty("isSecurity")]
        public bool IsSecurity { get; set; }

        [XmlElement(ElementName = "purchIsChanged")]
        [JsonProperty("purchIsChanged")]
        public bool PurchIsChanged { get; set; }

        [XmlElement(ElementName = "EndDate")]
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }

        [XmlElement(ElementName = "productDictionary")]
        [JsonProperty("productDictionary")]
        public List<string> ProductDictionary { get; set; }

        [XmlElement(ElementName = "OrgBuID")]
        [JsonProperty("OrgBuID")]
        public long OrgBuId { get; set; }

        [XmlElement(ElementName = "BranchNameSourceId")]
        [JsonProperty("BranchNameSourceId")]
        public long BranchNameSourceId { get; set; }

        [XmlElement(ElementName = "Prot1Number")]
        [JsonProperty("Prot1Number")]
        public string Prot1Number { get; set; }

        [XmlElement(ElementName = "BranchName")]
        [JsonProperty("BranchName")]
        public string BranchName { get; set; }

        [XmlElement(ElementName = "CustomerKpp")]
        [JsonProperty("CustomerKpp")]        
        public List<string> CustomerKpp { get; set; }

        [XmlElement(ElementName = "CustomerNickName")]
        [JsonProperty("CustomerNickName")]
        public List<string> CustomerNickName { get; set; }

        [XmlElement(ElementName = "PurchaseTypeNameTerm")]
        [JsonProperty("PurchaseTypeNameTerm")]
        public string PurchaseTypeNameTerm { get; set; }

        [XmlElement(ElementName = "purchStateChangedReason")]
        [JsonProperty("purchStateChangedReason")]
        public object PurchStateChangedReason { get; set; }

        [XmlElement(ElementName = "BranchNameSource")]
        [JsonProperty("BranchNameSource")]
        public string BranchNameSource { get; set; }

        [XmlElement(ElementName = "IsInArchive")]
        [JsonProperty("IsInArchive")]
        public long IsInArchive { get; set; }

        [XmlElement(ElementName = "RegionId")]
        [JsonProperty("RegionId")]
        public long RegionId { get; set; }

        [XmlElement(ElementName = "Prot2Number")]
        [JsonProperty("Prot2Number")]
        public string Prot2Number { get; set; }

        [XmlElement(ElementName = "purchIsRuPreferenced")]
        [JsonProperty("purchIsRuPreferenced")]
        public bool PurchIsRuPreferenced { get; set; }

        [XmlElement(ElementName = "prot2ProcessedMoney", IsNullable = true)]
        [JsonProperty("prot2ProcessedMoney", NullValueHandling = NullValueHandling.Ignore)]
        public string Prot2ProcessedMoney { get; set; }
        //public bool? Prot2ProcessedMoney { get; set; }

        [XmlElement(ElementName = "IsSMPTerm")]
        [JsonProperty("IsSMPTerm")]
        public string IsSmpTerm { get; set; }

        [XmlElement(ElementName = "@timestamp", IsNullable = true)]
        [JsonProperty("@timestamp", NullValueHandling = NullValueHandling.Ignore)]        
        public DateTimeOffset? Timestamp { get; set; }

        [XmlElement(ElementName = "RequestStartDate")]
        [JsonProperty("RequestStartDate")]
        public string RequestStartDate { get; set; }

        [XmlElement(ElementName = "ContrDetails")]
        [JsonProperty("ContrDetails")]
        public string ContrDetails { get; set; }

        [XmlElement(ElementName = "BranchDictionaryNew")]
        [JsonProperty("BranchDictionaryNew")]
        public string BranchDictionaryNew { get; set; }

        [XmlElement(ElementName = "purchCoverAmountInPercent")]
        [JsonProperty("purchCoverAmountInPercent")]
        public long PurchCoverAmountInPercent { get; set; }

        [XmlElement(ElementName = "productNames")]
        [JsonProperty("productNames")]
        public List<string> ProductNames { get; set; }

        [XmlElement(ElementName = "IsCancelByFas")]
        [JsonProperty("IsCancelByFas")]
        public bool IsCancelByFas { get; set; }

        [XmlElement(ElementName = "purchBranchId")]
        [JsonProperty("purchBranchId")]
        public long PurchBranchId { get; set; }

        [XmlElement(ElementName = "OrgInn")]
        [JsonProperty("OrgInn")]
        public string OrgInn { get; set; }

        [XmlElement(ElementName = "auctionProducts")]
        [JsonProperty("auctionProducts")]
        public object AuctionProducts { get; set; }

        [XmlElement(ElementName = "OrgInnKppHash")]
        [JsonProperty("OrgInnKppHash")]
        public string OrgInnKppHash { get; set; }

        [XmlElement(ElementName = "purchStateNameTerm")]
        [JsonProperty("purchStateNameTerm")]
        public string PurchStateNameTerm { get; set; }

        [XmlElement(ElementName = "productId")]
        [JsonProperty("productId")]
        public List<string> ProductId { get; set; }

        [XmlElement(ElementName = "AuctionSource")]
        [JsonProperty("AuctionSource")]
        public long AuctionSource { get; set; }

        [XmlElement(ElementName = "BranchCodeNew")]
        [JsonProperty("BranchCodeNew")]        
        public string BranchCodeNew { get; set; }

        [XmlElement(ElementName = "purchCodeTerm")]
        [JsonProperty("purchCodeTerm")]
        public string PurchCodeTerm { get; set; }

        [XmlElement(ElementName = "CustomerInn")]
        [JsonProperty("CustomerInn")]
        public List<string> CustomerInn { get; set; }

        [XmlElement(ElementName = "lastChange")]
        [JsonProperty("lastChange")]
        public string LastChange { get; set; }

        [XmlElement(ElementName = "purchCreateBy")]
        [JsonProperty("purchCreateBy")]
        public long PurchCreateBy { get; set; }

        [XmlElement(ElementName = "SourceTerm")]
        [JsonProperty("SourceTerm")]
        public string SourceTerm { get; set; }

        [XmlElement(ElementName = "prot1Date")]
        [JsonProperty("prot1Date")]
        public string Prot1Date { get; set; }

        [XmlElement(ElementName = "purchDescription")]
        [JsonProperty("purchDescription")]
        public string PurchDescription { get; set; }

        [XmlElement(ElementName = "CreateRequestHrefTerm")]
        [JsonProperty("CreateRequestHrefTerm")]
        public string CreateRequestHrefTerm { get; set; }

        [XmlElement(ElementName = "purchAddonExist")]
        [JsonProperty("purchAddonExist")]
        public bool PurchAddonExist { get; set; }

        [XmlElement(ElementName = "ASID")]
        [JsonProperty("ASID")]
        public string Asid { get; set; }

        [XmlElement(ElementName = "isShared")]
        [JsonProperty("isShared")]
        public bool IsShared { get; set; }

        [XmlElement(ElementName = "purchOKDP")]
        [JsonProperty("purchOKDP")]
        public string PurchOkdp { get; set; }

        [XmlElement(ElementName = "requestCount")]
        [JsonProperty("requestCount")]
        public long RequestCount { get; set; }

        [XmlElement(ElementName = "purchPreference")]
        [JsonProperty("purchPreference")]
        public long PurchPreference { get; set; }

        [XmlElement(ElementName = "CreateRequestAlowed")]
        [JsonProperty("CreateRequestAlowed")]
        public long CreateRequestAlowed { get; set; }

        [XmlElement(ElementName = "BranchNameTerm")]        
        [JsonProperty("BranchNameTerm")]
        public string BranchNameTerm { get; set; }

        [XmlElement(ElementName = "contrcoveramount")]
        [JsonProperty("contrcoveramount")]
        public long Contrcoveramount { get; set; }

        [XmlElement(ElementName = "PurchaseTypeName")]
        [JsonProperty("PurchaseTypeName")]
        public string PurchaseTypeName { get; set; }

        [XmlElement(ElementName = "OrgFullName")]
        [JsonProperty("OrgFullName")]
        public string OrgFullName { get; set; }

        [XmlElement(ElementName = "purchquantity")]
        [JsonProperty("purchquantity")]
        public string Purchquantity { get; set; }

        [XmlElement(ElementName = "purchPriceStepMin")]
        [JsonProperty("purchPriceStepMin")]
        public double PurchPriceStepMin { get; set; }

        [XmlElement(ElementName = "CustBuId")]
        [JsonProperty("CustBuId")]
        public List<long> SourceCustBuId { get; set; }

        [XmlElement(ElementName = "purchStateChangedAt")]
        [JsonProperty("purchStateChangedAt")]
        public object PurchStateChangedAt { get; set; }

        [XmlElement(ElementName = "CustomerInnKpp")]
        [JsonProperty("CustomerInnKpp")]
        public List<string> CustomerInnKpp { get; set; }

        [XmlElement(ElementName = "ES_TimeStamp")]
        [JsonProperty("ES_TimeStamp")]
        public long EsTimeStamp { get; set; }

        [XmlElement(ElementName = "StopType")]
        [JsonProperty("StopType")]
        public object StopType { get; set; }

        [XmlElement(ElementName = "productPath")]
        [JsonProperty("productPath")]
        public List<string> ProductPath { get; set; }

        [XmlElement(ElementName = "productCodes")]
        [JsonProperty("productCodes")]
        public List<string> ProductCodes { get; set; }

        [XmlElement(ElementName = "IsBlocked", IsNullable = true)]
        [JsonProperty("IsBlocked", NullValueHandling = NullValueHandling.Ignore)]
        public string IsBlocked { get; set; }
        //public bool? IsBlocked { get; set; }
        // was bool?

        [XmlElement(ElementName = "RegionName")]
        [JsonProperty("RegionName")]
        public string RegionName { get; set; }

        [XmlElement(ElementName = "PublicDate")]
        [JsonProperty("PublicDate")]
        public string PublicDate { get; set; }

        [XmlElement(ElementName = "prot1Count", IsNullable = false)]
        [JsonProperty("prot1Count", NullValueHandling = NullValueHandling.Ignore)]
        public string Prot1Count { get; set; }
        //public long Prot1Count { get; set; }

        [XmlElement(ElementName = "Prot2PlanDate")]
        [JsonProperty("Prot2PlanDate")]
        public string Prot2PlanDate { get; set; }

        [XmlElement(ElementName = "BranchFullId")]
        [JsonProperty("BranchFullId")]
        public string BranchFullId { get; set; }

        [XmlElement(ElementName = "RegionNameTerm")]
        [JsonProperty("RegionNameTerm")]
        public string RegionNameTerm { get; set; }

        [XmlElement(ElementName = "purchStateInt")]
        [JsonProperty("purchStateInt")]
        public long PurchStateInt { get; set; }

        [XmlElement(ElementName = "SourceHrefTerm")]
        [JsonProperty("SourceHrefTerm")]
        public string SourceHrefTerm { get; set; }

        [XmlElement(ElementName = "BranchNameSourceTerm")]
        [JsonProperty("BranchNameSourceTerm")]
        public string BranchNameSourceTerm { get; set; }

        [XmlElement(ElementName = "purchAmount")]
        [JsonProperty("purchAmount")]
        public double PurchAmount { get; set; }

        [XmlElement(ElementName = "isIncrease")]
        [JsonProperty("isIncrease")]
        public bool IsIncrease { get; set; }

        [XmlElement(ElementName = "OOSHref")]
        [JsonProperty("OOSHref")]
        public string OosHref { get; set; }

        [XmlElement(ElementName = "contrcoveramountinpercent")]
        [JsonProperty("contrcoveramountinpercent")]
        public long Contrcoveramountinpercent { get; set; }

        [XmlElement(ElementName = "OfferCnt")]
        [JsonProperty("OfferCnt")]
        public long OfferCnt { get; set; }

        [XmlElement(ElementName = "PurchaseGroupTerm")]
        [JsonProperty("PurchaseGroupTerm")]
        public string PurchaseGroupTerm { get; set; }

        [XmlElement(ElementName = "ContrNeed")]
        [JsonProperty("ContrNeed")]
        public bool ContrNeed { get; set; }

        [XmlElement(ElementName = "ForBuId", IsNullable = false)]
        [JsonProperty("ForBuId", NullValueHandling = NullValueHandling.Ignore)]
        public string ForBuId { get; set; }
        //public long ForBuId { get; set; }
        // was long?

        [XmlElement(ElementName = "purchName")]
        [JsonProperty("purchName")]
        public string PurchName { get; set; }

        [XmlElementAttribute(ElementName = "BidName")]
        [JsonPropertyAttribute("BidName")]
        public string BidNameSt { get; set; }

        [XmlElement(ElementName = "auctResultDate")]
        [JsonProperty("auctResultDate")]
        public string AuctResultDate { get; set; }

        [XmlElement(ElementName = "purchID")]
        [JsonProperty("purchID")]
        public long PurchId { get; set; }

        [XmlElement(ElementName = "LastActionDt")]
        [JsonProperty("LastActionDt")]
        public string LastActionDt { get; set; }

        [XmlElement(ElementName = "RequestAcceptDate")]
        [JsonProperty("RequestAcceptDate")]
        public string RequestAcceptDate { get; set; }

        [XmlElement(ElementName = "CustBuID", IsNullable = false)]
        [JsonProperty("CustBuID", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> CustBuId { get; set; }

        [XmlElement(ElementName = "OrgKPP")]
        [JsonProperty("OrgKPP")]        
        public string OrgKpp { get; set; }

        [XmlElement(ElementName = "auctResultsdocID", IsNullable = false)]
        [JsonProperty("auctResultsdocID", NullValueHandling = NullValueHandling.Ignore)]
        public long AuctResultsdocId { get; set; }
        //was long?

        [XmlElement(ElementName = "isExistsAuctResult", IsNullable = true)]
        [JsonProperty("isExistsAuctResult", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExistsAuctResult { get; set; }

        [XmlElement(ElementName = "isExistsProt1", IsNullable = true)]
        [JsonProperty("isExistsProt1", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExistsProt1 { get; set; }

        [XmlElement(ElementName = "isExistsProt2", IsNullable = true)]
        [JsonProperty("isExistsProt2", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExistsProt2 { get; set; }

        [XmlElement(ElementName = "offerHistoryCnt", IsNullable = false)]
        [JsonProperty("offerHistoryCnt", NullValueHandling = NullValueHandling.Ignore)]
        public long SourceOfferHistoryCnt { get; set; }
        //was long?

        [XmlElement(ElementName = "prot1docID", IsNullable = false)]
        [JsonProperty("prot1docID", NullValueHandling = NullValueHandling.Ignore)]
        public long Prot1DocId { get; set; }
        //was long?

        [XmlElement(ElementName = "prot2docID", IsNullable = false)]
        [JsonProperty("prot2docID", NullValueHandling = NullValueHandling.Ignore)]
        public long Prot2DocId { get; set; }
        //was long?

        [XmlElement(ElementName = "purchReq1docID", IsNullable = false)]
        [JsonProperty("purchReq1docID", NullValueHandling = NullValueHandling.Ignore)]
        public long PurchReq1DocId { get; set; }
        //was long?

        [XmlElement(ElementName = "purchReq2docID", IsNullable = false)]
        [JsonProperty("purchReq2docID", NullValueHandling = NullValueHandling.Ignore)]
        public long PurchReq2DocId { get; set; }
        //was long?
    }

    [XmlRoot(ElementName = "highlight")]
    public class Highlight
    {
        public Highlight()
        {
            PurchNames = new List<string>();            
            BidNams = new List<string>();
            OrgNams = new List<string>();

            /*
            PurchNames.Add("");
            BidNames.Add("");
            OrgNames.Add("");
            */
        }

        [XmlElement(ElementName = "purchName")]
        public string PurchName { get { if (OrgNams.Count > 0) return OrgNams[0]; else return ""; } set { if (OrgNams.Count > 0) OrgNams[0] = value; else OrgNams.Add(value); } }

        [JsonProperty("purchName")]
        public List<string> PurchNames { get; set; }

        [XmlElement(ElementName = "BidName")]
        public string BidNam { get { if (BidNams.Count > 0) return BidNams[0]; else return ""; } set { if (BidNams.Count > 0) BidNams[0] = value; else BidNams.Add(value); } }        

        [JsonProperty("BidName")]        
        public List<string> BidNams { get; set; }
        //public string[] BidNames { get; set; }

        [XmlElement(ElementName = "OrgName")]
        public string OrgNam { get { if (OrgNams.Count > 0) return OrgNams[0]; else return ""; } set { if (OrgNams.Count > 0) OrgNams[0] = value; else OrgNams.Add(value); } }
                
        [JsonProperty("OrgName")]
        public List<string> OrgNams { get; set; }
    }

    //JSON public partial class Hits
    //[JsonObject("hits")]
    [XmlRoot(ElementName = "datarow")]
    public class MyDataRow
    {
        [JsonProperty("total")]
        [XmlElement(ElementName = "total")]
        public Total Total { get; set; }

        [JsonProperty("max_score", NullValueHandling = NullValueHandling.Ignore)]
        [XmlElement(ElementName = "max_score", IsNullable = false)]
        public string Max_score { get; set; }
        //public long Max_score { get; set; }
        // was long?

        [JsonProperty("hits")]
        [XmlElement(ElementName = "hits")]
        public List<Hit> Hits { get; set; }
    }

    [XmlRoot(ElementName = "total")]
    public class Total
    {
        [JsonProperty("value")]
        [XmlElement(ElementName = "value")]
        public long Value { get; set; }

        [JsonProperty("relation")]
        [XmlElement(ElementName = "relation")]
        public string Relation { get; set; }
    }
}
