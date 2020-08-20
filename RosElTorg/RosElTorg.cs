using HtmlParser;
using IAuction;
using System;
using System.Collections.Generic;

namespace RosElTorg
{
    [Serializable]
    public class RosElTorg : ATorg
    {
        private string baseUrl = "https://www.roseltorg.ru/";
        //public string Type => "RosElTorg";
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }

        public string Status { get; private set; }
        public string Note { get; private set; }
        public string Section { get; private set; }
        public string Organisator { get; private set; }
        public string Region { get; private set; }
        public string TorgType { get; private set; }
        public string DateAcceptFinish { get; private set; }

        private void InitializeStrings()
        {
            LotNameStr = LotNameUrl = LotNumberStr = PriceStart = 
                Status = Note = Section = Organisator = Region = 
                TorgType = DateAcceptFinish = "";
        }

        public RosElTorg(Tag inpTag)
        {
            InitializeStrings();
            // Status
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "search-results__status status__icon--acceptance")))
            {
                if (item.Attributes.ContainsKey("title"))
                {
                    Status = item.Attributes["title"];
                    break;
                }                    
            }

            // LotNameUrl + LotNumberStr
            // LotNameStr            
            int k = 0;
            foreach (Tag item in inpTag.LookForChildTag("a", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "search-results__link")))
            {
                if (k == 0)
                {
                    foreach (Tag inItem in item.LookForChildTag(null))
                        LotNumberStr += inItem.Value;
                    /*
                    foreach (Tag inItem in item.ChildTags)
                    {
                        if (inItem.IsProto & !inItem.IsComment)
                        {
                            LotNumberStr = inItem.Value;
                            needBreak = true;
                            break;
                        }
                    }
                    */

                    if (item.Attributes.ContainsKey("href"))
                        LotNameUrl = item.Attributes["href"];
                }
                else if (k == 1)
                {
                    foreach (Tag inItem in item.LookForChildTag(null))
                        LotNameStr += inItem.Value;
                }
                else if (k >= 2)
                    break;                

                k++;
            }

            // Note + Section + Organisator
            foreach (Tag item in inpTag.LookForChildTag("p", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "search-results__tooltip")))
            {
                if (item.Attributes.ContainsKey("title"))
                {
                    switch (item.Attributes["title"])
                    {
                        case "Процедура для субъектов малого и среднего предпринимательства":
                            foreach (Tag inItem in item.LookForChildTag(null))
                                Note += inItem.Value;
                            break;
                        case "Торговая секция":
                            foreach (Tag inItem in item.LookForChildTag(null))
                                Section += inItem.Value;
                            break;
                        case "Заказчик":
                            foreach (Tag inItem in item.LookForChildTag(null))
                                Organisator += inItem.Value;
                            break;
                        case "Регион заказчика":
                            foreach (Tag inItem in item.LookForChildTag(null))
                                Region += inItem.Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            // Type
            foreach (Tag item in inpTag.LookForChildTag("p", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "search-results__type")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                    TorgType += inItem.Value;
            }

            // PriceStart
            foreach (Tag item in inpTag.LookForChildTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "search-results__price")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                    PriceStart += inItem.Value;                                        
            }

            // DateAcceptFinish
            foreach (Tag item in inpTag.LookForChildTag("time", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "search-results__time")))
            {
                foreach (Tag inItem in item.LookForChildTag(null))
                    DateAcceptFinish += inItem.Value;
            }

        }

        public override bool Equals(object obj)
        {
            if (!(obj is RosElTorg))
                return false;
            RosElTorg curObj = (RosElTorg)obj;

            if (this.internalID == curObj.internalID &
                this.LotNumberStr == curObj.LotNumberStr &                  // 0
                this.LotNameStr == curObj.LotNameStr &                      // 1
                this.LotNameUrl == curObj.LotNameUrl &                      // 2
                this.PriceStart == curObj.PriceStart &                      // 3
                this.Organisator == curObj.Organisator &                    // 4
                this.Region == curObj.Region &                              // 5
                this.DateAcceptFinish == curObj.DateAcceptFinish &          // 6
                this.Status == curObj.Status &                              // 7
                this.TorgType == curObj.TorgType &                          // 8
                this.Section == curObj.Section &                            // 9
                this.Note == curObj.Note)                                   // 10
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organisator);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Region);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);

            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            string result = "";
            string formatStr = @"{0}" + ";" +                    
                    @"{2}" + ";" +
                    @"{3}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{9}" + ";" +
                    @"{10}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"{1}" + ";" +
                    @"{1}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" + Environment.NewLine;
            if (html)
                formatStr = "<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +     // LotNumberStr + LotNameUrl
                    @"<a href =""{0}"">{2}</a>" + "</td><td>" +     // LotNameStr + LotNameUrl  
                    @"{3}" + "</td><td>" +                          // PriceStart  
                    @"{4}" + "</td><td>" +                          // Organisator
                    @"{5}" + "</td><td>" +                          // Region
                    @"{6}" + "</td><td>" +                          // DateAcceptFinish
                    @"{7}" + "</td><td>" +                          // Status
                    @"{8}" + "</td><td>" +                          // TorgType
                    @"{9}" + "</td><td>" +                          // Section
                    @"{10}" + "</td></tr>";                         // MSP            

            // Номер торга  + ссылка            // LotNumberString  + LotNameUrl
            // Наименование + ссылка            // LotNameStr + LotNameUrl
            // Начальная цена                   // PriceStart 
            // Организатор                      // Organizator
            // Регион                           // Region
            // Дата окончания приема заявок     // DateAcceptFinish            
            // Статус                           // Status 
            // Тип Торга                        // TorgType
            // Секция                           // Section            
            // МСП                              // Note

            result += String.Format(formatStr,
                baseUrl + LotNameUrl, 
                LotNumberStr,
                LotNameStr,
                PriceStart.Replace("&#8381;", " RUB."),
                Organisator,
                Region.Replace("\n", " | ").Replace("  ", " "),
                DateAcceptFinish,
                Status,
                TorgType,                
                Section,
                Note
                );

            return result;
        }
    }
}
