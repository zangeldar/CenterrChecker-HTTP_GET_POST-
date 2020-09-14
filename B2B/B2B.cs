using IAuction;
using HtmlParser;
using System;
using System.Collections;
using System.Collections.Generic;

namespace B2B
{
    [Serializable]
    public class B2B : ATorg
    {
        //private string baseUrl = "https://www.b2b-center.ru/";        

        public B2B(Tag inpTag, IRequest myReq) : base(myReq)
        {
            if (inpTag.ChildTags.Count != 4)
            {
                Exception e = new Exception("Unknown format! Must be contain 4 child Tags");
                return;
            }

            foreach (Tag item in inpTag.LookForChildTag("a", false, new KeyValuePair<string, string>("class", "search-results-title visited")))
            {
                if (item.Attributes.ContainsKey("href"))
                {
                    LotNameUrl = item.Attributes["href"];
                    LotNameUrl = LotNameUrl.Substring(0, LotNameUrl.LastIndexOf("#btid="));
                    LotNameUrl = baseUrl + LotNameUrl;
                }
                    
                foreach (Tag inItem in item.ChildTags)
                {
                    if (inItem.IsProto & !inItem.IsComment)
                    {
                        LotNameStr = inItem.Value;
                        break;
                    }                        
                }
            }
            /*
            LotNameUrl = inpTag.ChildTags[0].ChildTags[0].Attributes["href"];
            LotNameStr = inpTag.ChildTags[0].ChildTags[0].ChildTags[0].Value;
            */
            TorgType = LotNameStr.Substring(0, LotNameStr.IndexOf("№") - 1);
            LotNumberStr = LotNameStr.Substring(LotNameStr.IndexOf("№") + 1).TrimEnd();

            /*
            try
            {
            */
                List<Tag> searchList = inpTag.LookForChildTag("div", false, new KeyValuePair<string, string>("class", "search-results-title-desc"));
                foreach (Tag item in searchList)
                {
                    string tmpTorgName = "";
                    string tmpDescription = "";
                    foreach (Tag inItem in item.ChildTags)
                    {
                        if (inItem.IsProto & !inItem.IsComment)
                            tmpTorgName += inItem.Value;
                        else if (inItem.Name == "span")
                        {
                            foreach (Tag itemSp in inItem.LookForChildTag(null))
                            {
                                if (itemSp.IsProto & !itemSp.IsComment)
                                    tmpTorgName += itemSp.Value;
                            }
                        }
                        else if (inItem.Name == "div")
                        {
                            foreach (Tag itemSp in inItem.LookForChildTag(null))
                            {
                                if (itemSp.IsProto & !itemSp.IsComment)
                                    tmpDescription += itemSp.Value + " ";
                            }
                        }
                    }
                    TorgName = tmpTorgName;
                    Description = tmpDescription;
                }

            OrganizerStr = inpTag.ChildTags[1].ChildTags[0].ChildTags[0].Value;
            OrganizerUrl = baseUrl + inpTag.ChildTags[1].ChildTags[0].Attributes["href"];

            DateAcceptStart = inpTag.ChildTags[2].ChildTags[0].Value;
            DateAcceptFinish = inpTag.ChildTags[3].ChildTags[0].Value;
        }

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
                LotNameUrl,
                LotNumberStr,
                TorgName,
                Description, //LotNameStr,
                OrganizerUrl,
                OrganizerStr,
                TorgType,
                DateAcceptStart,
                DateAcceptFinish);

            return result;
        }
    }
}
