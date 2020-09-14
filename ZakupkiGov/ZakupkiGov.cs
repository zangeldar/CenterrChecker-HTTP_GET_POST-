using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;

namespace ZakupkiGov
{
    [Serializable]
    public class ZakupkiGov : ATorg
    {
        //private string baseUrl = "https://www.zakupki.gov.ru/";
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }
        ////
        public string Section { get; private set; }
        public string TorgType { get; private set; }
        public string Status { get; private set; }
        public string OrganizerStr { get; private set; }
        public string OrganizerUrl { get; private set; }
        public string DatePost { get; private set; }
        public string DateUpdate { get; private set; }
        public string DateFinishAccept { get; private set; }
        ////
        //public Exception LastError { get; private set; }

        public ZakupkiGov(Tag inpTag, IRequest myReq):base(myReq)
        {
            // registry-entry__header-top__title text-truncate
            List<Tag> workList;
            string workStr = "";

            // TorgType
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "registry-entry__header-top__title text-truncate"));
            foreach (Tag item in workList)
            {
                bool needBreak = false;
                foreach (Tag itemCh in item.ChildTags)
                {
                    if (itemCh.IsProto & !itemCh.IsComment)
                    {
                        TorgType = itemCh.Value;
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak)
                    break;
            }

            // TorgNumber
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "registry-entry__header-mid__number"));
            foreach (Tag item in workList)
            {
                bool needBreak = false;
                foreach (Tag itemCh in item.ChildTags)
                {
                    if (itemCh.Name == "a")
                    {                        
                        if (itemCh.Attributes.ContainsKey("href"))
                        {
                            LotNameUrl = itemCh.Attributes["href"];
                            LotNameUrl = LotNameUrl.Substring(0, LotNameUrl.LastIndexOf("&backUrl="));
                        }
                            

                        foreach (Tag itemChCh in itemCh.ChildTags)
                            if (itemChCh.IsProto & !itemChCh.IsComment)
                            {
                                LotNumberStr = itemChCh.Value;
                                needBreak = true;
                                break;
                            }
                        if (needBreak)
                            break;
                    }                    
                }
                if (needBreak)
                    break;
            }

            // Status
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "registry-entry__header-mid__title"));
            foreach (Tag item in workList)
            {
                bool needBreak = false;
                foreach (Tag itemCh in item.ChildTags)
                {
                    if (itemCh.IsProto & !itemCh.IsComment)
                    {
                        Status = itemCh.Value;
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak)
                    break;
            }

            // LotNameStr
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "registry-entry__body-value"));
            foreach (Tag item in workList)
            {
                string res = "";
                foreach (Tag itemCh in item.LookForChildTag(null))
                    res += itemCh.Value + " ";
                if (res != "")
                {
                    LotNameStr = res.Replace("  ", " ");
                    break;
                }                    
            }

            // Organizer
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "registry-entry__body-href"));
            foreach (Tag item in workList)
            {
                //bool needBreak = false;
                foreach (Tag itemCh in item.ChildTags)
                {
                    if (itemCh.Name == "a")
                    {
                        if (itemCh.Attributes.ContainsKey("href"))
                        {
                            OrganizerUrl = itemCh.Attributes["href"];
                            OrganizerUrl = OrganizerUrl.Substring(0, OrganizerUrl.LastIndexOf("&backUrl="));
                        }
                            

                        foreach (Tag itemChCh in itemCh.LookForChildTag(null))
                            if (itemChCh.IsProto & !itemChCh.IsComment)
                            {
                                OrganizerStr += itemChCh.Value+" ";
                                //needBreak = true;
                                //break;
                            }
                        //if (needBreak)
                          //  break;
                    }
                }
                //if (needBreak)
                  //  break;
            }
            OrganizerStr = OrganizerStr.Replace("  ", " ");

            // Price
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "price-block__value"));
            foreach (Tag item in workList)
            {
                bool needBreak = false;
                foreach (Tag itemCh in item.ChildTags)
                {
                    if (itemCh.IsProto & !itemCh.IsComment)
                    {
                        PriceStart = itemCh.Value;
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak)
                    break;
            }
                
                

            // Dates
            workList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "data-block__title"));
            List<Tag> tmpList = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "data-block__value"));
            if (workList.Count == tmpList.Count)
                for (int i = 0; i < workList.Count; i++)
                {
                    if (workList[i].ChildTags[0].Value == "Размещено")
                        DatePost = tmpList[i].ChildTags[0].Value;
                    else if (workList[i].ChildTags[0].Value == "Обновлено")
                        DateUpdate = tmpList[i].ChildTags[0].Value;
                    else if (workList[i].ChildTags[0].Value == "Окончание подачи заявок")
                        DateFinishAccept = tmpList[i].ChildTags[0].Value;
                    else
                        LastError = new Exception("Unexpected DATA in DATE segment!");
                }

            workStr = LotNameUrl.Replace("https://zakupki.gov.ru/", "").Replace("/epz/order/notice/", "");
            workStr = workStr.Remove(workStr.IndexOf("/"));
            switch (workStr)
            {
                case "ea44":
                    Section = "44-ФЗ";
                    break;
                case "223":
                    Section = "223-ФЗ";
                    break;
                case "ea615":
                    Section = "ПП РФ 615 (Капитальный ремонт)";
                    break;
                case "pgz":
                    Section = "94-ФЗ";
                    break;
                default:
                    Section = "UNKNOWN";
                    break;
            }
        }




        public override bool Equals(Object obj)
        {
            if (!(obj is ZakupkiGov))
                return false;
            ZakupkiGov curObj = (ZakupkiGov)obj;
            
            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.OrganizerStr == curObj.OrganizerStr &
                this.OrganizerUrl == curObj.OrganizerUrl &
                this.DateFinishAccept == curObj.DateFinishAccept &
                this.DatePost == curObj.DatePost &
                this.DateUpdate == curObj.DateUpdate &
                this.LotNumberStr == curObj.LotNumberStr &
                this.PriceStart == curObj.PriceStart &
                this.Section == curObj.Section &
                this.Status == curObj.Status &
                this.TorgType == curObj.TorgType)
                return true;
            
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateFinishAccept);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DatePost);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateUpdate);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);
            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            string result = "";
            string formatStr = @"{1}" + ";" +
                    @"{2}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{9}" + ";" +
                    @"{10}" + ";" +
                    @"{11}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"{0}" + ";" +
                    @"{0}" + ";" +
                    @"{3}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +                    
                    @"" + Environment.NewLine;
            if (html)
                formatStr = "<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +     // Номер + ссылка
                    @"<a href =""{0}"">{2}</a>" + "</td><td>" +     // Наименование + ссылка
                    @"<a href =""{3}"">{4}</a>" + "</td><td>" +     // Организатор + ссылка
                    @"{5}" + "</td><td>" +                          // Цена (если есть)
                    @"{6}" + "</td><td>" +                          // Статус
                    @"{7}" + "</td><td>" +                          // ДатаКонца
                    @"{8}" + "</td><td>" +                          // ДатаОбновления
                    @"{9}" + "</td><td>" +                          // ДатаПоста                    
                    @"{10}" + "</td><td>" +                          // ТипТорга
                    @"{11}" + "</td></tr>";                           // Секция  
            
            result += String.Format(formatStr,
                baseUrl + LotNameUrl.Replace("https://zakupki.gov.ru", ""), HTMLParser.ClearHtml(LotNumberStr, html),
                HTMLParser.ClearHtml(LotNameStr, html), 
                baseUrl + OrganizerUrl.Replace("https://zakupki.gov.ru", ""), HTMLParser.ClearHtml(OrganizerStr, html),
                HTMLParser.ClearHtml(PriceStart, html),
                HTMLParser.ClearHtml(Status, html),
                DateFinishAccept,
                DateUpdate, 
                DatePost,
                HTMLParser.ClearHtml(TorgType, html),
                HTMLParser.ClearHtml(Section, html)
                );
                
            
            return result;
        }
    }
}
