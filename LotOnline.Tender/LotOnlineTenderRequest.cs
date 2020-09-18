using IAuction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace LotOnline.Tender
{
    [Serializable]
    public class LotOnlineTenderRequest : ATorgRequest
    {
        public override bool isBuy => true;
        public LotOnlineTenderRequest() : base() { }
        public LotOnlineTenderRequest(string searchStr) : base(searchStr)
        {
            InitialiseParameters();
            this.SearchString = searchStr;
            MyParameters["query"] = JsonSerialize(jsonReq);
        }
        private Query jsonReq;
        private string searchString;
        public override string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                MakeJsonRequest();
            }
        }
        private void MakeJsonRequest()
        {
            if (searchString != null)
                jsonReq = new Query(searchString);
        }
        public override string Type => "LotOnline.Tender";

        public override string SiteName => "РАД Тендер";

        public override string SiteURL => "https://tender.lot-online.ru/";

        public override string ServURL => "searchServlet";

        public override string AllParametersInString(string separator = "")
        {
            //return base.AllParametersInString(separator);
            return SearchString;
        }



        public override IResponse MakeResponse()
        {
            return new LotOnlineTenderResponse(this);
        }

        protected override string getBlankResponse()
        {
            initialised = true;
            return "";
        }

        protected override void InitialiseParameters()
        {
            MyParameters = new SerializableDictionary<string, string>
            {
                { "query", ""},
                { "filter", JsonSerialize(new Filter())},
                { "sort", JsonSerialize(new Sort())},
                { "limit", JsonSerialize(new Limit())},
            };
            //jsonReq = new JsonRequest();
        }

        protected override bool Initialize()
        {
            getBlankResponse();
            return initialised;
        }

        protected override string MakePost(string postData = "")
        {
            // https://tender.lot-online.ru/app/SearchLots/page#
            //return makeAnPost(ServiceURL + "app/SearchLots/page#", postData);
            //return makeAnPost(ServiceURL + "searchServlet?query=", postData);
            return makeAnPost(SiteURL + ServURL, postData);
        }
                
        private string JsonSerialize(Object inpObj)
        {
            string result = "";

            JsonSerializer formatter = new JsonSerializer();
            using (StringWriter sw = new StringWriter())
            {
                formatter.Serialize(sw, inpObj);
                result += sw.ToString();
            }
            return HttpUtility.UrlEncode(result);
        }

        private string makeAnPost(string url = "https://tender.lot-online.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            //HttpUtility.UrlEncode(item.Value, Encoding.GetEncoding(1251));
            //postData = postData.Replace("+", "%2B");
            /*
            postData = HttpUtility.UrlEncode(postData);
            postData = postData.Replace("%3a", ":");
            postData = postData.Replace("%2c", ",");
            */
            /*
            postData = postData.Replace(":","%3a");
            postData = postData.Replace(",","%2c");
            */
            var request = (HttpWebRequest)WebRequest.Create(url+postData);
            
            request.Method = "GET";
            //request.SendChunked = true;

            request.Accept = "text/plain, */*";
            request.Accept = "text/plain";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            //request.ContentType = "application/json";
            //request.Referer = url + "/UnitedPurchaseList.aspx";
            //request.Host = ServiceURL;
            request.Referer = SiteURL + "app/SearchLots/page";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/7.0)";

            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("Accept-Language", "ru-RU");
            request.Headers.Add("DNT", "1");
            //request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");

            //request.CookieContainer.Add(new Cookie("es_nonathorized_last_filters|UnitedPurchaseList", ""));
            /*
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("ASP.NET_SessionId", "ac41wdmf1a0bqrzdpmsg3jbk", "", "www.sberbank-ast.ru"));
            request.CookieContainer.Add(new Cookie("es_nonathorized_last_filters|UnitedPurchaseList", "%3Cfilters%3E%3CmainSearchBar%3E%3Cvalue%3E%D1%82%D0%B5%D1%85%D0%BD%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F%20%D0%B6%D0%B8%D0%B4%D0%BA%D0%BE%D1%81%D1%82%D1%8C%3C%2Fvalue%3E%3Ctype%3Ebest_fields%3C%2Ftype%3E%3Cminimum_should_match%3E100%25%3C%2Fminimum_should_match%3E%3C%2FmainSearchBar%3E%3CpurchAmount%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FpurchAmount%3E%3CPublicDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FPublicDate%3E%3CPurchaseStageTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseStageTerm%3E%3CSourceTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FSourceTerm%3E%3CRegionNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FRegionNameTerm%3E%3CRequestStartDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FRequestStartDate%3E%3CRequestDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FRequestDate%3E%3CAuctionBeginDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FAuctionBeginDate%3E%3Cokdp2MultiMatch%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2Fokdp2MultiMatch%3E%3Cokdp2tree%3E%3Cvalue%3E%3C%2Fvalue%3E%3CproductField%3E%3C%2FproductField%3E%3CbranchField%3E%3C%2FbranchField%3E%3C%2Fokdp2tree%3E%3Cclassifier%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Fclassifier%3E%3CorgCondition%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2ForgCondition%3E%3CorgDictionary%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2ForgDictionary%3E%3Corganizator%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Forganizator%3E%3CCustomerCondition%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2FCustomerCondition%3E%3CCustomerDictionary%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2FCustomerDictionary%3E%3Ccustomer%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Fcustomer%3E%3CPurchaseWayTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseWayTerm%3E%3CPurchaseTypeNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseTypeNameTerm%3E%3CBranchNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FBranchNameTerm%3E%3CIsSMPTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FIsSMPTerm%3E%3Cstatistic%3E%3CtotalProc%3E8%20321%20887%3C%2FtotalProc%3E%3CTotalSum%3E22.14%20%D0%A2%D1%80%D0%BB%D0%BD.%3C%2FTotalSum%3E%3CDistinctOrgs%3E74%20479%3C%2FDistinctOrgs%3E%3C%2Fstatistic%3E%3C%2Ffilters%3E", "", "www.sberbank-ast.ru"));
            request.CookieContainer.Add(new Cookie("_ym_uid", "1594197776743467816", "", "www.sberbank-ast.ru"));
            request.CookieContainer.Add(new Cookie("_ym_d", "1594903014", "", "www.sberbank-ast.ru"));
            request.CookieContainer.Add(new Cookie("_ym_isad", "2", "", "www.sberbank-ast.ru"));
            */

            /*
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
             */

            //debug
            //postData = @"xmlData=%3Celasticrequest%3E%3Cfilters%3E%3CmainSearchBar%3E%3Cvalue%3E%D1%82%D0%B5%D1%85%D0%BD%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F+%D0%B6%D0%B8%D0%B4%D0%BA%D0%BE%D1%81%D1%82%D1%8C%3C%2Fvalue%3E%3Ctype%3Ebest_fields%3C%2Ftype%3E%3Cminimum_should_match%3E100%25%3C%2Fminimum_should_match%3E%3C%2FmainSearchBar%3E%3CpurchAmount%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FpurchAmount%3E%3CPublicDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FPublicDate%3E%3CPurchaseStageTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseStageTerm%3E%3CSourceTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FSourceTerm%3E%3CRegionNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FRegionNameTerm%3E%3CRequestStartDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FRequestStartDate%3E%3CRequestDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FRequestDate%3E%3CAuctionBeginDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FAuctionBeginDate%3E%3Cokdp2MultiMatch%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2Fokdp2MultiMatch%3E%3Cokdp2tree%3E%3Cvalue%3E%3C%2Fvalue%3E%3CproductField%3E%3C%2FproductField%3E%3CbranchField%3E%3C%2FbranchField%3E%3C%2Fokdp2tree%3E%3Cclassifier%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Fclassifier%3E%3CorgCondition%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2ForgCondition%3E%3CorgDictionary%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2ForgDictionary%3E%3Corganizator%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Forganizator%3E%3CCustomerCondition%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2FCustomerCondition%3E%3CCustomerDictionary%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2FCustomerDictionary%3E%3Ccustomer%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Fcustomer%3E%3CPurchaseWayTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseWayTerm%3E%3CPurchaseTypeNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseTypeNameTerm%3E%3CBranchNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FBranchNameTerm%3E%3CIsSMPTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FIsSMPTerm%3E%3Cstatistic%3E%3CtotalProc%3E3+656%3C%2FtotalProc%3E%3CTotalSum%3E3.28+%D0%9C%D0%BB%D1%80%D0%B4.%3C%2FTotalSum%3E%3CDistinctOrgs%3E1+306%3C%2FDistinctOrgs%3E%3C%2Fstatistic%3E%3C%2Ffilters%3E%3Cfields%3E%3Cfield%3ETradeSectionId%3C%2Ffield%3E%3Cfield%3EpurchAmount%3C%2Ffield%3E%3Cfield%3EpurchCurrency%3C%2Ffield%3E%3Cfield%3EpurchCodeTerm%3C%2Ffield%3E%3Cfield%3EPurchaseTypeName%3C%2Ffield%3E%3Cfield%3EpurchStateName%3C%2Ffield%3E%3Cfield%3EBidStatusName%3C%2Ffield%3E%3Cfield%3EOrgName%3C%2Ffield%3E%3Cfield%3ESourceTerm%3C%2Ffield%3E%3Cfield%3EPublicDate%3C%2Ffield%3E%3Cfield%3ERequestDate%3C%2Ffield%3E%3Cfield%3ERequestStartDate%3C%2Ffield%3E%3Cfield%3ERequestAcceptDate%3C%2Ffield%3E%3Cfield%3EEndDate%3C%2Ffield%3E%3Cfield%3ECreateRequestHrefTerm%3C%2Ffield%3E%3Cfield%3ECreateRequestAlowed%3C%2Ffield%3E%3Cfield%3EpurchName%3C%2Ffield%3E%3Cfield%3EBidName%3C%2Ffield%3E%3Cfield%3ESourceHrefTerm%3C%2Ffield%3E%3Cfield%3EobjectHrefTerm%3C%2Ffield%3E%3Cfield%3EneedPayment%3C%2Ffield%3E%3Cfield%3EIsSMP%3C%2Ffield%3E%3Cfield%3EisIncrease%3C%2Ffield%3E%3C%2Ffields%3E%3Csort%3E%3Cvalue%3Edefault%3C%2Fvalue%3E%3Cdirection%3E%3C%2Fdirection%3E%3C%2Fsort%3E%3Caggregations%3E%3Cempty%3E%3CfilterType%3Efilter_aggregation%3C%2FfilterType%3E%3Cfield%3E%3C%2Ffield%3E%3C%2Fempty%3E%3C%2Faggregations%3E%3Csize%3E20%3C%2Fsize%3E%3Cfrom%3E0%3C%2Ffrom%3E%3C%2Felasticrequest%3E&orgId=0&targetPageCode=UnitedPurchaseList";


            HttpWebResponse response;
            try
            {
                //using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), new UnicodeEncoding()))                
                //using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                /*
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), new UTF8Encoding()))
                {
                    //writer.WriteLine(postData);
                    writer.Write(postData);
                }
                */
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                lastError = e;
                return null;
                //throw;
            }

            lastAnswer = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();    // put result in lastAnswer to cache     
            response.Dispose();

            return lastAnswer;
        }


    }
}
