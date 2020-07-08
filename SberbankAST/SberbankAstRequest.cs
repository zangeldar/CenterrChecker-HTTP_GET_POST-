using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
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
            string result = "xmldata=";
            XmlSerializer formatter = new XmlSerializer(typeof(Elasticrequest));
            using (StringWriter sw = new StringWriter())
            {
                formatter.Serialize(sw, elRequest);
                result += sw.ToString();
            }
            result = result.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            result = result.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            result = result.Replace("\r\n", "");
            result = result.Replace("  ", " ");

            result += "&orgId=0";
            result += "&targetPageCode=UnitedPurchaseList";
            return result;
        }

        private string makeAnPost(string url = "https://www.sberbank-ast.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            var request = (HttpWebRequest)WebRequest.Create(url);
            postData = postData.Replace("+", "%2B");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.ContentLength = data.Length;            
            request.SendChunked = true;

            //request.Headers.Add("Accept", "*/*");
            request.Accept = "*/*";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("X-MicrosoftAjax", "Delta=true");
            request.Headers.Add("Cache-Control", "no-cache");
            //request.Headers.Add("Referer",              "https://bankrupt.centerr.ru/");
            request.Referer = url;
            request.Headers.Add("Accept-Language", "ru-RU");
            //request.Headers.Add("User-Agent",           "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            /*
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
             */
            HttpWebResponse response;
            try
            {
                //using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), new UnicodeEncoding()))
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), new UTF8Encoding()))
                {
                    writer.WriteLine(postData);
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
