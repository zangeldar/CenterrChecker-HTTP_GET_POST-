using HtmlParser;
using IAuction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace RTSTender
{
    [Serializable]
    public class RTSTenderRequest : ATorgRequest
    {
        public override bool isBuy => true;
        public RTSTenderRequest() : base() { }
        public RTSTenderRequest(string searchStr) : base(searchStr) { }
        public override string Type => "RTSTender";
        public override string SiteName => "РТС-тендер";
        public override string SiteURL => "https://www.rts-tender.ru/";

        private string Token = "";
        private string Cookies = "";

        public override string SearchString
        {
            get => MyParameters["SearchQuery"];
            set
            {
                MyParameters["SearchQuery"] = value;
                MakeASPRequest();
            }
        }

        public override string ServURL => "poisk/search/ajaxwithfullmodel";

        private AjaxRequest aspRequest;
        private void MakeASPRequest()
        {
            aspRequest.Filter.IsSearchAttachment = false;
            if (MyParameters.ContainsKey("SearchQuery"))
                aspRequest.SearchQuery = MyParameters["SearchQuery"];
        }

        public override IResponse MakeResponse()
        {
            return new RTSTenderResponse(this);
        }

        protected override string getBlankResponse()
        {
            string tmpStr = makeAnPost(SiteURL + "poisk/", "", false);
            if (Token != "" & Cookies != "")
                initialised = true;
            else if (lastError != null)
                return lastError.Message;
            return "";            
        }

        protected override void InitialiseParameters()
        {
            MyParameters = new SerializableDictionary<string, string>
            {                
                { "SearchQuery", "" },                                  //  строка поиска                                
                /*
                { "Top", "10" },                                        //  результатов на страницу
                { "Sort", "PublishDate" },
                { "keywords", ""}                                       //  строка поиска  
                */
            };
            aspRequest = new AjaxRequest();            
        }

        protected override bool Initialize()
        {
            lastError = null;
            initialised = false;
            Cookies = "";
            Token = "";
            getBlankResponse();
            return initialised;
        }

        protected override string myRawPostData()
        {
            //return base.myRawPostData();
            string result = "";
            
            JsonSerializer formatter = new JsonSerializer();
            using (StringWriter sw = new StringWriter())
            {
                formatter.Serialize(sw, aspRequest);
                result += sw.ToString();
            }
            return result;
        }

        protected override string MakePost(string postData = "")
        {
            // Вычленяем ID searchProfile для получения результатов поиска
            {
                /*
                List<Tag> res = HTMLParser.Parse(tmpStr);
                bool needBreak = false;
                string searchPriofileId = "";
                foreach (Tag item in res)
                {
                    foreach (Tag inItem in item.LookForChildTag("input", true, new KeyValuePair<string, string>("id", "SearchProfile")))
                    {
                        if (inItem.Attributes.ContainsKey("value"))
                        {
                            searchPriofileId = inItem.Attributes["value"];
                            if (searchPriofileId != "")
                            {
                                needBreak = true;
                                break;
                            }

                        }
                    }
                    if (needBreak)
                        break;
                }
                */
            }
            
            return makeAnPost(SiteURL + ServURL, postData, true);
        }

        /// <summary>
        /// Выполняет непосредственно формирование и отправку запроса
        /// </summary>
        /// <param name="url">Ссылка запроса</param>
        /// <param name="postData">Параметры запроса строкой</param>
        /// <param name="isPOST">Указатель типа запроса POST. По-умолчанию - GET</param>
        /// <returns></returns>
        private string makeAnPost(string url = "https://www.rts-tender.ru/", string postData = "", bool isPOST = true)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            //postData = HttpUtility.UrlEncode(postData);
            //postData = "{\"Filter\":{\"ContractStatuses\":[1,2,3],\"NotificationStatuses\":[],\"IsSearch223\":true,\"IsSearch44\":true,\"IsSearch615\":true,\"IsSearchCOM\":true,\"IsSearchZMO\":true,\"CustomerIds\":[],\"DeliveryRegionIds\":[],\"EtpCodes\":[\"1\",\"5\",\"13\",\"25\",\"32\",\"74\",\"125\",\"126\",\"1\",\"4\",\"7\",\"3\",\"9\",\"2\",\"6\",\"5\",\"8\",\"10\",\"11\",\"12\",\"13\",\"14\",\"15\",\"16\",\"17\",\"18\",\"19\",\"20\",\"21\",\"22\",\"23\",\"24\",\"25\",\"26\",\"27\",\"28\",\"29\",\"30\",\"34\",\"31\",\"32\",\"33\",\"40\",\"38\",\"35\",\"36\",\"37\",\"44\",\"43\",\"39\",\"42\",\"45\",\"47\",\"41\",\"48\",\"49\",\"56\",\"46\",\"55\",\"53\",\"51\",\"52\",\"54\",\"59\",\"68\",\"57\",\"66\",\"61\",\"67\",\"50\",\"69\",\"62\",\"64\",\"100\",\"76\",\"70\",\"58\",\"77\",\"75\",\"84\",\"72\",\"73\",\"63\",\"82\",\"71\",\"65\",\"79\",\"81\",\"86\",\"88\",\"83\",\"85\",\"91\",\"96\",\"93\",\"80\",\"74\",\"102\",\"97\",\"98\",\"103\",\"94\",\"101\",\"110\",\"106\",\"104\",\"90\",\"99\",\"89\",\"115\",\"113\",\"87\",\"78\",\"92\",\"105\",\"107\",\"108\",\"109\",\"112\",\"116\",\"114\",\"111\",\"125\",\"126\",\"95\",\"1\",\"5\",\"13\",\"25\",\"32\",\"74\",\"125\",\"126\",\"1\",\"1\"],\"Inn\":null,\"InnConcurents\":null,\"InnOrganizers\":null,\"NmcIds\":[],\"Number\":null,\"OKEICodes\":[],\"OKPD2Codes\":[],\"PreferenseIds\":[],\"PwsIds\":[],\"RegionIds\":[],\"Restrict\":[],\"StateAtMarketPlace\":[],\"TruName\":null,\"ApplicationGuaranteeAmount\":null,\"AuctionTimeEnd\":null,\"AuctionTimeStart\":null,\"CollectingEndDateEnd\":null,\"CollectingEndDateStart\":null,\"ContractGuaranteeAmount\":null,\"ContractSignEnd\":null,\"ContractSignStart\":null,\"CreateDateEnd\":null,\"CreateDateStart\":null,\"IsExactMatch\":false,\"IsMedicAnalog\":false,\"IsNotJoint\":false,\"IsSearchAttachment\":true,\"IsSearchByOrganization\":false,\"IsSearchNotActualStandarts\":false,\"IsSearchPaymentDate\":false,\"IsSearchPrePayment\":false,\"IsSmp\":null,\"NmcFrom\":0,\"NmcTo\":null,\"PriceFrom\":null,\"PriceTo\":null,\"PublishDateEnd\":null,\"PublishDateStart\":null,\"QuantityFrom\":null,\"QuantityTo\":null,\"WithoutApplicationGuarantee\":false,\"WithoutContractGuarantee\":false,\"ApplicationFillingNotCompleted\":true},\"IsAscendingSorting\":false,\"SearchQuery\":\"С‚РµС…РЅРёС‡РµСЃРєР°СЏ Р¶РёРґРєРѕСЃС‚СЊ\",\"Skip\":0,\"Sort\":\"PublishDate\",\"Top\":10,\"Type\":1,\"RegionAlias\":\"\",\"CityName\":\"\"}";

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (!isPOST)
                request = (HttpWebRequest)WebRequest.Create(url + postData);

            //postData = postData.Replace("+", "%2B");
            //postData = postData.Replace(" ", "+");            
            request.Method = "POST";
            if (!isPOST)
                request.Method = "GET";
            else
            {
                request.Headers.Add("X-XSRF-TOKEN", Token);
                request.KeepAlive = true;
                {
                    var sp = request.ServicePoint;
                    var prop = sp.GetType().GetProperty("HttpBehaviour",
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    prop.SetValue(sp, (byte)0, null);
                }
                //request.Connection = "Keep-Alive";
                request.Headers.Add("Cache-Control", "no-cache");
                request.ServicePoint.Expect100Continue = false;
                request.ContentLength = bytes.Length;
                
                request.Headers.Add("Cookie", Cookies);                
            }
                

            //request.KeepAlive = true;
            //request.KeepAlive = false;
            //request.Timeout = System.Threading.Timeout.Infinite;
            //request.ProtocolVersion = HttpVersion.Version10;

            request.Referer = "https://www.rts-tender.ru/poisk/";
            request.Accept = "application/json, text/plain, */*";
            //request.Accept = "*/*";
            request.Headers.Add("DNT", "1");
            //request.Headers.Add("User-Agent",           "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentType = "application/json;charset=UTF-8";
            request.Headers.Add("Accept-Language", "ru-RU");    //Accept-Language:ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            //request.ContentLength = postData.Length;            
            
            
            //request.SendChunked = true;


            /*
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            */

            HttpWebResponse response;

            try
            {

                //using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))//, new UTF8Encoding()))
                if (isPOST)
                {
                    using (Stream writer = request.GetRequestStream())//, new UTF8Encoding()))
                    {
                        //writer.Write(postData);                    
                        writer.Write(bytes, 0, bytes.Length);
                    }
                }

                /*
                //byte[] bytes = Encoding.UTF8.GetBytes(postData);
                Stream writer = request.GetRequestStream();
                writer.Write(bytes, 0, bytes.Length);
                */

                /*
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write()
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

            if (response.Headers.HasKeys())
            {
                for (int i = 0; i < response.Headers.Count; i++)
                {
                    if (response.Headers.Keys[i] == "Set-Cookie")
                    {
                        Cookies += response.Headers[i];

                        // Вычленяем XSRF-Token, если он потребуется..
                        
                        if (Cookies.Contains("XSRF-TOKEN="))
                        {
                            int start = Cookies.IndexOf("XSRF-TOKEN=");
                            int end = Cookies.Substring(start).IndexOf(";");
                            Token = Cookies.Substring(start, end).Replace("XSRF-TOKEN=", "");
                        }
                        

                        break;
                    }  
                }
            }
            lastAnswer = new StreamReader(response.GetResponseStream()).ReadToEnd();    // put result in lastAnswer to cache   

            response.Dispose();

            return lastAnswer;
        }
    }
}
