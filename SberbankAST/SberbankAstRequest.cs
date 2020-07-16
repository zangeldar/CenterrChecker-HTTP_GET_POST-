using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace SberbankAST
{
    public class SberbankAstRequest : ATorgRequest
    {
        private Elasticrequest elRequest;
        public SberbankAstRequest() : base()
        {
            InitialiseParameters();
        }
        public SberbankAstRequest(string searchStr) : base(searchStr)
        {
            InitialiseParameters();
            elRequest.Filters.MainSearchBar.Value = searchStr;
        }

        public override string Type => "SberbankAST";

        public override string SiteName => "Сбербанк-АСТ";

        public override string ServiceURL => "https://www.sberbank-ast.ru/";

        public override string SearchString {
            get => elRequest.Filters.MainSearchBar.Value;
            set => elRequest.Filters.MainSearchBar.Value = value;
        }

        public override IResponse MakeResponse()
        {
            throw new NotImplementedException();
        }

        protected override string getBlankResponse()
        {
            //string result = makeAnPost();
            initialised = true;
            return "";
            //throw new NotImplementedException();
        }

        protected override void InitialiseParameters()
        {
            elRequest = new Elasticrequest();
            //throw new NotImplementedException();
        }

        protected override bool Initialize()
        {
            getBlankResponse();
            return initialised;
            //throw new NotImplementedException();
        }

        protected override string MakePost(string postData = "")
        {
            return makeAnPost(ServiceURL+ "SearchQuery.aspx?name=Main", postData);
            //return makeAnPost(ServiceURL + "UnitedPurchaseList.aspx", postData);
            //throw new NotImplementedException();
        }

        protected override string myRawPostData()
        {
            string result = "";// = "xmldata=";
            XmlSerializer formatter = new XmlSerializer(typeof(Elasticrequest));
            using (StringWriter sw = new StringWriter())
            {
                formatter.Serialize(sw, elRequest);
                result += sw.ToString();
            }
            result = result.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            result = result.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            result = result.Replace("\r\n", "");
            while (result.Contains("  "))
                result = result.Replace("  ", " ");
            result = result.Replace("> <", "><");

            result = HttpUtility.UrlEncode(result); // получаем %% вместо спец.симоволов

            result += "&orgId=0";
            result += "&targetPageCode=UnitedPurchaseList";

            result = "xmlData=" + result;

            return result;
        }

        private string makeAnPost(string url = "https://www.sberbank-ast.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            var request = (HttpWebRequest)WebRequest.Create(url);
            //postData = postData.Replace("+", "%2B");
            request.Method = "POST";                        
            request.SendChunked = true;
                       
            request.Accept = "*/*";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.Referer = url + "/UnitedPurchaseList.aspx";
            request.Referer = ServiceURL + "UnitedPurchaseList.aspx";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/7.0)";

            request.Headers.Add("X-Requested-With", "XMLHttpRequest");                        
            request.Headers.Add("Accept-Language", "ru-RU");                        
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Cache-Control", "no-cache");
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
            postData = @"xmlData=%3Celasticrequest%3E%3Cfilters%3E%3CmainSearchBar%3E%3Cvalue%3E%D1%82%D0%B5%D1%85%D0%BD%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B0%D1%8F+%D0%B6%D0%B8%D0%B4%D0%BA%D0%BE%D1%81%D1%82%D1%8C%3C%2Fvalue%3E%3Ctype%3Ebest_fields%3C%2Ftype%3E%3Cminimum_should_match%3E100%25%3C%2Fminimum_should_match%3E%3C%2FmainSearchBar%3E%3CpurchAmount%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FpurchAmount%3E%3CPublicDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FPublicDate%3E%3CPurchaseStageTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseStageTerm%3E%3CSourceTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FSourceTerm%3E%3CRegionNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FRegionNameTerm%3E%3CRequestStartDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FRequestStartDate%3E%3CRequestDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FRequestDate%3E%3CAuctionBeginDate%3E%3Cminvalue%3E%3C%2Fminvalue%3E%3Cmaxvalue%3E%3C%2Fmaxvalue%3E%3C%2FAuctionBeginDate%3E%3Cokdp2MultiMatch%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2Fokdp2MultiMatch%3E%3Cokdp2tree%3E%3Cvalue%3E%3C%2Fvalue%3E%3CproductField%3E%3C%2FproductField%3E%3CbranchField%3E%3C%2FbranchField%3E%3C%2Fokdp2tree%3E%3Cclassifier%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Fclassifier%3E%3CorgCondition%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2ForgCondition%3E%3CorgDictionary%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2ForgDictionary%3E%3Corganizator%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Forganizator%3E%3CCustomerCondition%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2FCustomerCondition%3E%3CCustomerDictionary%3E%3Cvalue%3E%3C%2Fvalue%3E%3C%2FCustomerDictionary%3E%3Ccustomer%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2Fcustomer%3E%3CPurchaseWayTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseWayTerm%3E%3CPurchaseTypeNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FPurchaseTypeNameTerm%3E%3CBranchNameTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FBranchNameTerm%3E%3CIsSMPTerm%3E%3Cvalue%3E%3C%2Fvalue%3E%3Cvisiblepart%3E%3C%2Fvisiblepart%3E%3C%2FIsSMPTerm%3E%3Cstatistic%3E%3CtotalProc%3E3+656%3C%2FtotalProc%3E%3CTotalSum%3E3.28+%D0%9C%D0%BB%D1%80%D0%B4.%3C%2FTotalSum%3E%3CDistinctOrgs%3E1+306%3C%2FDistinctOrgs%3E%3C%2Fstatistic%3E%3C%2Ffilters%3E%3Cfields%3E%3Cfield%3ETradeSectionId%3C%2Ffield%3E%3Cfield%3EpurchAmount%3C%2Ffield%3E%3Cfield%3EpurchCurrency%3C%2Ffield%3E%3Cfield%3EpurchCodeTerm%3C%2Ffield%3E%3Cfield%3EPurchaseTypeName%3C%2Ffield%3E%3Cfield%3EpurchStateName%3C%2Ffield%3E%3Cfield%3EBidStatusName%3C%2Ffield%3E%3Cfield%3EOrgName%3C%2Ffield%3E%3Cfield%3ESourceTerm%3C%2Ffield%3E%3Cfield%3EPublicDate%3C%2Ffield%3E%3Cfield%3ERequestDate%3C%2Ffield%3E%3Cfield%3ERequestStartDate%3C%2Ffield%3E%3Cfield%3ERequestAcceptDate%3C%2Ffield%3E%3Cfield%3EEndDate%3C%2Ffield%3E%3Cfield%3ECreateRequestHrefTerm%3C%2Ffield%3E%3Cfield%3ECreateRequestAlowed%3C%2Ffield%3E%3Cfield%3EpurchName%3C%2Ffield%3E%3Cfield%3EBidName%3C%2Ffield%3E%3Cfield%3ESourceHrefTerm%3C%2Ffield%3E%3Cfield%3EobjectHrefTerm%3C%2Ffield%3E%3Cfield%3EneedPayment%3C%2Ffield%3E%3Cfield%3EIsSMP%3C%2Ffield%3E%3Cfield%3EisIncrease%3C%2Ffield%3E%3C%2Ffields%3E%3Csort%3E%3Cvalue%3Edefault%3C%2Fvalue%3E%3Cdirection%3E%3C%2Fdirection%3E%3C%2Fsort%3E%3Caggregations%3E%3Cempty%3E%3CfilterType%3Efilter_aggregation%3C%2FfilterType%3E%3Cfield%3E%3C%2Ffield%3E%3C%2Fempty%3E%3C%2Faggregations%3E%3Csize%3E20%3C%2Fsize%3E%3Cfrom%3E0%3C%2Ffrom%3E%3C%2Felasticrequest%3E&orgId=0&targetPageCode=UnitedPurchaseList";


            HttpWebResponse response;
            try
            {
                //using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), new UnicodeEncoding()))
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), new UTF8Encoding()))
                {
                    //writer.WriteLine(postData);
                    writer.Write(postData);
                }
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                lastError = e;
                return null;
                //throw;
            }

            lastAnswer = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();    // put result in lastAnswer to cache     

            return lastAnswer;
        }
    }
}
