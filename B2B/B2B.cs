using IAuction;
using HtmlParser;
using System;
using System.Collections;
using System.Collections.Generic;

namespace B2B
{
    public class B2B : ATorg
    {
        private string baseUrl = "https://www.b2b-center.ru/";

        public B2B(Tag inpTag)
        {
            if (inpTag.ChildTags.Count != 4)
            {
                Exception e = new Exception("Unkonwon format! Must be contain 4 child Tags");
                return;
            }
            
            LotNameUrl = inpTag.ChildTags[0].ChildTags[0].Attributes["href"];
            LotNameStr = inpTag.ChildTags[0].ChildTags[0].ChildTags[0].Value;
            TorgType = LotNameStr.Substring(0, LotNameStr.IndexOf("№") - 1);
            LotNumberStr = LotNameStr.Substring(LotNameStr.IndexOf("№") + 1).TrimEnd();

            try
            {
                TorgName = inpTag.ChildTags[0].ChildTags[0].ChildTags[1].ChildTags[0].Value;
                Description = "";
                foreach (Tag item in inpTag.ChildTags[0].ChildTags[0].ChildTags[1].ChildTags[1].ChildTags)
                {
                    if (item.IsProto & !item.IsComment)
                        Description += item.Value;
                    else
                        Description += item.ChildTags[0].Value;
                }
            }
            catch (Exception e)
            {

                //throw;
            }
            
            //
            OrganizerStr = inpTag.ChildTags[1].ChildTags[0].ChildTags[0].Value;
            OrganizerUrl = inpTag.ChildTags[1].ChildTags[0].Attributes["href"];

            DateAcceptStart = inpTag.ChildTags[2].ChildTags[0].Value;
            DateAcceptFinish = inpTag.ChildTags[3].ChildTags[0].Value;

        }
        /*
        public B2B(_Tag item)
        {
            string str = item.InnerTags[0].InnerTags[0].Value; // надо распарсить на номер торга, тип торга, наименование торга и строку с ключевыми словами.

            str = str.Replace("<br>", Environment.NewLine);
            str = str.Replace("<br />", Environment.NewLine);

            TorgType = str.Substring(0, str.IndexOf("№")-1);
            str = str.Replace(TorgType, "");
            LotNumberStr = str.Substring(0, str.IndexOf("<"));
            str = str.Replace(LotNumberStr, "");

            if (str.Contains("<div class=\"search-results-title-desc\">"))  // правильный результат 
            {
                str = str.Replace("<div class=\"search-results-title-desc\">", "");

                int startIndx = str.IndexOf("<div style=");
                if (startIndx < 0)
                    startIndx = 0;
                LotNameStr = str.Substring(startIndx);

                str = str.Replace(LotNameStr, "");
                LotNameStr = ClearOfTags(LotNameStr);                

                str = str.Remove(0, str.IndexOf(">")+1);

                TorgName = ClearOfTags(str);
                TorgName = TorgName.Replace("Свернуть", "");
                TorgName = TorgName.Replace("Развернуть", "");

                if (TorgName == "")
                    TorgName = LotNameStr;
                    
            }            

            LotNameUrl = getAttributeValue(item.InnerTags[0].InnerTags[0].Attributes, "href");
            //string TorgUrl = item.InnerTags[0].InnerTags[0].Attributes[0].Value; // ищем Attributes[].Name = href // получаем ссылку на торг

            OrganizerStr = item.InnerTags[1].InnerTags[0].Value; // Наименование Организатора            
            OrganizerUrl = getAttributeValue(item.InnerTags[1].InnerTags[0].Attributes, "href");    // ссылка на Организатора
            //string sssUrl = item.InnerTags[1].InnerTags[0].Attributes[1].Value; // ищем Attributes[].Name = href // получаем ссылку на организатора

            DateAcceptStart = item.InnerTags[2].Value;         // Дата публикации
            DateAcceptFinish = item.InnerTags[3].Value;           // Дата окончания приема заявок
        }   
        */
        private string ClearOfTags(string inpString)
        {
            int startLength = inpString.Length;
            while (inpString.Contains("<") | inpString.Contains(">"))
            {
                int startTag = inpString.IndexOf("<");                
                if (startTag < 0)
                    startTag = 0;
                int endTag = inpString.IndexOf(">");
                if (endTag < 0)
                    endTag = 0;

                inpString = inpString.Remove(startTag, endTag - startTag+1);
                if (startLength == inpString.Length)    // прерывание на случай рекурсии
                    break;
                startLength = inpString.Length;
            }
            return inpString;
        }

        /*private string getAttributeValue(IEnumerable Attributes, string attName)
        {
            string result = "";
            foreach (tagAttribute attr in Attributes)
                if (attr.Name == "href")
                {
                    result = attr.Value;
                    break;
                }
            return result;
        }
        */
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }

        public string DateAcceptStart { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string OrganizerStr { get; private set; }
        public string OrganizerUrl { get; private set; }
        public string TorgType { get; private set; }
        public string TorgName { get; private set; }
        public string Description { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is B2B))
                return false;
            B2B curObj = (B2B)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.LotNumberStr == curObj.LotNumberStr & 
                this.DateAcceptStart == curObj.DateAcceptStart &
                this.DateAcceptFinish == curObj.DateAcceptFinish &
                this.OrganizerStr == curObj.OrganizerStr &
                this.OrganizerUrl == curObj.OrganizerUrl &
                this.TorgType == curObj.TorgType &                
                this.TorgName == curObj.TorgName)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgName);            

            internalID = hashCode.ToString();

            return base.GetHashCode();
        }

        public override string ToString(bool html)
        {
            //throw new NotImplementedException();

            string result = "";
            string formatStr = @"{1}" + ";" +
                    @"{2}" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + Environment.NewLine +
                    @"{0}" + ";" +
                    @"{0}" + ";" +
                    @"{0}" + ";" +
                    @"{4}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" + Environment.NewLine;

            if (html)
                formatStr = "<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +
                    @"<a href =""{0}"">{2}</a>" + "</td><td>" +
                    @"<a href =""{0}"">{3}</a>" + "</td><td>" +
                    @"<a href =""{4}"">{5}</a>" + "</td><td>" +
                    @"{6}" + "</td><td>" +
                    @"{7}" + "</td><td>" +
                    @"{8}" + "</td></tr>";

            result += String.Format(formatStr,
                baseUrl + LotNameUrl,
                LotNumberStr,
                TorgName,
                LotNameStr,
                baseUrl + OrganizerUrl,
                OrganizerStr,
                TorgType,
                DateAcceptStart,
                DateAcceptFinish);

            /*
            if (html)
                result += String.Format("<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +
                    @"<a href =""{0}"">{2}</a>" + "</td><td>" +
                    @"<a href =""{0}"">{3}</a>" + "</td><td>" +
                    @"<a href =""{4}"">{5}</a>" + "</td><td>" +
                    @"{6}" + "</td><td>" +
                    @"{7}" + "</td><td>" +                    
                    @"{8}" + "</td></tr>",
                    baseUrl + LotNameUrl,
                    LotNumberStr,
                    TorgName,
                    LotNameStr,                    
                    baseUrl + OrganizerUrl,
                    OrganizerStr,
                    Type,
                    DateAcceptStart,
                    DateAcceptFinish
                    );
            else
                result += String.Format(
                    @"{1}" + ";" +
                    @"{2}" + ";" +
                    @"{3}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + Environment.NewLine +
                    @"{0}" + ";" +
                    @"{0}" + ";" +
                    @"{0}" + ";" +
                    @"{4}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" + Environment.NewLine,
                    baseUrl + LotNameUrl,
                    LotNumberStr,
                    TorgName,
                    LotNameStr,
                    baseUrl + OrganizerUrl,
                    OrganizerStr,
                    Type,
                    DateAcceptStart,
                    DateAcceptFinish
                    );
                    */

            return result;
        }
    }
}
