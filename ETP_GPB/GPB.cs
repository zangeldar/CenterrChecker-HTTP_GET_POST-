using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace ETP_GPB
{
    public class GPB : ATorg
    {
        private string baseUrl = "https://etpgpb.ru/procedure/";
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }


        public string TorgType { get; private set; }
        public string Section { get; private set; }
        public string OrganizerStr { get; private set; }
        public string TorgName { get; private set; }
        public List<string> Props { get; private set; }
        public string Status { get; private set; }
        public string DateAcceptFinish { get; private set; }
        public string Region { get; private set; }
        
        public GPB (Tag inpTag)
        {
            FillByTag(inpTag);
        }

        private void FillByTag(Tag inpTag)
        {
            Props = new List<string>();
            List<Tag> workList = new List<Tag>();
            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__companyName"));
            if (workList.Count > 0)
                if (workList[0].ChildTags.Count > 0)
                    OrganizerStr = workList[0].ChildTags[0].Value;

            workList = inpTag.LookForTag("a", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__link procedure__infoTitle"));
            if (workList.Count > 0)
            {
                if(workList[0].Attributes.ContainsKey("href"))
                    LotNameUrl = workList[0].Attributes["href"];
                if (workList[0].ChildTags.Count > 0)
                    LotNameStr = workList[0].ChildTags[0].Value;
            }            

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoDescriptionShort"));
            if (workList.Count > 0)
                if (workList[0].ChildTags.Count > 0)
                {
                    if (((Tag)workList[0].ChildTags[0]).ChildTags.Count > 0)
                        TorgName = ((Tag)workList[0].ChildTags[0]).ChildTags[0].Value;
                    if (workList[0].ChildTags.Count > 1)
                        if (((Tag)workList[0].ChildTags[1]).ChildTags.Count > 0)
                            LotNumberStr = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;
                }

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoAuction"));
            if (workList.Count > 0)
                if (workList[0].ChildTags.Count > 0)
                {
                    if (((Tag)workList[0].ChildTags[0]).ChildTags.Count > 0)
                        Section = ((Tag)workList[0].ChildTags[0]).ChildTags[0].Value;
                    if (workList[0].ChildTags.Count > 1)
                        if (((Tag)workList[0].ChildTags[1]).ChildTags.Count > 0)
                            TorgType = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;
                }

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoPropsValue"));
            foreach (Tag item in workList)
            {
                if (item.ChildTags.Count > 0)
                    if (item.ChildTags[0].IsProto)
                        Props.Add(item.ChildTags[0].Value);
            }
                

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__detailsReception"));
            if (workList.Count > 0)
                if (workList[0].ChildTags.Count > 0)
                {
                    if (((Tag)workList[0].ChildTags[0]).ChildTags.Count > 0)
                        if (((Tag)workList[0].ChildTags[0]).ChildTags[0].IsProto)
                            Status = ((Tag)workList[0].ChildTags[0]).ChildTags[0].Value;
                    if (workList[0].ChildTags.Count > 1)
                        if (((Tag)workList[0].ChildTags[1]).ChildTags.Count > 0)
                            if (((Tag)workList[0].ChildTags[1]).ChildTags[0].IsProto)
                                DateAcceptFinish = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;
                }

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__detailsSum"));
            if (workList.Count > 0)
                if (workList[0].ChildTags.Count > 0)
                    if (workList[0].ChildTags[0].IsProto)
                        PriceStart = workList[0].ChildTags[0].Value;

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__detailsRegion"));
            if (workList.Count > 0)
                if (workList[0].ChildTags.Count > 1)
                    if (((Tag)workList[0].ChildTags[1]).ChildTags.Count > 0)
                        if (((Tag)workList[0].ChildTags[1]).ChildTags[0].IsProto)
                            Region = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GPB))
                return false;
            GPB curObj = (GPB)obj;

            if (this.internalID == curObj.internalID &
                this.LotNameStr == curObj.LotNameStr &
                this.LotNameUrl == curObj.LotNameUrl &
                this.LotNumberStr == curObj.LotNumberStr &
                this.PriceStart == curObj.PriceStart &
                this.Status == curObj.Status &
                this.Region == curObj.Region &
                this.DateAcceptFinish == curObj.DateAcceptFinish &
                this.OrganizerStr == curObj.OrganizerStr &                
                this.Section == curObj.Section &
                this.TorgName == curObj.TorgName &
                this.TorgType == curObj.TorgType &
                this.Props == curObj.Props)
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
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Region);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateAcceptFinish);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizerStr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Section);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TorgType);            
            //hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Props);

            internalID = hashCode.ToString();

            return base.GetHashCode();
        }

  
        public override string ToString(bool html)
        {
            //throw new NotImplementedException();
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
                    @"<a href =""{0}"">{1}</a>" + "</td><td>" +
                    @"<a href =""{0}"">{2}</a>" + "</td><td>" +
                    @"{3}" + "</td><td>" +
                    @"{4}" + "</td><td>" +
                    @"{5}" + "</td><td>" +
                    @"{6}" + "</td><td>" +
                    @"{7}" + "</td><td>" +
                    @"{8}" + "</td><td>" +
                    @"{9}" + "</td><td>" +
                    @"{10}" + "</td><td>" +
                    @"{11}" + "</td></tr>",
                    baseUrl + LotNameUrl,
                    LotNumberStr,
                    LotNameStr,
                    TorgName,
                    PriceStart,
                    OrganizerStr,
                    DateAcceptFinish,
                    Status,
                    Region,
                    TorgType,
                    Section,
                    GetListAsString(Props, html)
                    );
            else
                result += String.Format(
                    @"{1}" + ";" +
                    @"{2}" + ";" +
                    @"{3}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + ";" +
                    @"{8}" + ";" +
                    @"{9}" + ";" +
                    @"{10}" + ";" +                    
                    @"{11}" + Environment.NewLine +
                    @"{0}" + ";" +
                    @"{0}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" + Environment.NewLine,
                    baseUrl + LotNameUrl,
                    LotNumberStr,
                    LotNameStr,
                    TorgName,
                    PriceStart,
                    OrganizerStr,
                    DateAcceptFinish,
                    Status,
                    Region,
                    TorgType,
                    Section,
                    GetListAsString(Props, html)
                    );
            return result;

        }

        private string GetListAsString(List<string> inpList, bool html)
        {
            string result = "";
            string newLine = Environment.NewLine;
            if (html)
                newLine = "<br>";

            foreach (string item in inpList)
                result += item + newLine;

            result = result.Remove(result.LastIndexOf(newLine));

            return result;
        }

    }
}
