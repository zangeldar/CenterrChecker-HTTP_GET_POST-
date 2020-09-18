using CenterRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.Text;

namespace UTender
{
    [Serializable]
    public class UTenderRequest:CenterrRequest
    {
        override public string SiteName { get { return "Ю-Тендер"; } }
        override public string Type { get { return "uTender"; } }
        override public string SiteURL { get { return "http://www.utender.ru/"; } }

        override public string ReqParamTitle { get { return "&ctl00$ctl00$MainExpandableArea$phExpandCollapse$scPurchaseAllSearch$"; } }        

        public UTenderRequest() : base() { }
        public UTenderRequest(string searchStr) : base(searchStr) { }

        override protected string myRawPostData()
        {
            if (!initialised)
                return null;
            string _CVIEWSTATE = _cviewstate;
            string _EVENTVALIDATION = _eventvalidation;
            return "ctl00$ctl00$BodyScripts$BodyScripts$scripts=ctl00$ctl00$MainExpandableArea$phExpandCollapse$UpdatePanel1|ctl00$ctl00$MainExpandableArea$phExpandCollapse$btnSearch" +                
                "&ctl00$ctl00$LeftContentLogin$ctl00$Login1$UserName=&ctl00$ctl00$LeftContentLogin$ctl00$Login1$Password=&ctl00$ctl00$LeftContentSideMenu$mSideMenu$extAccordionMenu_AccordionExtender_ClientState=0" + 
                ReqParamTitle + "vPurchaseLot_lotNumber_лота=" +
                    MyParameters["vPurchaseLot_lotNumber"] +
                ReqParamTitle + "vPurchaseLot_purchaseNumber_торга=" +
                    MyParameters["vPurchaseLot_purchaseNumber"] +
                ReqParamTitle + "vPurchaseLot_lotTitle_Наименованиелота=" +
                    MyParameters["vPurchaseLot_lotTitle"] +
                ReqParamTitle + "vPurchaseLot_fullTitle_Наименованиеторга=" +
                    MyParameters["vPurchaseLot_fullTitle"] +                
                ReqParamTitle + "vPurchaseLot_procurementClassifierID_Категориялота=" +
                    //MyParameters["vPurchaseLot_InitialPrice"] +
                    "" +
                ReqParamTitle + "vPurchaseLot_procurementClassifierID_Категориялота_desc=" +
                    //MyParameters["vPurchaseLot_InitialPrice"] +
                    "" +
                ReqParamTitle + "vPurchaseLot_InitialPrice_Начальнаяценаотруб=" +
                    MyParameters["vPurchaseLot_InitialPrice"] +                
                ReqParamTitle + "Party_contactName_AliasFullOrganizerTitle=" +
                  MyParameters["Party_contactName"] +
                ReqParamTitle + "vPurchaseLot_bargainTypeID_Типторгов$ddlBargainType=" +
                    MyParameters["vPurchaseLot_bargainTypeID"] +
                ReqParamTitle + "Party_inn_ИННорганизатора=" +
                    MyParameters["Party_inn"] +
                ReqParamTitle + "BargainType_PriceForm_Формапредставленияпредложенийоцене=" +
                    MyParameters["BargainType_PriceForm"] +
                ReqParamTitle + "Party_kpp_КППорганизатора=" +
                    MyParameters["Party_kpp"] +
                ReqParamTitle + "vPurchaseLot_purchaseStatusID_Статус=" +
                    MyParameters["vPurchaseLot_purchaseStatusID"] +
                ReqParamTitle + "Party_registeredAddress_Адресрегистрацииорганизатора=" +
                    MyParameters["Party_registeredAddress"] +
                //ReqParamTitle + "vPurchaseLot_ParticipationFormID_Форматоргапосоставуучастников=" +
                  //  MyParameters["vPurchaseLot_ParticipationFormID"] +
                ReqParamTitle + "vPurchaseLot_BankruptName_Должник=" +
                    MyParameters["vPurchaseLot_BankruptName"] +                
                ReqParamTitle + "vPurchaseLot_BankruptINN_ИННдолжника=" +
                    MyParameters["vPurchaseLot_BankruptINN"] +
                ReqParamTitle + "vPurchaseLot_BankruptRegionID_Региондолжника=" +
                    MyParameters["vPurchaseLot_BankruptRegionID"] +
                ReqParamTitle + "vPurchaseLot_BankruptRegionID_Региондолжника_desc=" +
                    MyParameters["vPurchaseLot_BankruptRegionID_desc"] +
                "&hiddenInputToUpdateATBuffer_CommonToolkitScripts=1" +
                "&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=&__SCROLLPOSITIONX=0&__SCROLLPOSITIONY=0&__EVENTVALIDATION=" +
                _EVENTVALIDATION +
                "&__CVIEWSTATE=" + 
                _CVIEWSTATE + 
                "&__ASYNCPOST=true&ctl00$ctl00$MainExpandableArea$phExpandCollapse$btnSearch=Искать";
        }        

        override public IResponse MakeResponse()
        {
            return new UTenderResponse(this);            
        }
    }
}
