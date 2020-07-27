using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace ETP_GPB
{
    public class GPB : ATorg
    {
        public override string internalID { get; protected set; }
        public override string LotNameStr { get; protected set; }
        public override string LotNameUrl { get; protected set; }
        public override string PriceStart { get; protected set; }
        public override string LotNumberStr { get; protected set; }


        public string TorgType { get; private set; }
        public string OrganizerType { get; private set; }
        public string OrganizerStr { get; private set; }
        public string Description { get; private set; }
        public List<string> Props { get; private set; }
        public string Status { get; private set; }
        public string DateEndAccept { get; private set; }
        public string Region { get; private set; }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString(bool html)
        {
            throw new NotImplementedException();
        }

        public GPB (Tag inpTag)
        {
            FillByTag(inpTag);
        }

        private void FillByTag(Tag inpTag)
        {
            List<string> Props = new List<string>();
            List<Tag> workList = new List<Tag>();
            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__companyName"));
            OrganizerStr = workList[0].ChildTags[0].Value;

            workList = inpTag.LookForTag("a", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__link procedure__infoTitle"));
            LotNameUrl = workList[0].Attributes["href"];
            LotNameStr = workList[0].ChildTags[0].Value;

            //workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoDescriptionShort"));
            //Description = ((Tag)workList[0].ChildTags[0]).ChildTags[0].Value;

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoDescriptionShort"));            
            internalID = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoAuction"));
            OrganizerType = ((Tag)workList[0].ChildTags[0]).ChildTags[0].Value;
            TorgType = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__infoPropsValue"));
            foreach (Tag item in workList)
                Props.Add(item.ChildTags[0].Value);

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__detailsReception"));
            DateEndAccept = ((Tag)workList[0].ChildTags[1]).ChildTags[0].Value;

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__detailsSum"));
            PriceStart = workList[0].ChildTags[0].Value;

            workList = inpTag.LookForTag("div", true, new System.Collections.Generic.KeyValuePair<string, string>("class", "procedure__detailsegion"));
            if (workList.Count > 0)
                Region = ((Tag)workList[0].ChildTags[0]).ChildTags[0].Value;
        }
    }
}
