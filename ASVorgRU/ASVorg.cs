using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace ASVorgRU
{
    [Serializable]
    public class ASVorg : ATorg
    {
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


        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }
        public StringUri LotName;
        public string Section { get; private set; }
        public string Content { get; private set; }        

        public override bool Equals(object obj)
        {
            if (!(obj is ASVorg))
                return false;
            ASVorg curObj = (ASVorg)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.Section == curObj.Section &
                this.Content == curObj.Content)
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
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);

            internalID = hashCode.ToString();

            return base.GetHashCode();
        }

        public override string ToString(bool html)
        {
            string baseUri = "https://www.asv.org.ru";
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
                    @"{0}" + "</td><td>" +
                    @"<a href =""{1}"">{2}</a>" + "</td><td>" +
                    @"{3}" + "</td></tr>",
                    Section,
                    baseUri + LotNameUrl, LotNameStr,
                    Content
                    );
            else
                result += String.Format(
                    @"{0}" + ";" +
                    @"{2}" + ";" +
                    @"{3}" + Environment.NewLine +
                    @"" + ";" +
                    @"{1}" + ";" +
                    @"" + ";" + Environment.NewLine,
                    Section,
                    baseUri + LotNameUrl, LotNameStr,
                    Content
                    );         
            return result;
        }
    }
}
