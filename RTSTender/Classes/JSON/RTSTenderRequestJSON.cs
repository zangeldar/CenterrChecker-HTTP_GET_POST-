using System;
using System.Collections.Generic;
using System.Text;

namespace RTSTender
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Filter
    {
        public Filter()
        {
            ContractStatuses                    = new List<int>() { 1, 2, 3 };
            NotificationStatuses                = new List<object>();
            IsSearch223                         = true;
            IsSearch44                          = true;
            IsSearch615                         = true;
            IsSearchCOM                         = true;
            IsSearchZMO = true;
            CustomerIds = new List<object>();
            DeliveryRegionIds = new List<object>();
            EtpCodes = new List<string>() { "1", "5", "13", "25", "32", "74", "125", "126", "1", "4", "7", "3", "9", "2", "6", "5", "8", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "34", "31", "32", "33", "40", "38", "35", "36", "37", "44", "43", "39", "42", "45", "47", "41", "48", "49", "56", "46", "55", "53", "51", "52", "54", "59", "68", "57", "66", "61", "67", "50", "69", "62", "64", "100", "76", "70", "58", "77", "75", "84", "72", "73", "63", "82", "71", "65", "79", "81", "86", "88", "83", "85", "91", "96", "93", "80", "74", "102", "97", "98", "103", "94", "101", "110", "106", "104", "90", "99", "89", "115", "113", "87", "78", "92", "105", "107", "108", "109", "112", "116", "114", "111", "125", "126", "95", "1", "5", "13", "25", "32", "74", "125", "126", "1", "1" };
            Inn = null;
            InnConcurents = null;
            InnOrganizers = null;
            NmcIds = new List<object>();
            Number = null;
            OKEICodes = new List<object>();
            OKPD2Codes = new List<object>();
            PreferenseIds = new List<object>();
            PwsIds = new List<object>();
            RegionIds = new List<object>();
            Restrict = new List<object>();
            StateAtMarketPlace = new List<object>();
            TruName = null;
            ApplicationGuaranteeAmount = null;
            AuctionTimeEnd = null;
            AuctionTimeStart = null;
            CollectingEndDateEnd = null;
            CollectingEndDateStart = null;
            ContractGuaranteeAmount = null;
            ContractSignEnd = null;
            ContractSignStart = null;
            CreateDateEnd = null;
            CreateDateStart = null;
            IsExactMatch = false;
            IsMedicAnalog = false;
            IsNotJoint = false;
            IsSearchAttachment = false;                  // Искать в приложенных файлах? по-умолчанию - true
            IsSearchByOrganization = false;
            IsSearchNotActualStandarts = false;
            IsSearchPaymentDate = false;
            IsSearchPrePayment = false;
            IsSmp = null;
            NmcFrom = 0;
            NmcTo = null;
            PriceFrom = null;
            PriceTo = null;
            PublishDateEnd = null;
            PublishDateStart = null;
            QuantityFrom = null;
            QuantityTo = null;
            WithoutApplicationGuarantee = false;
            WithoutContractGuarantee = false;
            ApplicationFillingNotCompleted = true;
        }

        public List<int> ContractStatuses { get; set; }
        public List<object> NotificationStatuses { get; set; }
        public bool IsSearch223 { get; set; }
        public bool IsSearch44 { get; set; }
        public bool IsSearch615 { get; set; }
        public bool IsSearchCOM { get; set; }
        public bool IsSearchZMO { get; set; }
        public List<object> CustomerIds { get; set; }
        public List<object> DeliveryRegionIds { get; set; }
        public List<string> EtpCodes { get; set; }
        public object Inn { get; set; }
        public object InnConcurents { get; set; }
        public object InnOrganizers { get; set; }
        public List<object> NmcIds { get; set; }
        public object Number { get; set; }
        public List<object> OKEICodes { get; set; }
        public List<object> OKPD2Codes { get; set; }
        public List<object> PreferenseIds { get; set; }
        public List<object> PwsIds { get; set; }
        public List<object> RegionIds { get; set; }
        public List<object> Restrict { get; set; }
        public List<object> StateAtMarketPlace { get; set; }
        public object TruName { get; set; }
        public object ApplicationGuaranteeAmount { get; set; }
        public object AuctionTimeEnd { get; set; }
        public object AuctionTimeStart { get; set; }
        public object CollectingEndDateEnd { get; set; }
        public object CollectingEndDateStart { get; set; }
        public object ContractGuaranteeAmount { get; set; }
        public object ContractSignEnd { get; set; }
        public object ContractSignStart { get; set; }
        public object CreateDateEnd { get; set; }
        public object CreateDateStart { get; set; }
        public bool IsExactMatch { get; set; }
        public bool IsMedicAnalog { get; set; }
        public bool IsNotJoint { get; set; }
        public bool IsSearchAttachment { get; set; }
        public bool IsSearchByOrganization { get; set; }
        public bool IsSearchNotActualStandarts { get; set; }
        public bool IsSearchPaymentDate { get; set; }
        public bool IsSearchPrePayment { get; set; }
        public object IsSmp { get; set; }
        public int NmcFrom { get; set; }
        public object NmcTo { get; set; }
        public object PriceFrom { get; set; }
        public object PriceTo { get; set; }
        public object PublishDateEnd { get; set; }
        public object PublishDateStart { get; set; }
        public object QuantityFrom { get; set; }
        public object QuantityTo { get; set; }
        public bool WithoutApplicationGuarantee { get; set; }
        public bool WithoutContractGuarantee { get; set; }
        public bool ApplicationFillingNotCompleted { get; set; }
    }

    public class AjaxRequest
    {
        public AjaxRequest(string searchStr="", bool searchInFiles = false)
        {
            Filter = new Filter() { IsSearchAttachment = searchInFiles, };
            IsAscendingSorting = false;
            SearchQuery = searchStr;
            Skip = 0;
            Sort = "PublishDate";
            Top = 10;
            Type = 1;
            RegionAlias = "";
            CityName = "";
        }
        public Filter Filter { get; set; }
        public bool IsAscendingSorting { get; set; }
        public string SearchQuery { get; set; }
        public int Skip { get; set; }
        public string Sort { get; set; }
        public int Top { get; set; }
        public int Type { get; set; }
        public string RegionAlias { get; set; }
        public string CityName { get; set; }
    }


}
