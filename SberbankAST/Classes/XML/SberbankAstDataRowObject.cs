using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SberbankAST
{
    /*
    [XmlRoot(ElementName = "total")]
    public class Total
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "relation")]
        public string Relation { get; set; }
    }
    */

    /*
    [XmlRoot(ElementName = "_source")]
    public class _source
    {
        [XmlElement(ElementName = "BranchNameNew")]
        public string BranchNameNew { get; set; }
        [XmlElement(ElementName = "purchStateName")]
        public string PurchStateName { get; set; }
        [XmlElement(ElementName = "RequestDate")]
        public string RequestDate { get; set; }
        [XmlElement(ElementName = "auResProcessedMoney")]
        public string AuResProcessedMoney { get; set; }
        [XmlElement(ElementName = "Need2Part4Cust")]
        public string Need2Part4Cust { get; set; }
        [XmlElement(ElementName = "ToAS")]
        public string ToAS { get; set; }
        [XmlElement(ElementName = "purchVersion")]
        public string PurchVersion { get; set; }
        [XmlElement(ElementName = "purchIsExplained")]
        public string PurchIsExplained { get; set; }
        [XmlElement(ElementName = "products")]
        public string Products { get; set; }
        [XmlElement(ElementName = "purchType")]
        public string PurchType { get; set; }
        [XmlElement(ElementName = "PurchaseStageTerm")]
        public string PurchaseStageTerm { get; set; }
        [XmlElement(ElementName = "ProtocolCount")]
        public string ProtocolCount { get; set; }
        [XmlElement(ElementName = "purchCoverAmount")]
        public string PurchCoverAmount { get; set; }
        [XmlElement(ElementName = "purchAmountRUB")]
        public string PurchAmountRUB { get; set; }
        [XmlElement(ElementName = "OrgKpp")]
        public string OrgKpp { get; set; }
        [XmlElement(ElementName = "IsSMP")]
        public string IsSMP { get; set; }
        [XmlElement(ElementName = "PublicMonthTerm")]
        public string PublicMonthTerm { get; set; }
        [XmlElement(ElementName = "prot1ProcessedMoney")]
        public string Prot1ProcessedMoney { get; set; }
        [XmlElement(ElementName = "ContactPersonTerm")]
        public string ContactPersonTerm { get; set; }
        [XmlElement(ElementName = "AuctResultsNumber")]
        public string AuctResultsNumber { get; set; }
        [XmlElement(ElementName = "AuctionEndDate")]
        public string AuctionEndDate { get; set; }
        [XmlElement(ElementName = "AuctionBeginDate")]
        public string AuctionBeginDate { get; set; }
        [XmlElement(ElementName = "isPurchCostDetails")]
        public string IsPurchCostDetails { get; set; }
        [XmlElement(ElementName = "BranchPath")]
        public string BranchPath { get; set; }
        [XmlElement(ElementName = "purchPriceStepMax")]
        public string PurchPriceStepMax { get; set; }
        [XmlElement(ElementName = "TradeSectionId")]
        public string TradeSectionId { get; set; }
        [XmlElement(ElementName = "purchUpdateAt")]
        public string PurchUpdateAt { get; set; }
        [XmlElement(ElementName = "objectHrefTerm")]
        public string ObjectHrefTerm { get; set; }
        [XmlElement(ElementName = "purchLastState")]
        public string PurchLastState { get; set; }
        [XmlElement(ElementName = "purchCurrency")]
        public string PurchCurrency { get; set; }
        [XmlElement(ElementName = "totalRequestCount")]
        public string TotalRequestCount { get; set; }
        [XmlElement(ElementName = "orgContacts")]
        public string OrgContacts { get; set; }
        [XmlElement(ElementName = "OfferHistoryCnt")]
        public List<string> OfferHistoryCnt { get; set; }
        [XmlElement(ElementName = "OrgNickName")]
        public string OrgNickName { get; set; }
        [XmlElement(ElementName = "PublicDateText")]
        public string PublicDateText { get; set; }
        [XmlElement(ElementName = "purchCreateAt")]
        public string PurchCreateAt { get; set; }
        [XmlElement(ElementName = "CustomerInnKppHash")]
        public List<string> CustomerInnKppHash { get; set; }
        [XmlElement(ElementName = "PurchaseWayTerm")]
        public string PurchaseWayTerm { get; set; }
        [XmlElement(ElementName = "needPayment")]
        public string NeedPayment { get; set; }
        [XmlElement(ElementName = "purchState")]
        public string PurchState { get; set; }
        [XmlElement(ElementName = "OrgInnKpp")]
        public string OrgInnKpp { get; set; }
        [XmlElement(ElementName = "CustomerFullName")]
        public List<string> CustomerFullName { get; set; }
        [XmlElement(ElementName = "prot2Date")]
        public string Prot2Date { get; set; }
        [XmlElement(ElementName = "auctionItems")]
        public string AuctionItems { get; set; }
        [XmlElement(ElementName = "purchIsByPreferenced")]
        public string PurchIsByPreferenced { get; set; }
        [XmlElement(ElementName = "OrgName")]
        public string OrgName { get; set; }
        [XmlElement(ElementName = "prot2Count")]
        public string Prot2Count { get; set; }
        [XmlElement(ElementName = "PublicQuoterTerm")]
        public string PublicQuoterTerm { get; set; }
        [XmlElement(ElementName = "purchCode")]
        public string PurchCode { get; set; }
        [XmlElement(ElementName = "AnswerCount")]
        public string AnswerCount { get; set; }
        [XmlElement(ElementName = "isSecurity")]
        public string IsSecurity { get; set; }
        [XmlElement(ElementName = "purchIsChanged")]
        public string PurchIsChanged { get; set; }
        [XmlElement(ElementName = "EndDate")]
        public string EndDate { get; set; }
        [XmlElement(ElementName = "productDictionary")]
        public List<string> ProductDictionary { get; set; }
        [XmlElement(ElementName = "OrgBuID")]
        public string OrgBuID { get; set; }
        [XmlElement(ElementName = "BranchNameSourceId")]
        public string BranchNameSourceId { get; set; }
        [XmlElement(ElementName = "Prot1Number")]
        public string Prot1Number { get; set; }
        [XmlElement(ElementName = "BranchName")]
        public string BranchName { get; set; }
        [XmlElement(ElementName = "CustomerKpp")]
        public List<string> CustomerKpp { get; set; }
        [XmlElement(ElementName = "CustomerNickName")]
        public List<string> CustomerNickName { get; set; }
        [XmlElement(ElementName = "PurchaseTypeNameTerm")]
        public string PurchaseTypeNameTerm { get; set; }
        [XmlElement(ElementName = "purchStateChangedReason")]
        public string PurchStateChangedReason { get; set; }
        [XmlElement(ElementName = "BranchNameSource")]
        public string BranchNameSource { get; set; }
        [XmlElement(ElementName = "IsInArchive")]
        public string IsInArchive { get; set; }
        [XmlElement(ElementName = "RegionId")]
        public string RegionId { get; set; }
        [XmlElement(ElementName = "Prot2Number")]
        public string Prot2Number { get; set; }
        [XmlElement(ElementName = "purchIsRuPreferenced")]
        public string PurchIsRuPreferenced { get; set; }
        [XmlElement(ElementName = "prot2ProcessedMoney")]
        public string Prot2ProcessedMoney { get; set; }
        [XmlElement(ElementName = "IsSMPTerm")]
        public string IsSMPTerm { get; set; }
        [XmlElement(ElementName = "RequestStartDate")]
        public string RequestStartDate { get; set; }
        [XmlElement(ElementName = "ContrDetails")]
        public string ContrDetails { get; set; }
        [XmlElement(ElementName = "BranchDictionaryNew")]
        public string BranchDictionaryNew { get; set; }
        [XmlElement(ElementName = "purchCoverAmountInPercent")]
        public string PurchCoverAmountInPercent { get; set; }
        [XmlElement(ElementName = "productNames")]
        public List<string> ProductNames { get; set; }
        [XmlElement(ElementName = "IsCancelByFas")]
        public string IsCancelByFas { get; set; }
        [XmlElement(ElementName = "purchBranchId")]
        public string PurchBranchId { get; set; }
        [XmlElement(ElementName = "OrgInn")]
        public string OrgInn { get; set; }
        [XmlElement(ElementName = "auctionProducts")]
        public string AuctionProducts { get; set; }
        [XmlElement(ElementName = "OrgInnKppHash")]
        public string OrgInnKppHash { get; set; }
        [XmlElement(ElementName = "purchStateNameTerm")]
        public string PurchStateNameTerm { get; set; }
        [XmlElement(ElementName = "productId")]
        public List<string> ProductId { get; set; }
        [XmlElement(ElementName = "AuctionSource")]
        public string AuctionSource { get; set; }
        [XmlElement(ElementName = "BranchCodeNew")]
        public string BranchCodeNew { get; set; }
        [XmlElement(ElementName = "purchCodeTerm")]
        public string PurchCodeTerm { get; set; }
        [XmlElement(ElementName = "CustomerInn")]
        public List<string> CustomerInn { get; set; }
        [XmlElement(ElementName = "lastChange")]
        public string LastChange { get; set; }
        [XmlElement(ElementName = "purchCreateBy")]
        public string PurchCreateBy { get; set; }
        [XmlElement(ElementName = "SourceTerm")]
        public string SourceTerm { get; set; }
        [XmlElement(ElementName = "prot1Date")]
        public string Prot1Date { get; set; }
        [XmlElement(ElementName = "purchDescription")]
        public string PurchDescription { get; set; }
        [XmlElement(ElementName = "CreateRequestHrefTerm")]
        public string CreateRequestHrefTerm { get; set; }
        [XmlElement(ElementName = "purchAddonExist")]
        public string PurchAddonExist { get; set; }
        [XmlElement(ElementName = "ASID")]
        public string ASID { get; set; }
        [XmlElement(ElementName = "isShared")]
        public string IsShared { get; set; }
        [XmlElement(ElementName = "purchOKDP")]
        public string PurchOKDP { get; set; }
        [XmlElement(ElementName = "requestCount")]
        public string RequestCount { get; set; }
        [XmlElement(ElementName = "purchPreference")]
        public string PurchPreference { get; set; }
        [XmlElement(ElementName = "CreateRequestAlowed")]
        public string CreateRequestAlowed { get; set; }
        [XmlElement(ElementName = "BranchNameTerm")]
        public string BranchNameTerm { get; set; }
        [XmlElement(ElementName = "contrcoveramount")]
        public string Contrcoveramount { get; set; }
        [XmlElement(ElementName = "PurchaseTypeName")]
        public string PurchaseTypeName { get; set; }
        [XmlElement(ElementName = "OrgFullName")]
        public string OrgFullName { get; set; }
        [XmlElement(ElementName = "purchquantity")]
        public string Purchquantity { get; set; }
        [XmlElement(ElementName = "purchPriceStepMin")]
        public string PurchPriceStepMin { get; set; }
        [XmlElement(ElementName = "CustBuId")]
        public List<string> CustBuId { get; set; }
        [XmlElement(ElementName = "purchStateChangedAt")]
        public string PurchStateChangedAt { get; set; }
        [XmlElement(ElementName = "CustomerInnKpp")]
        public List<string> CustomerInnKpp { get; set; }
        [XmlElement(ElementName = "ES_TimeStamp")]
        public string ES_TimeStamp { get; set; }
        [XmlElement(ElementName = "StopType")]
        public string StopType { get; set; }
        [XmlElement(ElementName = "productPath")]
        public List<string> ProductPath { get; set; }
        [XmlElement(ElementName = "productCodes")]
        public List<string> ProductCodes { get; set; }
        [XmlElement(ElementName = "IsBlocked")]
        public string IsBlocked { get; set; }
        [XmlElement(ElementName = "RegionName")]
        public string RegionName { get; set; }
        [XmlElement(ElementName = "PublicDate")]
        public string PublicDate { get; set; }
        [XmlElement(ElementName = "prot1Count")]
        public string Prot1Count { get; set; }
        [XmlElement(ElementName = "Prot2PlanDate")]
        public string Prot2PlanDate { get; set; }
        [XmlElement(ElementName = "BranchFullId")]
        public string BranchFullId { get; set; }
        [XmlElement(ElementName = "RegionNameTerm")]
        public string RegionNameTerm { get; set; }
        [XmlElement(ElementName = "purchStateInt")]
        public string PurchStateInt { get; set; }
        [XmlElement(ElementName = "SourceHrefTerm")]
        public string SourceHrefTerm { get; set; }
        [XmlElement(ElementName = "BranchNameSourceTerm")]
        public string BranchNameSourceTerm { get; set; }
        [XmlElement(ElementName = "purchAmount")]
        public string PurchAmount { get; set; }
        [XmlElement(ElementName = "isIncrease")]
        public string IsIncrease { get; set; }
        [XmlElement(ElementName = "OOSHref")]
        public string OOSHref { get; set; }
        [XmlElement(ElementName = "contrcoveramountinpercent")]
        public string Contrcoveramountinpercent { get; set; }
        [XmlElement(ElementName = "OfferCnt")]
        public string OfferCnt { get; set; }
        [XmlElement(ElementName = "PurchaseGroupTerm")]
        public string PurchaseGroupTerm { get; set; }
        [XmlElement(ElementName = "ContrNeed")]
        public string ContrNeed { get; set; }
        [XmlElement(ElementName = "ForBuId")]
        public string ForBuId { get; set; }
        [XmlElement(ElementName = "purchName")]
        public string PurchName { get; set; }
        [XmlElement(ElementName = "BidName")]
        public string BidName { get; set; }
        [XmlElement(ElementName = "auctResultDate")]
        public string AuctResultDate { get; set; }
        [XmlElement(ElementName = "purchID")]
        public string PurchID { get; set; }
        [XmlElement(ElementName = "LastActionDt")]
        public string LastActionDt { get; set; }
        [XmlElement(ElementName = "RequestAcceptDate")]
        public string RequestAcceptDate { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }
        [XmlElement(ElementName = "CustBuID")]
        public string CustBuID { get; set; }
        [XmlElement(ElementName = "OrgKPP")]
        public string OrgKPP { get; set; }
        [XmlElement(ElementName = "auctResultsdocID")]
        public string AuctResultsdocID { get; set; }
        [XmlElement(ElementName = "isExistsAuctResult")]
        public string IsExistsAuctResult { get; set; }
        [XmlElement(ElementName = "isExistsProt1")]
        public string IsExistsProt1 { get; set; }
        [XmlElement(ElementName = "isExistsProt2")]
        public string IsExistsProt2 { get; set; }
        [XmlElement(ElementName = "prot1docID")]
        public string Prot1docID { get; set; }
        [XmlElement(ElementName = "prot2docID")]
        public string Prot2docID { get; set; }
        [XmlElement(ElementName = "purchReq1docID")]
        public string PurchReq1docID { get; set; }
        [XmlElement(ElementName = "purchReq2docID")]
        public string PurchReq2docID { get; set; }
    }
    */

    /*
    [XmlRoot(ElementName = "highlight")]    
    public class Highlight
    {
        [XmlElement(ElementName = "purchName")]
        public string PurchName { get; set; }
        [XmlElement(ElementName = "BidName")]
        public string BidName { get; set; }
        [XmlElement(ElementName = "OrgName")]
        public string OrgName { get; set; }
    }
    */

    /*
    [XmlRoot(ElementName = "hits")]
    public class Hits
    {
        [XmlElement(ElementName = "_index")]
        public string _index { get; set; }
        [XmlElement(ElementName = "_type")]
        public string _type { get; set; }
        [XmlElement(ElementName = "_id")]
        public string _id { get; set; }
        [XmlElement(ElementName = "_score")]
        public string _score { get; set; }
        [XmlElement(ElementName = "_source")]
        public _source _source { get; set; }
        [XmlElement(ElementName = "highlight")]
        public Highlight Highlight { get; set; }
    }
    */

    /*
    [XmlRoot(ElementName = "datarow")]
    public class Datarow
    {
        [XmlElement(ElementName = "total")]
        public Total Total { get; set; }
        [XmlElement(ElementName = "max_score")]
        public string Max_score { get; set; }
        [XmlElement(ElementName = "hits")]
        public List<Hits> Hits { get; set; }
    }
    */
}

