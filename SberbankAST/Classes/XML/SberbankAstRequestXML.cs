using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SberbankAST
{
    [Serializable]
    [XmlRoot(ElementName = "mainSearchBar")]
    public class MainSearchBar
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "minimum_should_match")]
        public string Minimum_should_match { get; set; }
        public MainSearchBar()
        {
            Value = " ";
            Minimum_should_match = "100%";
            Type = "best_fields";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "purchAmount")]
    public class PurchAmount
    {
        [XmlElement(ElementName = "minvalue")]
        public string Minvalue { get; set; }
        [XmlElement(ElementName = "maxvalue")]
        public string Maxvalue { get; set; }
        public PurchAmount()
        {
            Minvalue = " ";
            Maxvalue = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "PublicDate")]
    public class PublicDate
    {
        [XmlElement(ElementName = "minvalue")]
        public string Minvalue { get; set; }
        [XmlElement(ElementName = "maxvalue")]
        public string Maxvalue { get; set; }
        public PublicDate()
        {
            Minvalue = " ";
            Maxvalue = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "PurchaseStageTerm")]
    public class PurchaseStageTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public PurchaseStageTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "SourceTerm")]
    public class SourceTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public SourceTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "RegionNameTerm")]
    public class RegionNameTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public RegionNameTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "RequestStartDate")]
    public class RequestStartDate
    {
        [XmlElement(ElementName = "minvalue")]
        public string Minvalue { get; set; }
        [XmlElement(ElementName = "maxvalue")]
        public string Maxvalue { get; set; }
        public RequestStartDate()
        {
            Minvalue = " ";
            Maxvalue = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "RequestDate")]
    public class RequestDate
    {
        [XmlElement(ElementName = "minvalue")]
        public string Minvalue { get; set; }
        [XmlElement(ElementName = "maxvalue")]
        public string Maxvalue { get; set; }
        public RequestDate()
        {
            Minvalue = " ";
            Maxvalue = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "AuctionBeginDate")]
    public class AuctionBeginDate
    {
        [XmlElement(ElementName = "minvalue")]
        public string Minvalue { get; set; }
        [XmlElement(ElementName = "maxvalue")]
        public string Maxvalue { get; set; }
        public AuctionBeginDate()
        {
            Minvalue = " ";
            Maxvalue = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "okdp2MultiMatch")]
    public class Okdp2MultiMatch
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        public Okdp2MultiMatch()
        {
            Value = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "okdp2tree")]
    public class Okdp2tree
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "productField")]
        public string ProductField { get; set; }
        [XmlElement(ElementName = "branchField")]
        public string BranchField { get; set; }
        public Okdp2tree()
        {
            Value = " ";
            ProductField = " ";
            BranchField = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "classifier")]
    public class Classifier
    {
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public Classifier()
        {
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "orgCondition")]
    public class OrgCondition
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        public OrgCondition()
        {
            Value = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "orgDictionary")]
    public class OrgDictionary
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        public OrgDictionary()
        {
            Value = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "organizator")]
    public class Organizator
    {
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public Organizator()
        {
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "CustomerCondition")]
    public class CustomerCondition
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        public CustomerCondition()
        {
            Value = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "CustomerDictionary")]
    public class CustomerDictionary
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        public CustomerDictionary()
        {
            Value = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "customer")]
    public class Customer
    {
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public Customer()
        {
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "PurchaseWayTerm")]
    public class PurchaseWayTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public PurchaseWayTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "PurchaseTypeNameTerm")]
    public class PurchaseTypeNameTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public PurchaseTypeNameTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "BranchNameTerm")]
    public class BranchNameTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public BranchNameTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "IsSMPTerm")]
    public class IsSMPTerm
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "visiblepart")]
        public string Visiblepart { get; set; }
        public IsSMPTerm()
        {
            Value = " ";
            Visiblepart = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "statistic")]
    public class Statistic
    {
        [XmlElement(ElementName = "totalProc")]
        public string TotalProc { get; set; }
        [XmlElement(ElementName = "TotalSum")]
        public string TotalSum { get; set; }
        [XmlElement(ElementName = "DistinctOrgs")]
        public string DistinctOrgs { get; set; }
        public Statistic()
        {
            TotalProc = " ";
            TotalSum = " ";
            DistinctOrgs = " ";
            /*
            TotalProc = "8 294 048";
            TotalSum = "22.05 Òðëí.";
            DistinctOrgs = "74447";
            */
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "filters")]
    public class Filters
    {
        [XmlElement(ElementName = "mainSearchBar")]
        public MainSearchBar MainSearchBar { get; set; }
        [XmlElement(ElementName = "purchAmount")]
        public PurchAmount PurchAmount { get; set; }
        [XmlElement(ElementName = "PublicDate")]
        public PublicDate PublicDate { get; set; }
        [XmlElement(ElementName = "PurchaseStageTerm")]
        public PurchaseStageTerm PurchaseStageTerm { get; set; }
        [XmlElement(ElementName = "SourceTerm")]
        public SourceTerm SourceTerm { get; set; }
        [XmlElement(ElementName = "RegionNameTerm")]
        public RegionNameTerm RegionNameTerm { get; set; }
        [XmlElement(ElementName = "RequestStartDate")]
        public RequestStartDate RequestStartDate { get; set; }
        [XmlElement(ElementName = "RequestDate")]
        public RequestDate RequestDate { get; set; }
        [XmlElement(ElementName = "AuctionBeginDate")]
        public AuctionBeginDate AuctionBeginDate { get; set; }
        [XmlElement(ElementName = "okdp2MultiMatch")]
        public Okdp2MultiMatch Okdp2MultiMatch { get; set; }
        [XmlElement(ElementName = "okdp2tree")]
        public Okdp2tree Okdp2tree { get; set; }
        [XmlElement(ElementName = "classifier")]
        public Classifier Classifier { get; set; }
        [XmlElement(ElementName = "orgCondition")]
        public OrgCondition OrgCondition { get; set; }
        [XmlElement(ElementName = "orgDictionary")]
        public OrgDictionary OrgDictionary { get; set; }
        [XmlElement(ElementName = "organizator")]
        public Organizator Organizator { get; set; }
        [XmlElement(ElementName = "CustomerCondition")]
        public CustomerCondition CustomerCondition { get; set; }
        [XmlElement(ElementName = "CustomerDictionary")]
        public CustomerDictionary CustomerDictionary { get; set; }
        [XmlElement(ElementName = "customer")]
        public Customer Customer { get; set; }
        [XmlElement(ElementName = "PurchaseWayTerm")]
        public PurchaseWayTerm PurchaseWayTerm { get; set; }
        [XmlElement(ElementName = "PurchaseTypeNameTerm")]
        public PurchaseTypeNameTerm PurchaseTypeNameTerm { get; set; }
        [XmlElement(ElementName = "BranchNameTerm")]
        public BranchNameTerm BranchNameTerm { get; set; }
        [XmlElement(ElementName = "IsSMPTerm")]
        public IsSMPTerm IsSMPTerm { get; set; }
        [XmlElement(ElementName = "statistic")]
        public Statistic Statistic { get; set; }
        public Filters()
        {
            MainSearchBar = new MainSearchBar();
            PurchAmount = new PurchAmount();
            PublicDate = new PublicDate();
            PurchaseStageTerm = new PurchaseStageTerm();
            SourceTerm = new SourceTerm();
            RegionNameTerm = new RegionNameTerm();
            RequestStartDate = new RequestStartDate();
            RequestDate = new RequestDate();
            AuctionBeginDate = new AuctionBeginDate();
            Okdp2MultiMatch = new Okdp2MultiMatch();
            Okdp2tree = new Okdp2tree();
            Classifier = new Classifier();
            OrgCondition = new OrgCondition();
            OrgDictionary = new OrgDictionary();
            Organizator = new Organizator();
            CustomerCondition = new CustomerCondition();
            CustomerDictionary = new CustomerDictionary();
            Customer = new Customer();
            PurchaseWayTerm = new PurchaseWayTerm();
            PurchaseTypeNameTerm = new PurchaseTypeNameTerm();
            BranchNameTerm = new BranchNameTerm();
            IsSMPTerm = new IsSMPTerm();
            Statistic = new Statistic();



        }
    }

    [Serializable]
    [XmlRoot(ElementName = "fields")]
    public class Fields
    {
        [XmlElement(ElementName = "field")]
        public List<string> Field { get; set; }
        public Fields()
        {
            Field = new List<string>()
            {
                "TradeSectionId",
                "purchAmount",
                "purchCurrency",
                "purchCodeTerm",
                "PurchaseTypeName",
                "purchStateName",
                "BidStatusName",
                "OrgName",
                "SourceTerm",
                "PublicDate",
                "RequestDate",
                "RequestStartDate",
                "RequestAcceptDate",
                "EndDate",
                "CreateRequestHrefTerm",
                "CreateRequestAlowed",
                "purchName",
                "BidName",
                "SourceHrefTerm",
                "objectHrefTerm",
                "needPayment",
                "IsSMP",
                "isIncrease",
            };
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "sort")]
    public class Sort
    {
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "direction")]
        public string Direction { get; set; }
        public Sort()
        {
            Value = "default";
            Direction = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "empty")]
    public class Empty
    {
        [XmlElement(ElementName = "filterType")]
        public string FilterType { get; set; }
        [XmlElement(ElementName = "field")]
        public string Field { get; set; }
        public Empty()
        {
            FilterType = "filter_aggregation";
            Field = " ";
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "aggregations")]
    public class AggregationsXml
    {
        [XmlElement(ElementName = "empty")]
        public Empty Empty { get; set; }
        public AggregationsXml()
        {
            Empty = new Empty();
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "elasticrequest")]
    public class Elasticrequest
    {
        [XmlElement(ElementName = "filters")]
        public Filters Filters { get; set; }
        [XmlElement(ElementName = "fields")]
        public Fields Fields { get; set; }
        [XmlElement(ElementName = "sort")]
        public Sort Sort { get; set; }
        [XmlElement(ElementName = "aggregations")]
        public AggregationsXml Aggregations { get; set; }
        [XmlElement(ElementName = "size")]
        public string Size { get; set; }
        [XmlElement(ElementName = "from")]
        public string From { get; set; }
        
        public Elasticrequest()
        {
            Filters = new Filters();
            Fields = new Fields();
            Sort = new Sort();
            Aggregations = new AggregationsXml();
            Size = "20";
            From = "0";
        }
    }
}
