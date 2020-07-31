using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;

namespace TorgiASV
{
    [Serializable]
    public class TorgASV : ATorg
    {
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
                lowerParent = lowerParent.LookForChildTag("h3", true, new KeyValuePair<string, string>("class", "lot-catalog__group-title bold"))[0];
                TorgTypeStr = lowerParent.ChildTags[0].ChildTags[0].Value;
                TorgTypeUrl = lowerParent.ChildTags[0].Attributes["href"];
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
                this.LotDesc == curObj.LotDesc &
                this.LotNumberStr == curObj.LotNumberStr &
                this.Organisator == curObj.Organisator &
                this.TorgRegion == curObj.TorgRegion &
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
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PriceStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LotNumberStr);

            internalID = hashCode.ToString();

            return hashCode;
        }

        public override string ToString(bool html)
        {
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
                    @"{0}" + "</td><td>" +
                    @"{1}" + "</td><td>" +
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +
                    @"{4}" + "</td><td>" +
                    @"{5}" + "</td><td>" +
                    @"{6}" + "</td><td>" +
                    @"{7}" + "</td></tr>",
                    internalID,
                    LotNumberStr,
                    //LotName.ItemUri, LotName.ItemString,
                    LotNameUrl, LotNameStr,
                    LotDesc,
                    Organisator,
                    TorgRegion,
                    PriceStart
                    );
            else
                result += String.Format(
                    @"{0}" + ";" +
                    @"{1}" + ";" +
                    @"{3}" + ";" +
                    @"{4}" + ";" +
                    @"{5}" + ";" +
                    @"{6}" + ";" +
                    @"{7}" + Environment.NewLine +
                    // строка таблицы с ссылками                    
                    @"" + ";" +
                    @"" + ";" +
                    @"{2}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + Environment.NewLine,
                    internalID,
                    LotNumberStr,
                    //LotName.ItemUri, LotName.ItemString,
                    LotNameStr, LotNameUrl,
                    LotDesc,
                    Organisator,
                    TorgRegion,
                    PriceStart
                    );

            return result;
        }
    }
}
