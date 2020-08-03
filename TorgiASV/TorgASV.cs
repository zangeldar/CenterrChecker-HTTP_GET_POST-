using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace TorgiASV
{
    [Serializable]
    public class TorgASV : ATorg
    {
        private string baseUrl = "https://www.torgiasv.ru/";
        public Exception LastError { get; private set; }
        public TorgASV(List<_Tag> itemsList)
        {
            /*
            LotName = new StringUri
            {
                ItemString = itemsList[2].InnerTags[0].InnerTags[0].Value.Replace("  ", "").Replace("\n", ""),
                ItemUri = "https://torgiasv.ru" + itemsList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "")
            };
            */
            LotNameStr = itemsList[2].InnerTags[0].InnerTags[0].Value.Replace("  ", "").Replace("\n", "");
            LotNameUrl = "https://torgiasv.ru" + itemsList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "");

            //
            //AsvID = .Substring();
            string urlID = itemsList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "");
            int startID = urlID.Substring(0, urlID.Length - 1).LastIndexOf('/');
            urlID = urlID.Substring(startID).Replace("/", "");
            //AsvID = int.Parse(urlID);
            internalID = urlID;
            //
            LotDesc = itemsList[3].Value;
            Organisator = itemsList[4].Value;
            TorgRegion = itemsList[5].Value;
            PriceStart = itemsList[7].InnerTags[0].Value.Replace("<span class=\"text-muted\">", "");
            LotNumberStr = itemsList[9].Value;
        }

        public TorgASV(Tag inpTag)
        {            
            List<Tag> Parents = inpTag.LookForParentTag("div", true, new KeyValuePair<string, string>("class", "lot-catalog__group"));
            Tag lowerParent = null;
            foreach (Tag item in Parents)
            {                
                if (lowerParent == null || item.Level < lowerParent.Level)
                    lowerParent = item;
            }
            try
            {                
                // ищем ссылку "ПОКАЗАТЬ ВСЕ"
                foreach (Tag item in lowerParent.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "full-link-block full-link-block--lg")))
                {
                    if (item.ChildTags.Count > 0)
                        if (item.ChildTags[0].Name == "a")
                            if (item.ChildTags[0].Attributes.ContainsKey("href"))
                            {
                                TorgTypeUrl = item.ChildTags[0].Attributes["href"];
                                break;
                            }
                }

                // ищем заголовок
                lowerParent = lowerParent.LookForChildTag("h3", true, new KeyValuePair<string, string>("class", "lot-catalog__group-title bold"))[0];
                // Ищем название торгов
                TorgTypeStr = lowerParent.ChildTags[0].ChildTags[0].Value;
                //TorgTypeUrl = lowerParent.ChildTags[0].Attributes["href"];
                //lowerParent.Parent.Parent.ChildTags[2] /* lot-catalog__group-foot */ .ChildTags[0].ChildTags[0].ChildTags[0].ChildTags[0] /* a class="full-link" */.Attributes["href"];
            }
            catch (Exception e)
            {
                LastError = e;
                //throw;
            }

            try
            {
                lowerParent = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "multitext-crop"))[0];
                foreach (Tag item in lowerParent.LookForChildTag("h5", true, new KeyValuePair<string, string>("class", "item-head__title lot-catalog-item__head-title bold"))[0].ChildTags)
                {
                    if (item.IsComment)
                        continue;
                    if (item.IsProto)
                    {
                        LotNameStr = item.Value;
                        break;
                    } else
                        foreach (Tag inItem in item.LookForChildTag(null))
                        {
                            if (inItem.IsProto & !inItem.IsComment)
                            {
                                LotNameStr = inItem.Value;
                                if (((Tag)inItem.Parent).Attributes.ContainsKey("href"))
                                    LotNameUrl = ((Tag)inItem.Parent).Attributes["href"];
                                break;
                            }
                        }
                }
                //LotNameUrl = "";                // для ФО заполнять ссылку из Активов
                
                foreach (Tag item in lowerParent.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "lot-catalog-item__organization"))[0].ChildTags)
                {
                    if (item.IsProto & !item.IsComment)
                    {
                        Organisator = item.Value;
                        break;
                    }
                }

                foreach (Tag item in lowerParent.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "multitext-crop__item lot-catalog-item__foot text-muted"))[0].ChildTags)
                {
                    if (item.IsProto & !item.IsComment)
                    {
                        TorgRegion = item.Value;
                        break;
                    }
                }

                List<Tag> testList = lowerParent.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "multitext-crop__item multitext-crop__toggle lot-catalog-item__cont"));
                if (testList.Count > 0)
                    foreach (Tag item in testList[0].ChildTags)
                    {
                        if (item.IsProto & !item.IsComment)
                        {
                            LotDesc = item.Value;
                            break;
                        }
                    }
            }
            catch (Exception e)
            {
                LastError = e;
                //throw;
            }

            // lot-catalog-item__column lot-catalog-item__column--action
            try
            {
                lowerParent = inpTag.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "lot-catalog-item__column lot-catalog-item__column--action"))[0];
                List<Tag> testTags = lowerParent.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "lot-catalog-item__organization lot-catalog-item__organization--fo"));                
                if (testTags.Count > 0)
                {
                    //TorgTypeStr = "ФО / Агентство";
                    foreach (Tag item in testTags)
                    {
                        // заполняем ссылки на лоты и активы
                        if (item.ChildTags[0].Attributes.ContainsKey("href"))
                        {
                            if (item.ChildTags[0].Attributes["href"].Contains("pre-sale"))
                            {
                                LotNameUrl = item.ChildTags[0].Attributes["href"];                                
                            }                                
                            else
                            {
                                LotNumberUrl = item.ChildTags[0].Attributes["href"];
                                LotNumberStr = item.ChildTags[0].ChildTags[0].Value;
                            }   
                        }
                    }
                }                    
                else
                {
                    testTags = lowerParent.LookForChildTag("div", true, new KeyValuePair<string, string>("class", "lot-catalog-item__price price"));
                    if (testTags.Count > 0)
                    {
                        //TorgTypeStr = "Лоты (Продажа имущества) - в наименовании / описании содержится искомый текст";
                        foreach (Tag item in testTags)
                        {
                            // заполняем прайс и номер лота
                            PriceStart = item.ChildTags[1].ChildTags[0].ChildTags[0].Value + item.ChildTags[1].ChildTags[0].ChildTags[1].ChildTags[0].Value;
                            LotNumberStr = ((Tag)item.Parent).ChildTags[2].ChildTags[0].Value;
                        }
                    }
                    else
                    {
                        //TorgTypeStr = "Активы (Скоро в продаже) - в наименовании / описании содержится искомый текст";
                    }
                }
            }
            catch (Exception e)
            {
                LastError = e;
                //throw;
            }


        }

        override public string internalID { get; protected set; }        
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public string Organisator { get; private set; }
        public string TorgRegion { get; private set; }
        public string TorgTypeStr { get; private set; }
        public string TorgTypeUrl { get; private set; }
        public string LotDesc { get; private set; }    
        override public string LotNumberStr { get; protected set; }
        public string LotNumberUrl { get; private set; }
        override public string PriceStart { get; protected set; }        

        public override bool Equals(Object obj)
        {
            if (!(obj is TorgASV))
                return false;
            TorgASV curObj = (TorgASV)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.Organisator == curObj.Organisator &
                this.TorgRegion == curObj.TorgRegion &
                this.TorgTypeStr == curObj.TorgTypeStr &
                this.TorgTypeUrl == curObj.TorgTypeUrl &
                this.LotDesc == curObj.LotDesc &
                this.LotNumberStr == curObj.LotNumberStr &
                this.LotNumberUrl == curObj.LotNumberUrl &
                this.PriceStart == curObj.PriceStart)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();            
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organisator);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgRegion);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgTypeStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgTypeUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotDesc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);            

            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            string result = "";
            string formatStr = @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{9}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{8}" + Environment.NewLine;
            if (html)
                formatStr = "<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +     // Секция + ссылка
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +     // Наименование + ссылка
                    @"{4}" + "</td><td>" +                          // Описание (если есть)
                    @"{5}" + "</td><td>" +                          // Регион
                    @"{6}" + "</td><td>" +                          // Организатор
                    @"{7}" + "</td><td>" +                          // Цена (если есть)
                    @"<a href =""{8}"">{9}</a>" + "</td><td>";      // Лоты (если есть) + ссылка (если есть)   
                
            result += String.Format(formatStr,
                TorgTypeStr, TorgTypeUrl,
                LotNameStr, LotNameUrl,
                LotDesc,
                TorgRegion,
                Organisator,
                PriceStart,
                LotNumberStr, LotNumberUrl
                );                

            /*
            if (html)
                result += String.Format("<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +     // Секция + ссылка
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +     // Наименование + ссылка
                    @"{4}" + "</td><td>" +                          // Описание (если есть)
                    @"{5}" + "</td><td>" +                          // Регион
                    @"{6}" + "</td><td>" +                          // Организатор
                    @"{7}" + "</td><td>" +                          // Цена (если есть)
                    @"<a href =""{8}"">{9}</a>" + "</td><td>",      // Лоты (если есть) + ссылка (если есть)   
                    TorgTypeStr, TorgTypeUrl,
                    LotNameStr, LotNameUrl,                                        
                    LotDesc,
                    TorgRegion,
                    Organisator,
                    PriceStart,
                    LotNumberStr, LotNumberUrl
                    );
            else
                result += String.Format(
                    @"{1}" + ";" +
                    @"{3}" + ";" +                    
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{9}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"{8}" + Environment.NewLine,
                    TorgTypeStr, TorgTypeUrl,
                    LotNameStr, LotNameUrl,
                    LotDesc,
                    TorgRegion,
                    Organisator,
                    PriceStart,
                    LotNumberStr, LotNumberUrl
                    );
            */
            return result;
        }
    }
}
