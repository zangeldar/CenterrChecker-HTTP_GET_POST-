using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;

namespace RTSTender
{
    [Serializable]
    public class RTSTender : ATorg
    {
        //const string baseUrl = "https://www.rts-tender.ru/"; 
        private void MyInitialize()
        {
        }
        public RTSTender(Tag inpTag, IRequest myReq):base(myReq)
        {
            MyInitialize();
            string tmpStr = "";
            string sepStr = " | ";
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__cell")))
            {
                if (item.IsProto & !item.IsComment)
                    tmpStr += item.Value + sepStr;
                else
                    foreach (Tag inItem in item.LookForChildTag(null))
                        if (!inItem.IsComment)
                            tmpStr += inItem.Value + sepStr;
                foreach (Tag inItem in item.LookForChildTag("span", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "link")))
                {
                    if (inItem.Attributes.ContainsKey("onclick"))
                        if (inItem.Attributes["onclick"] != "")
                            NoteUrl = inItem.Attributes["onclick"].Replace("window.open(\"", "").Replace("\",\"_blank\")", "");
                }
            }
            NoteUrl = baseUrl + NoteUrl.Replace(baseUrl, "");

            tmpStr = tmpStr.Remove(tmpStr.LastIndexOf(sepStr));
            NoteStr = tmpStr.Replace("\n", "").Replace("\t", "");
            while (NoteStr.Contains("  "))
                NoteStr = NoteStr.Replace("  ", " ");

            foreach (Tag item in inpTag.LookForChildTag("a", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "button-red")))
            {
                if (item.Attributes.ContainsKey("href"))
                    if (item.Attributes["href"] != "")
                        LotNameUrl = item.Attributes["href"];
            }
            LotNameUrl = baseUrl + LotNameUrl.Replace(baseUrl, "");

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__about")))
            {
                foreach (Tag inItem in item.LookForChildTag("span", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "link")))
                {
                    if (inItem.Attributes.ContainsKey("onclick"))
                        if (inItem.Attributes["onclick"] != "")
                            LotNumberUrl = inItem.Attributes["onclick"].Replace("window.open(\"", "").Replace("\",\"_blank\")", "");
                    foreach (Tag inInItem in inItem.LookForChildTag(null))
                        if (!inInItem.IsComment)
                            LotNumberStr = inInItem.Value;
                }
            }
            LotNumberUrl = baseUrl + LotNumberUrl.Replace(baseUrl, "");

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__title")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                    if (!inItem.IsComment)
                        LotNameStr += inItem.Value + " ";                
            }
            while (LotNameStr.Contains("  "))
                LotNameStr = LotNameStr.Replace("  ", " ");

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__properties-name")))
            {
                string caseSw = "";
                foreach (Tag inItem in item.LookForChildTag(null))
                    if (!inItem.IsComment)
                        caseSw += inItem.Value;
                string value = "";
                foreach (Tag inItem in item.Parent.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__properties-desc")))
                    foreach (Tag inInItem in inItem.LookForChildTag(null))
                        if (!inInItem.IsComment)
                            value += inInItem.Value;

                while (value.Contains("  "))
                    value = value.Replace("  ", " ");

                switch (caseSw)
                {
                    case "НАЧАЛЬНАЯ ЦЕНА":
                        PriceStart = value;
                        break;
                    case "ОБЕСПЕЧЕНИЕ ЗАЯВКИ":
                        GarantAcc = value;
                        break;
                    case "ОБЕСПЕЧЕНИЕ КОНТРАКТА":
                        GarantContract = value;
                        break;
                    case "СТАТУС":
                        Status = value;
                        break;
                    default:
                        break;
                }

            }

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__info")))
                foreach (Tag inItem in item.LookForChildTag("time"))
                    if (inItem.Attributes.ContainsKey("itemprop"))
                    {
                        string value = "";
                        if (inItem.Attributes.ContainsKey("datetime"))
                            value = inItem.Attributes["datetime"];
                        else
                            foreach (Tag inInItem in inItem.LookForChildTag(null))
                                if (!inInItem.IsComment)
                                    value += inInItem.Value;
                        switch (inItem.Attributes["itemprop"])
                        {
                            case "availabilityStarts":
                                DateAcceptStart = value;
                                break;
                            case "availabilityEnds":
                                DateAcceptFinish = value;
                                break;
                            default:
                                break;
                        }
                    }

            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__organization")))
            {
                string swValue = "";
                foreach (Tag inItem in item.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__organization-title")))
                {
                    
                    foreach (Tag inInItem in inItem.LookForChildTag(null))
                        if (!inInItem.IsComment)
                            swValue += inInItem.Value;
                }

                string url = "";
                foreach (Tag inItem in item.LookForChildTag("a", true))
                {
                    if (inItem.Attributes.ContainsKey("href"))
                        url = inItem.Attributes["href"];
                }

                string value = "";
                string adValue = "";
                foreach (Tag inItem in item.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-item__organization-main")))
                {
                    foreach (Tag inInItem in inItem.LookForChildTag("p"))
                    {
                        foreach (Tag inInInItem in inInItem.LookForChildTag(null))
                            if (!inInInItem.IsComment)
                                value += inInInItem.Value + " ";
                    }

                    foreach (Tag inInItem in inItem.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "content-address")))
                    {
                        foreach (Tag inInInItem in inInItem.LookForChildTag(null))
                            if (!inInInItem.IsComment)
                                adValue += inInInItem.Value + " ";
                    }
                }

                switch (swValue)
                {
                    case "ОРГАНИЗАТОР":
                        OrganisatorStr = value;
                        OrganisatorUrl = url;
                        url = "";
                        break;
                    case "ЗАКАЗЧИК":
                        OrgAcceptStr = value;
                        RegionStr = adValue.Replace("Адрес поставки:", "");
                        RegionUrl = url;
                        url = "";
                        break;
                    default:
                        break;
                }
            }
            OrganisatorUrl = baseUrl + OrganisatorUrl.Replace(baseUrl, "");
            RegionUrl = baseUrl + RegionUrl.Replace(baseUrl, "");
                        
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-content__row parent")))
                foreach (Tag inItem in item.LookForChildTag("table", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "card-table")))
                    Content = inItem.GetValue();

            TableRowMeans = new string[]
            {
                LotNumberStr,
                LotNameStr,
                HTMLParser.ClearHtml(OrganisatorStr, true),
                HTMLParser.ClearHtml(OrgAcceptStr, true),
                HTMLParser.ClearHtml(RegionStr, true),
                HTMLParser.ClearHtml(PriceStart, true),
                DateAcceptStart,
                DateAcceptFinish,
                NoteStr,
                Content
            };

            TableRowUrls = new string[]
            {
                baseUrl + LotNumberUrl.Replace(baseUrl,""),
                baseUrl + LotNameUrl.Replace(baseUrl,""),
                baseUrl + OrganisatorUrl.Replace(baseUrl,""),
                "",
                baseUrl + RegionUrl.Replace(baseUrl,""),
                "",
                "",
                "",
                baseUrl + NoteUrl.Replace(baseUrl,""),
                ""
            };
        }

        /*
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }
        */

        public string LotNumberUrl { get; private set; }
        public string NoteStr { get; private set; }
        public string NoteUrl { get; private set; }
        public string Status { get; private set; }
        public string GarantAcc { get; private set; }
        public string GarantContract { get; private set; }
        public string DateAcceptStart { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string DateLeave { get; private set; }
        public string OrgAcceptStr { get; private set; }
        public string OrganisatorStr { get; private set; }
        public string OrganisatorUrl { get; private set; }
        public string RegionStr { get; private set; }
        public string RegionUrl { get; private set; }
        public string Content { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is RTSTender))
                return false;
            return base.Equals(obj);

            /*
            RTSTender curObj = (RTSTender)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.PriceStart == curObj.PriceStart &
                this.LotNumberStr == curObj.LotNumberStr &

                this.LotNumberUrl == curObj.LotNumberUrl &
                this.NoteStr == curObj.NoteStr &
                this.NoteUrl == curObj.NoteUrl &
                this.Status == curObj.Status &
                this.GarantAcc == curObj.GarantAcc &
                this.GarantContract == curObj.GarantContract &
                this.DateAcceptStart == curObj.DateAcceptStart &
                this.DateAcceptFinish == curObj.DateAcceptFinish &                
                this.OrganisatorStr == curObj.OrganisatorStr &
                this.OrganisatorUrl == curObj.OrganisatorUrl &
                this.RegionStr == curObj.RegionStr &
                this.RegionUrl == curObj.RegionUrl &
                this.DateLeave == curObj.DateLeave &
                this.Content == curObj.Content)

                return true;
            return false;
            */
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);

            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NoteStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NoteUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GarantAcc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GarantContract);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganisatorStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganisatorUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RegionStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RegionUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateLeave);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);

            internalID = hashCode.ToString();

            return hashCode;
        }

        /*
        public override string ToString(bool html)
        {
            string result = "";
            string formatStr = @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{8}" + ";" +
                    @"{9}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + ";" +
                    @"{13}" + ";" +
                    @"{14}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"{4}" + ";" +
                    @"" + ";" +
                    @"{7}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{12}" + ";" +
                    @"" + Environment.NewLine;
            if (html)
                formatStr = "<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +                         // LotNumber
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +                         // LotName
                    @"<a href =""{4}"">{5}</a>" + "</td><td>" +                         // Organisator
                    @"{6}" + "</td><td>" +                                              // OrgAccept  
                    @"<a href =""{7}"">{8}</a>" + "</td><td>" +                         // Region
                    @"{9}" + "</td><td>" +                                              // PriceStart  
                    @"{10}" + "</td><td>" +                                             // DateAcceptStart  
                    @"{11}" + "</td><td>" +                                             // DateAcceptFinish  
                    @"<a href =""{12}"">{13}</a>" + "</td><td>" +                       // Note
                    @"{14}" + "</td></tr>";                       // Details
           

            // Секция                           // Section
            // Тип Торга                        // TorgType
            // Номер торга                      // LotNumberString
            // Наименование + ссылка            // LotNameStr + LotNameUrl
            // Организатор + ссылка             // Organizator + OrganizatorUrl
            // Начальная цена                   // PriceStart
            // Статус                           // Status
            // Дата публикации                  // DateAcceptStart
            // Дата окончания приема заявок     // DateAcceptFinish
            // Дата Проведения торгов           // DateTorgStart
            // Дата Подведения итогов           // DateTorgFinish
            // МСП                              // Note
            // ОКВЭД                            // OKVED
            // ОКПД                             // OKPD



            result += String.Format(formatStr,
                baseUrl + LotNumberUrl, LotNumberStr,
                baseUrl + LotNameUrl, LotNameStr,
                baseUrl + OrganisatorUrl, HTMLParser.ClearHtml(OrganisatorStr, html),
                HTMLParser.ClearHtml(OrgAcceptStr, html),
                baseUrl + RegionUrl, HTMLParser.ClearHtml(RegionStr, html),
                HTMLParser.ClearHtml(PriceStart, html),
                DateAcceptStart, 
                DateAcceptFinish,
                baseUrl + NoteUrl, NoteStr,
                Content
                );

            return result;
        }
        */
    }
}

