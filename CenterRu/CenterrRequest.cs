using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CenterRu
{
    [Serializable]
    public class CenterrRequest : ATorgRequest
    {
        override public string SiteName { get { return "Центр Реализации"; } }
        override public string Type { get { return "Centerr"; } }
        override public string ServiceURL { get { return "https://bankrupt.centerr.ru"; } }
        //public Exception LastError() { return lastError; }
        public CenterrRequest()
        {
            InitialiseParameters();
        }

        public CenterrRequest(string searchStr)
        {
            InitialiseParameters();
            SearchString = searchStr;
        }

        protected string _cviewstate;
        protected string _eventvalidation;

        override protected bool Initialize()
        {
            getBlankResponse();
            return initialised;
        }

        // Первый пустой запрос для получения параметров запроса _cviewstate и _eventvalidation
        override protected string getBlankResponse()   // используется для выполнения первого запроса, с целью получить идентификаторы сессии
        {
            string answer = makeAnPost();
            if (answer != null)
            {
                _cviewstate = getHtmlParameter(answer, "<input type=\"hidden\" name=\"__CVIEWSTATE\" id=\"__CVIEWSTATE\" value=\"", "\"");
                _eventvalidation = getHtmlParameter(answer, "<input type=\"hidden\" name=\"__EVENTVALIDATION\" id=\"__EVENTVALIDATION\" value=\"", "\"");
                if (_cviewstate.Length > 0 & _eventvalidation.Length > 0)
                    initialised = true;
            }

            return answer;
        }
        // Получение значений параметров запроса _cviewstate и _eventvalidation
        private string getHtmlParameter(string searchStr, string searchPatternStart, string searchPatternEnd)
        {
            int parStartInd = searchStr.IndexOf(searchPatternStart) + searchPatternStart.Length;
            int parEndInd = searchStr.IndexOf(searchPatternEnd, parStartInd);
            string result = searchStr.Substring(parStartInd, parEndInd - parStartInd);
            return result;
        }
        virtual public string ReqParamTitle { get { return "&ctl00$ctl00$MainExpandableArea$phExpandCollapse$PurchasesSearchCriteria$"; } }
        // строка запроса для CENTERR.RU
        override protected string myRawPostData()
        {
            if (!initialised)
                return null;
            string _CVIEWSTATE = _cviewstate;
            string _EVENTVALIDATION = _eventvalidation;
            return "ctl00$ctl00$BodyScripts$BodyScripts$scripts=ctl00$ctl00$MainExpandableArea$phExpandCollapse$UpdatePanel1|ctl00$ctl00$MainExpandableArea$phExpandCollapse$SearchButton&__EVENTTARGET=&__EVENTARGUMENT=&__CVIEWSTATE=" +
                _CVIEWSTATE +
                "&__VIEWSTATE=&__SCROLLPOSITIONX=0&__SCROLLPOSITIONY=0&__EVENTVALIDATION=" +
                _EVENTVALIDATION +
                "&ctl00$ctl00$LeftContentLogin$ctl00$Login1$UserName=&ctl00$ctl00$LeftContentLogin$ctl00$Login1$Password=&ctl00$ctl00$LeftContentSideMenu$mSideMenu$extAccordionMenu_AccordionExtender_ClientState=0" + ReqParamTitle + "vPurchaseLot_lotNumber_лота=" +
                MyParameters["vPurchaseLot_lotNumber"] +
                ReqParamTitle + "vPurchaseLot_purchaseNumber_торга=" +
                MyParameters["vPurchaseLot_purchaseNumber"] +
                ReqParamTitle + "vPurchaseLot_lotTitle_Наименованиелота=" +
                MyParameters["vPurchaseLot_lotTitle"] +
                ReqParamTitle + "vPurchaseLot_fullTitle_Наименованиеторга=" +
                MyParameters["vPurchaseLot_fullTitle"] +
                ReqParamTitle + "Party_contactName_AliasFullOrganizerTitle=" +
                MyParameters["Party_contactName"] +
                ReqParamTitle + "vPurchaseLot_InitialPrice_Начальнаяценаотруб=" +
                MyParameters["vPurchaseLot_InitialPrice"] +
                ReqParamTitle + "Party_inn_ИННорганизатора=" +
                MyParameters["Party_inn"] +
                ReqParamTitle + "vPurchaseLot_bargainTypeID_Типторгов$ddlBargainType=" +
                MyParameters["vPurchaseLot_bargainTypeID"] +
                ReqParamTitle + "Party_kpp_КППорганизатора=" +
                MyParameters["Party_kpp"] +
                ReqParamTitle + "vPurchaseLot_ParticipationFormID_Форматоргапосоставуучастников=" +
                MyParameters["vPurchaseLot_ParticipationFormID"] +
                ReqParamTitle + "Party_registeredAddress_Адресрегистрацииорганизатора=" +
                MyParameters["Party_registeredAddress"] +
                ReqParamTitle + "BargainType_PriceForm_Формапредставленияпредложенийоцене=" +
                MyParameters["BargainType_PriceForm"] +
                ReqParamTitle + "vPurchaseLot_BankruptName_Должник=" +
                MyParameters["vPurchaseLot_BankruptName"] +
                ReqParamTitle + "vPurchaseLot_purchaseStatusID_Статус=" +
                MyParameters["vPurchaseLot_purchaseStatusID"] +
                ReqParamTitle + "vPurchaseLot_BankruptINN_ИННдолжника=" +
                MyParameters["vPurchaseLot_BankruptINN"] +
                ReqParamTitle + "vPurchaseLot_BankruptRegionID_Региондолжника=" +
                MyParameters["vPurchaseLot_BankruptRegionID"] +
                ReqParamTitle + "vPurchaseLot_BankruptRegionID_Региондолжника_desc=" +
                MyParameters["vPurchaseLot_BankruptRegionID_desc"] +
                "&hiddenInputToUpdateATBuffer_CommonToolkitScripts=1&__ASYNCPOST=true&ctl00$ctl00$MainExpandableArea$phExpandCollapse$SearchButton=Искать торги";
        }
        private string makeAnPost(string url = "https://bankrupt.centerr.ru", string postData = "")
        {
            url = ServiceURL;
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

            lastAnswer = new StreamReader(response.GetResponseStream()).ReadToEnd();    // put result in lastAnswer to cache            

            return lastAnswer;
        }
        protected override string MakePost(string postData = "")
        {
            return makeAnPost(ServiceURL, postData);
        }

        override public string SearchString
        {
            get { return MyParameters["vPurchaseLot_lotTitle"]; }
            set { MyParameters["vPurchaseLot_lotTitle"] = value; }
        }

        override protected void InitialiseParameters()
        {
            MyParameters = new SerializableDictionary<string, string>
            {
                { "vPurchaseLot_lotNumber", "" },                        //  № лота
                { "vPurchaseLot_purchaseNumber", "" },                        //  № торга
                { "vPurchaseLot_lotTitle", "" },                   //  Наименование лота          // пирит
                { "vPurchaseLot_fullTitle", "" },                     //  Наименование торга         // прб
                { "Party_contactName", "" },                     //  AliasFullOrganizerTitle    // асв
                { "vPurchaseLot_InitialPrice", "" },                        //  Начальная Цена от руб
                { "Party_inn", "" },                        //  ИНН Организатора
                { "vPurchaseLot_bargainTypeID", "10,11,12,111,13" },         //  Тип торгов
                { "Party_kpp", "" },                        //  КПП Организатора
                { "vPurchaseLot_ParticipationFormID", "" },                        //  Форма торга по составу участников
                { "Party_registeredAddress", "" },                        //  Адрес регистрации организатора
                { "BargainType_PriceForm", "" },                        //  Форма представления предложений о цене
                { "vPurchaseLot_BankruptName", "" },                        //  Должник
                { "vPurchaseLot_purchaseStatusID", "" },                        //  Статус
                { "vPurchaseLot_BankruptINN", "" },                        //  ИНН Должника
                { "vPurchaseLot_BankruptRegionID", "" },                        //  Регион Должника
                { "vPurchaseLot_BankruptRegionID_desc", "" }                        //  ИД Регион Должника
            };
        }

        override public string AllParametersInString(string separator = "")
        {
            string parSet = "";

            foreach (string item in this.MyParameters.Values)
                if (item.Length > 0 & item != "10,11,12,111,13")
                    parSet += separator + item;

            if (parSet.Length > separator.Length)
                parSet = parSet.Remove(0, separator.Length);

            return parSet;
        }

        public override string ToString()
        {
            return AllParametersInString("_");
        }

        override public IResponse MakeResponse()
        {
            return new CenterrResponse(this);
            //throw new NotImplementedException("Making Response from request will be implemented later");
        }
    }
}
