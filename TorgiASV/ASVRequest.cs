using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TorgiASV
{    
    [Serializable]
    public class ASVRequest
    {
        private string postData;
        public ASVRequest(string postData)
        {
            this.postData = postData;
            Initialize();
        }
        private bool initialised = false;
        private bool Initialize()
        {
            if (true)
                initialised = true;
            return initialised;
        }

        // internal cached result
        private string lastAnswer;
        public string GetResponse
        {
            get
            {
                if (!initialised)       // initialized already?
                    if (!Initialize())  // if not then Initialize now!
                        return null;    // if not success then break
                return makeAnPost("https://www.torgiasv.ru", postData);
            }
        }
        private string makeAnPost(string url = "https://www.torgiasv.ru", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            var request = (HttpWebRequest)WebRequest.Create(url+postData);
            postData = postData.Replace("+", "%2B");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //request.ContentLength = data.Length;            
            //request.SendChunked = true;

            //request.Headers.Add("Accept", "*/*");
            request.Accept = "*/*";
            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //request.Headers.Add("X-MicrosoftAjax", "Delta=true");
            //request.Headers.Add("Cache-Control", "no-cache");
            //request.Headers.Add("Referer",              "https://www.torgiasv.ru");
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

            var response = (HttpWebResponse)request.GetResponse();

            lastAnswer = new StreamReader(response.GetResponseStream()).ReadToEnd();    // put result in lastAnswer to cache   

            response.Dispose();            

            return lastAnswer;
        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
