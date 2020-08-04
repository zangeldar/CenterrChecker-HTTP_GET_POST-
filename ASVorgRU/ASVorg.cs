using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace ASVorgRU
{
    [Serializable]
    public class ASVorg : ATorg
    {
        public ASVorg(Tag inpTag)
        {
            string tmpResult="";

            if (inpTag.ChildTags.Count > 2)
            {
                List<Tag> OneString = inpTag.LookForChildTag("a");
                foreach (Tag item in OneString)
                    if (item.Attributes.ContainsKey("href"))
                    {
                        LotNameUrl = item.Attributes["href"];
                        break;
                    }

                OneString = inpTag.ChildTags[0].LookForChildTag(null, true);
                foreach (Tag item in OneString)
                    tmpResult += item.Value;
                LotNameStr = tmpResult;

                tmpResult = "";

                OneString = inpTag.ChildTags[2].LookForChildTag(null, true);
                foreach (Tag item in OneString)
                    tmpResult += item.Value;
                Description = tmpResult;
            }

            string[] indicatorSection = LotNameUrl.Split('/');
            if (indicatorSection.Length > 1)
            switch (indicatorSection[1])
            {
                    case "insurance":
                        Section = "Страхование вкладов";
                        break;
                    case "liquidation":
                        Section = "Ликвидация банков";
                        break;
                    case "agency":
                        Section = "Другое";
                        break;
                default:
                        Section = "НЕИЗВЕСТНО";
                        break;
            }

        }

        /*
        public ASVorg(_Tag item, string section)
        {
            if (item.InnerTags.Count == 2)  // третий уровень - строки записи (их 2)
            {
                section = section.Remove(section.Length - 1);
                this.Section = section.Replace("Найдено в разделе", "");
                                 
                this.LotNameStr = item.InnerTags[0].Value;    // caption
                if (item.InnerTags[0].Attributes.Count > 0)
                    this.LotNameUrl = "https://www.asv.org.ru"+ item.InnerTags[0].Attributes[0].Value;
                                
                this.Content = item.InnerTags[1].Value;    // content                        
                LotName = new StringUri
                {
                    ItemString = LotNameStr,
                    ItemUri = LotNameUrl
                };
            }
        }
        */


        private string baseUrl = "https://www.asv.org.ru/";
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }
        public StringUri LotName;
        public string Section { get; private set; }
        public string Description { get; private set; }        

        public override bool Equals(object obj)
        {
            if (!(obj is ASVorg))
                return false;
            ASVorg curObj = (ASVorg)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.Section == curObj.Section &
                this.Description == curObj.Description)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -494222953;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNameUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);

            internalID = hashCode.ToString();

            return base.GetHashCode();
        }

        public override string ToString(bool html)
        {            
            string result = "";
            string formatStr = @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"{3}" + Environment.NewLine +
                    @"" + ";" +
                    @"{1}" + ";" +
                    @"" + ";" + Environment.NewLine;

            if (html)
                formatStr = "<tr><td>" +
                    @"{0}" + "</td><td>" +
                    @"<a href =""{1}"">{2}</a>" + "</td><td>" +
                    @"{3}" + "</td></tr>";

            result += String.Format(formatStr,
                    HTMLParser.ClearHtml(Section, html),
                    baseUrl + LotNameUrl, HTMLParser.ClearHtml(LotNameStr, html),
                    HTMLParser.ClearHtml(Description, html)
                    );         
            return result;
        }   
    }
}
