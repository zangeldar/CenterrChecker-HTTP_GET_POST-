using IAuction;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace TorgiASV
{
    [Serializable]
    public class ASV : IComparable<ASV>, IObject
    {
        public string internalID { get; private set; }
        public StringUri LotName { get; private set; }
        public string LotNameStr { get { return LotName.ItemString; } }
        public string LotNameUrl { get { return LotName.ItemUri; } }
        public String LotDesc { get; private set; }
        public String LotNumberStr { get; private set; }
        public String TorgBank { get; private set; }
        public String TorgRegion { get; private set; }
        public String PriceStart { get; private set; }

        public ASV(List<Tag> itemList)
        {
            LotName = new StringUri
            {
                ItemString = itemList[2].InnerTags[0].InnerTags[0].Value.Replace("  ", "").Replace("\n",""),
                ItemUri = "https://torgiasv.ru" + itemList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "")
            };

            //
            //AsvID = .Substring();
            string urlID = itemList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "");
            int startID = urlID.Substring(0, urlID.Length - 1).LastIndexOf('/');
            urlID = urlID.Substring(startID).Replace("/", "");
            //AsvID = int.Parse(urlID);
            internalID = urlID;
            //
            LotDesc = itemList[3].Value;
            TorgBank = itemList[4].Value;
            TorgRegion = itemList[5].Value;
            PriceStart = itemList[7].InnerTags[0].Value.Replace("<span class=\"text-muted\">","");
            LotNumber = itemList[9].Value;            
        }

        public int CompareTo(ASV other)
        {
            int thisID;
            int compID;
            if (int.TryParse(this.internalID, out thisID) & int.TryParse(other.internalID, out compID))
            {
                if (thisID > compID)
                    return 1;
                else if (thisID < compID)
                    return -1;
                else
                    return 0;
            }
            return 0;
        }

        public string ToString(bool html = true)
        {
            string baseUri = "https://torgiasv.ru";
            string result = "";

            if (html)
                result += String.Format("<tr><td>" +
                    @"{0}" + "</td><td>" +
                    @"{1}" + "</td><td>" +
                    @"<a href =""{2}"">{3}</a>" + "</td><td>" +                    
                    @"{4}" + "</td><td>" +
                    @"{5}" + "</td><td>" +
                    @"{6}" + "</td></tr>",
                    internalID,
                    LotNumber,
                    baseUri + LotName.ItemUri, LotName.ItemString,
                    LotDesc,
                    TorgBank,
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
                    @"{6}" + @"\n" +
                    // строка таблицы с ссылками                    
                    @"" + ";" + 
                    @"" + ";" +
                    @"{2}" + ";" +
                    @"" + ";" +
                    @"" + ";" +
                    @"" + @"\n",
                    internalID,
                    LotNumber,
                    baseUri + LotName.ItemUri, LotName.ItemString,
                    LotDesc,
                    TorgBank,
                    TorgRegion,
                    PriceStart
                    );

            return result;
        }
    }
}
