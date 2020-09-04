using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LotOnline.Sales
{
    [Serializable]
    public class LotOnlineSalesRequest : ATorgRequest
    {
        public LotOnlineSalesRequest() : base() { }
        public LotOnlineSalesRequest(string searchStr) : base(searchStr) { }
        public override string Type => "LotOnline.Sales";

        public override string SiteName => "РАД Банкротство";

        public override string ServiceURL => "https://sales.lot-online.ru/";

        public override string SearchString { get => Base64Decode(MyParameters["cscs"]); set => MyParameters["cscs"] = Base64Encode(value); }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public override IResponse MakeResponse()
        {
            return new LotOnlineSalesResponse(this);            
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
                { "cscs", "" },                                 //  строка поиска         
            };
        }

        protected override bool Initialize()
        {
            getBlankResponse();
            return initialised;
        }

        protected override string MakePost(string postData = "")
        {
            return makeAnPost(ServiceURL + "e-auction/lots.xhtml", postData);
        }

        private string makeAnPost(string url = "https://sales.lot-online.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            //postData = postData.Replace("+", "%2B");
            //postData = postData.Replace(" ", "+");
            var request = (HttpWebRequest)WebRequest.Create(url + postData);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.ContentLength = data.Length;            
            //request.SendChunked = true;

            //request.Headers.Add("Accept", "*/*");
            request.Accept = "*/*";
            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //request.Headers.Add("X-MicrosoftAjax", "Delta=true");
            //request.Headers.Add("Cache-Control", "no-cache");
            //request.Headers.Add("Referer",              "https://lot-online.ru/");
            request.Referer = url;
            request.Headers.Add("Accept-Language", "ru-RU");    //Accept-Language:ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7
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
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                lastError = e;
                return null;
                //throw;
            }

            lastAnswer = new StreamReader(response.GetResponseStream()).ReadToEnd();    // put result in lastAnswer to cache   

            response.Dispose();

            return lastAnswer;
        }

        public override string AllParametersInString(string separator = "")
        {
            return base.AllParametersInString(separator);
        }
    }
}
