using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LotOnline
{
    [Serializable]
    public abstract class LotOnlineRequest : ATorgRequest
    {
        public LotOnlineRequest() : base() { }
        public LotOnlineRequest(string searchStr) : base(searchStr) { }

        public override string Type => "LotOnline";

        public override string SiteName => "Лот-Онлайн";

        public override string SiteURL => "https://lot-online.ru/";
        public override string ServURL => "search.rest";

        public override string SearchString { get => MyParameters["keyWords"]; set => MyParameters["keyWords"] = value; }

        public override IResponse MakeResponse()
        {
            return new LotOnlineResponse(this);
            //return LotOnlineResponse.FactoryMethod(this, ServiceURL);
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
                { "keyWords", "" },                                 //  строка поиска
                //{ "callback", "privatizationGrid_success" },        //  представление результата (одно из вариантов значения: lot) - для разбора бесполезно                
                //{ "publicationDateFrom", "" },                      //  дата публикации с ..
                //{ "publicationDateTo", "" },                        //  дата публикации по ..
                //{ "organization", "" },                             //  организация
                //{ "priceFrom", "" },                                //  цена с ..
                //{ "priceTo", "" },                                  //  цена по ..
                //{ "countryCode", "" },                              //  код страны
                //{ "regionCode", "" },                               //  код региона
                //{ "_search", "false" },                             //  ? (false)
                //{ "nd", "1598512570172" },                          //  ? (1598512570172)
                { "rows", "10" },                                    //  количество строк (5)
                { "page", "1" },                                    //  вывести страницу № (1)
                { "sidx", "publicationDate" },                      //  сортировка по дате публикации
                { "sord", "desc" },                                 //  сортировка по убыванию
                //{ "_", "1598512570172" },                           //  ? (1598512570172)
            };
        }

        protected override bool Initialize()
        {
            getBlankResponse();
            return initialised;
        }

        /*
        protected override string myRawPostData()
        {
            return base.myRawPostData();
        }
        */

        protected override string MakePost(string postData = "")
        {
            return makeAnPost(SiteURL + ServURL, postData);
        }

        // ПЕРЕДЕЛАТЬ 
        private string makeAnPost(string url = "https://lot-online.ru/", string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            postData = postData.Replace("+", "%2B");
            postData = postData.Replace(" ", "+");
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
    }
}
