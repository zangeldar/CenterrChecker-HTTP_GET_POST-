using HTTP_GET_POST;
using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace TorgiASV
{
    [Serializable]
    public class ASV : IComparable<ASV>
    {
        public int AsvID { get; private set; }
        public StringUri LotName { get; private set; }
        public String LotDesc { get; private set; }
        public String LotNumber { get; private set; }
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
            AsvID = int.Parse(urlID);
            //
            LotDesc = itemList[3].Value;
            TorgBank = itemList[4].Value;
            TorgRegion = itemList[5].Value;
            PriceStart = itemList[7].InnerTags[0].Value.Replace("<span class=\"text-muted\">","");
            LotNumber = itemList[9].Value;            
        }

        public int CompareTo(ASV other)
        {
            //throw new NotImplementedException();
            if (this.AsvID > other.AsvID)
                return 1;
            else if (this.AsvID < other.AsvID)
                return -1;
            else
                return 0;
        }
    }
}
