using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace TorgiASV
{    
    [Serializable]
    public class ASVRequest : IRequest
    {
        public string Type { get { return "ASV"; } }
        public string ServiceURL { get { return "https://www.torgiasv.ru"; } }
        private Exception lastError;
        public Exception LastError() { return lastError; }
        public ASVRequest()
        {
            //this.postData = postData;
            InitialiseParameters();
            Initialize();
        }
        public ASVRequest(string searchStr)
        {
            InitialiseParameters();
            Initialize();
            SearchString = searchStr;
        }

        private bool initialised = false;
        private bool Initialize()
        {
            getBlankResponse();
            return initialised;
        }
        private string getBlankResponse()   // используется для выполнения первого запроса, с целью получить идентификаторы сессии
        {
            /*
            string answer = makeAnPost();
            _cviewstate = getHtmlParameter(answer, "<input type=\"hidden\" name=\"__CVIEWSTATE\" id=\"__CVIEWSTATE\" value=\"", "\"");
            _eventvalidation = getHtmlParameter(answer, "<input type=\"hidden\" name=\"__EVENTVALIDATION\" id=\"__EVENTVALIDATION\" value=\"", "\"");
            if (_cviewstate.Length > 0 & _eventvalidation.Length > 0)
                initialised = true;
            return answer;
            */
            initialised = true;
            return "";
        }

        public string SiteName { get { return "Торги АСВ"; } }

        public string SearchString
        {
            get { return myPar["q"]; }
            set { myPar["q"] = value; }
        }

        private SerializableDictionary<string, string> myPar;
        public SerializableDictionary<string, string> MyParameters { get { return myPar; } set { myPar = value; } }
        private void InitialiseParameters()
        {
            //myPar = new Dictionary<string, string>();
            myPar = new SerializableDictionary<string, string>
            {
                { "q", "" },                                //  строка поиска
                //{ "show", "" },                             //  представление результата (одно из вариантов значения: lot) - для разбора бесполезно                
            };
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
                return makeAnPost("https://www.torgiasv.ru", myRawPostData());
            }
        }

        public SerializableDictionary<string, string> MyPar { get => myPar; set => myPar = value; }

        private string myRawPostData()
        {
            string result = "";
            bool first = true;
            foreach (KeyValuePair<string, string> item in myPar)
            {
                if (item.Value != "")
                {
                    if (first)
                        result += "?";
                    else
                        result += "&";
                    result += item.Key + "=" + item.Value;
                }
            }

            return "/search/" + result;
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

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public void ResetParameters()
        {
            InitialiseParameters();
        }

        public void ResetInit()
        {
            initialised = false;
        }

        public string CreateFileName(bool request = false)
        {
            return GenerateFileName(this, request);
        }

        public bool SaveToXml(string fileName = "lastrequest.req")
        {
            return SaveMyRequestObjectXML(this, fileName);
        }

        public IRequest LoadFromXML(string fileName = "lastrequest.req")
        {
            return LoadMyRequestObjectXML(fileName);
        }

        static public string GenerateFileName(ASVRequest inpObj, bool request = false)
        {
            string result = "";

            foreach (var item in inpObj.MyParameters)
            {
                if (item.Value == "" || item.Value == "10,11,12,111,13")
                    continue;

                result += item.Value;
            }
            return result;
            if (request)
                return result + ".req";

            return result + ".bcntr";
        }

        public override string ToString()
        {
            string result = "";

            foreach (var item in this.MyParameters)
            {
                if (item.Value == "" || item.Value == "10,11,12,111,13")
                    continue;

                result += "_" + item.Value;
            }

            return result.Substring(1);
        }

        static bool SaveMyRequestObjectXML(ASVRequest curObj, string fileName = "lastrequest.tasv.req")
        {
            bool result = false;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ASVRequest));

                using (Stream output = File.OpenWrite(fileName))
                {
                    formatter.Serialize(output, curObj);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                //throw;
            }

            return result;
        }

        static ASVRequest LoadMyRequestObjectXML(string fileName = "lastrequest.req")
        {
            ASVRequest result = null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ASVRequest));
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (ASVRequest)formatter.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        public string GetRequestStringPrintable()
        {
            string parSet = "";

            foreach (string item in this.MyParameters.Values)
                if (item.Length > 0)
                    parSet += ", " + item;
            if (parSet.Length > 2)
                parSet = parSet.Remove(0, 2);

            return parSet;
        }

        public IResponse MakeResponse()
        {
            return new ASVResponse(this);
        }

        public string AllParametersInString(string separator = "")
        {
            string parSet = "";

            foreach (string item in this.MyParameters.Values)
                if (item.Length > 0 & item != "10,11,12,111,13")
                    parSet += ", " + item;

            if (parSet.Length > separator.Length)
                parSet = parSet.Remove(0, separator.Length);

            return parSet;
        }
    }
}
