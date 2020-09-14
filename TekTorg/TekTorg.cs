using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;

namespace TekTorg
{
    [Serializable]
    public class TekTorg : ATorg
    {
        //private string baseUrl = "https://www.tektorg.ru/";
        //public Exception LastError { get; private set; }

        public TekTorg(Tag inpTag, IRequest myReq):base(myReq)
        {
            if (inpTag.Name == "div")
            {
                if (inpTag.Attributes.ContainsKey("class"))
                    if (inpTag.Attributes["class"] == "section-procurement__item")
                        FillByBlock(inpTag);
            }
            else if (true)
            {
                if (true)
                    if (true)
                        FillByTableRow(inpTag);
            }
            else
            {
                LastError = new Exception("Unknown format of TekTorg Tag");
                return;
            }
                
        }

        private void FillByBlock(Tag inpTag)
        {
            if (inpTag.ChildTags.Count != 3)
            {
                Exception e = new Exception("Unknown format! Must be contain 3 child Tags");
                LastError = e;
                return;
            }

            // SECTION
            foreach (Tag item in inpTag.LookForChildTag("i", false, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-logo")))
            {
                if (item.Attributes.ContainsKey("title"))
                {
                    Section = item.Attributes["title"];
                    break;
                }
            }

            //TorgNumber 
            bool needBreak = false;
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-numbers")))
            {
                if (!item.IsProto)
                    foreach (Tag inItem in item.LookForChildTag(null))
                    {
                        if (inItem.IsProto & !inItem.IsComment)
                        {
                            LotNumberStr = inItem.Value;
                            needBreak = true;
                            break;
                        }
                    }
                if (needBreak)
                    break;
            }

            // LotNameStr + LotNameUrl
            needBreak = false;
            foreach (Tag item in inpTag.LookForChildTag("a", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-title")))
            {
                if (item.Attributes.ContainsKey("href"))
                {
                    LotNameUrl = item.Attributes["href"];
                }
                if (!item.IsProto)
                    foreach (Tag inItem in item.LookForChildTag(null))
                    {
                        if (inItem.IsProto & !inItem.IsComment)
                        {
                            LotNameStr = inItem.Value;
                            needBreak = true;
                            break;
                        }
                    }
                if (needBreak)
                    break;
            }

            // Descripton parsing
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-description")))
            {
                string switchKey = "";
                string switchValue = "";
                string switchLink = "";
                if (item.ChildTags.Count >= 2)
                {
                    if (item.ChildTags[0].ChildTags.Count >= 1)
                    {
                        if (item.ChildTags[0].ChildTags[0].IsProto)
                            switchKey = item.ChildTags[0].ChildTags[0].Value;
                    }

                    if (item.ChildTags[1].ChildTags.Count >= 1)
                    {
                        if (item.ChildTags[1].ChildTags[0].IsProto)
                            switchValue = item.ChildTags[1].ChildTags[0].Value;
                        if (item.ChildTags[1].Attributes.ContainsKey("href"))
                            switchLink = item.ChildTags[1].Attributes["href"];
                    }
                }
                else
                    continue;

                switch (switchKey)
                {
                    case "":
                        break;
                    case "Организатор:":
                        OrganisatorStr = switchValue;
                        OrganisatorUrl = switchLink;
                        break;
                    case "ОКВЭД/ОКВЭД2:":
                        OKVED = switchValue;
                        //OKVEDUrl = switchLink;
                        break;
                    case "ОКДП/ОКПД2:":
                        OKDP = switchValue;
                        //OKDPUrl = switchLink;
                        break;
                    default:
                        break;
                }
            }

            // STATUS parsing
            needBreak = false;
            foreach (Tag item in inpTag.LookForChildTag("span", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-dateTo")))
            {
                if (!item.IsProto)
                    foreach (Tag  inItem in item.ChildTags)
                    {
                        if (inItem.IsProto)
                        {
                            Status = inItem.Value;
                            needBreak = true;
                            break;
                        }
                    }
                if (needBreak)
                    break;
            }

            // DATEs parsing
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-dateTo")))
            {
                string switchKey = "";
                string switchValue = "";
                
                if (item.ChildTags.Count >= 2)
                {
                    if (item.ChildTags[0].ChildTags.Count >= 1)
                    {
                        if (item.ChildTags[0].ChildTags[0].IsProto)
                            switchKey = item.ChildTags[0].ChildTags[0].Value;
                    }

                    if (item.ChildTags[1].IsProto)
                    {
                        //switchValue = item.ChildTags[1].ChildTags[0].Value;        ]
                        switchValue = item.ChildTags[1].Value;
                    }
                }
                else
                    continue;

                switch (switchKey)
                {
                    case "":
                        break;
                    case "Дата публикации процедуры:":
                        DateAcceptStart = switchValue;                        
                        break;
                    case "Дата окончания приема заявок:":
                        DateAcceptFinish = switchValue;                        
                        break;
                    case "Дата проведения торгов:":
                        DateTorgStart = switchValue;
                        break;
                    case "Подведение итогов не позднее:":
                        DateTorgFinish = switchValue;
                        break;
                        /*
                    case "Статус:":
                        Status = switchValue;
                        break;
                        */
                    default:
                        break;
                }
            }

            // TorgType + PriceStart
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-request-price")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                {
                    TorgType = inItem.Value;
                    break;
                }                    
            }

            // PriceStart
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-totalPrice")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                {
                    PriceStart = inItem.Value;
                    break;
                }
            }

            //MSP
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "section-procurement__item-msp")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                {
                    Note = inItem.Value;
                    break;
                }
            }
        }

        private void FillByTableRow(Tag inpTag)
        {
            throw new NotImplementedException();
        }

        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }

        public string Section { get; private set; }
        public string OrganisatorStr { get; private set; }
        public string OrganisatorUrl { get; private set; }
        public string OKVED { get; private set; }
        public string OKDP { get; private set; }
        public string DateAcceptStart { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string DateTorgStart { get; private set; }
        public string DateTorgFinish { get; private set; }
        public string Status { get; private set; }
        public string TorgType { get; private set; }
        public string Note { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TekTorg))
                return false;
            TekTorg curObj = (TekTorg)obj;

            if (this.internalID == curObj.internalID &
                this.Section == curObj.Section &
                this.TorgType == curObj.TorgType &
                this.LotNumberStr == curObj.LotNumberStr &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.OrganisatorStr == curObj.OrganisatorStr &
                this.OrganisatorUrl == curObj.OrganisatorUrl &
                this.PriceStart == curObj.PriceStart &
                this.Status == curObj.Status &
                this.DateAcceptStart == curObj.DateAcceptStart &
                this.DateAcceptFinish == curObj.DateAcceptFinish &
                this.DateTorgStart == curObj.DateTorgStart &
                this.DateTorgFinish == curObj.DateTorgFinish &
                this.Note == curObj.Note &
                this.OKDP == curObj.OKDP &
                this.OKVED == curObj.OKVED)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganisatorStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganisatorUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateTorgStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateTorgFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OKDP);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OKVED);

            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            string tmpStr = OKDP + "<br>" + OKVED + "<br>" + Note;
            string result = "";
            string formatStr = @"{0}" + ";" +
                    @"{1}" + ";" +
                    @"{2}" + ";" +
                    @"{4}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{9}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + ";" +
                    @"{12}" + ";" +                    
                    @"{13}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + Environment.NewLine;
            if (html)
                formatStr = "<tr><td>" +
                    @"{0}" + "</td><td>" +                          // Section
                    @"{1}" + "</td><td>" +                          // TorgType  
                    @"{2}" + "</td><td>" +                          // LotNumberString  
                    @"<a href =""{3}"">{4}</a>" + "</td><td>" +     // LotNameStr + LotNameUrl
                    @"<a href =""{5}"">{6}</a>" + "</td><td>" +     // Organizator + OrganizatorUrl
                    @"{7}" + "</td><td>" +                          // PriceStart
                    @"{8}" + "</td><td>" +                          // Status
                    @"{9}" + "</td><td>" +                          // DateAcceptStart
                    @"{10}" + "</td><td>" +                         // DateAcceptFinish
                    @"{11}" + "</td><td>" +                         // DateTorgStart
                    @"{12}" + "</td><td>" +                         // DateTorgFinish
                    @"{13}" + "</td></tr>";                         // OKPD + OKVED + MSP            
            else
                tmpStr = tmpStr.Replace("<br>", " | ");

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
                Section,
                TorgType,
                LotNumberStr.Replace("Номер закупки на сайте ЭТП:", ""),
                GetFullUrl(LotNameUrl), LotNameStr,
                GetFullUrl(OrganisatorUrl), OrganisatorStr,
                PriceStart,
                Status,
                DateAcceptStart,
                DateAcceptFinish,
                DateTorgStart,
                DateTorgFinish,
                tmpStr
                );

            return result;
        }

        private string GetFullUrl(string srcUrl)
        {
            string result = baseUrl;

            if (srcUrl == null | srcUrl == "")
                result = baseUrl;
            else if (srcUrl.StartsWith("http"))
                result = srcUrl;
            else if (srcUrl.StartsWith("/"))
                result += srcUrl;
            else if (srcUrl.StartsWith("?"))
                result += "procedures/" + srcUrl;
            else
                result = baseUrl;

            return result.Replace("//", "/");
        }
    }
}
