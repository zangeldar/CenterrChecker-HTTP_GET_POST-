﻿using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ETP_GPB
{
    [Serializable]
    public class GPBRequest : ATorgRequest        
    {
        public override bool isBuy => true;
        public GPBRequest() : base() { }
        public GPBRequest(string searchStr) : base(searchStr) { }

        public override string Type => "ETP_GPB";

        public override string SiteName => "ЭТП ГПБ";

        public override string SiteURL => "https://etpgpb.ru/";

        public override string SearchString { get => MyParameters["search"]; set => MyParameters["search"] = value; }

        public override string ServURL => "procedures/";

        public override IResponse MakeResponse()
        {
            return new GPBResponse(this);
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
                { "search", "" },                             //  строка поиска
                //{ "s", "" },                             //  непонятно что                
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
                    //result += item.Key + "=" + HttpUtility.UrlEncode(item.Value, Encoding.GetEncoding(1251));
                    result += item.Key + "=" + HttpUtility.UrlEncode(item.Value);
                    first = false;
                }
            }

            return ServURL + result;
        }

        private string makeAnPost(string url = "https://etpgpb.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            //postData = postData.Replace("+", "%2B");

            //postData = HttpUtility.HtmlEncode(postData);

            var request = (HttpWebRequest)WebRequest.Create(url + postData);

            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentType = "application/x-www-form-urlencoded; charset=windows-1251";
            request.Host = SiteURL.Substring(SiteURL.IndexOf(":") + 3).Replace("/", "");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");    //Accept-Language:ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7                        
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Referer = url;

            //request.Headers.Add("DNT", "1");            
            //request.ContentLength = data.Length;            
            //request.SendChunked = true;
            //request.Headers.Add("User-Agent",           "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko");
            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //request.Headers.Add("X-MicrosoftAjax", "Delta=true");
            //request.Headers.Add("Cache-Control", "no-cache");
            //request.Headers.Add("Referer",              "https://www.asv.org.ru");
            //request.Referer = url;

            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                lastError = e;
                return null;
            }

            //lastAnswer = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1251)).ReadToEnd();    // put result in lastAnswer to cache   
            lastAnswer = new StreamReader(response.GetResponseStream()).ReadToEnd();    // put result in lastAnswer to cache   

            response.Dispose();

            return lastAnswer;
        }
    }
}
