using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace RosElTorg
{
    [Serializable]
    public class RosElTorgRequest : ATorgRequest
    {
        public override bool isBuy => true;
        public RosElTorgRequest() : base() { }
        public RosElTorgRequest(string searchStr) : base(searchStr) { }

        public override string Type => "RosElTorg";

        public override string SiteName => "РосЭлТорг";

        public override string SiteURL => "https://www.roseltorg.ru/";

        public override string SearchString { get => MyParameters["query_field"]; set => MyParameters["query_field"] = value; }

        public override string ServURL => "/procedures/search";

        public override IResponse MakeResponse()
        {
            return new RosElTorgResponse(this);
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
                { "query_field", "" },                                //  строка поиска                                
            };
        }

        protected override bool Initialize()
        {
            getBlankResponse();
            return initialised;
        }

        protected override string MakePost(string postData = "")
        {
            return makeAnPost(SiteURL, postData);
        }

        protected override string myRawPostData()
        {
            string result = "";
            bool first = true;
            foreach (KeyValuePair<string, string> item in MyParameters)
            {
                if (item.Value != "")
                {
                    if (first)
                    {
                        result += "?";
                        first = false;
                    }
                    else
                        result += "&";
                    result += item.Key + "=" + item.Value;
                }
            }

            return ServURL + result;
        }

        private string makeAnPost(string url = "https://www.roseltorg.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            var request = (HttpWebRequest)WebRequest.Create(url + postData);
            //postData = postData.Replace("+", "%2B");
            postData = postData.Replace(" ", "+");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.ContentLength = data.Length;            
            //request.SendChunked = true;

            //request.Headers.Add("Accept", "*/*");
            request.Accept = "*/*";
            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //request.Headers.Add("X-MicrosoftAjax", "Delta=true");
            //request.Headers.Add("Cache-Control", "no-cache");
            //request.Headers.Add("Referer",              "https://www.roseltorg.ru/");
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
    }
}
