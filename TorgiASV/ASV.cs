using HTTP_GET_POST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace TorgiASV
{
    [Serializable]
    public class ASV
    {
        public StringUri LotName { get; private set; }
        public String LotDesc { get; private set; }
        public String LotNumber { get; private set; }
        public String TorgBank { get; private set; }
        public String TorgRegion { get; private set; }
        public String PriceStart { get; private set; }

        public ASV(List<HTTP_GET_POST.Tag> itemList)
        {
            LotName = new StringUri
            {
                ItemString = itemList[2].InnerTags[0].InnerTags[0].Value.Replace("  ", "").Replace("\n",""),
                ItemUri = "https://torgiasv.ru" + itemList[2].InnerTags[0].InnerTags[0].Attributes[0].Value.Replace("\"", "")
            };

            LotDesc = itemList[3].Value;
            TorgBank = itemList[4].Value;
            TorgRegion = itemList[5].Value;
            PriceStart = itemList[7].InnerTags[0].Value.Replace("<span class=\"text-muted\">","");
            LotNumber = itemList[9].Value;            
        }

        static bool SaveMyRequestObjectXML(ASVRequest curObj, string fileName = "lastrequest.tasv.req")
        {
            bool result = false;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(CenterrRequest));

                using (Stream output = File.OpenWrite(fileName))
                {
                    formatter.Serialize(output, curObj);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                //throw;
            }

            return result;
        }

        static bool SaveMyASVObject(ASVResponse curObj, string fileName = "lastresponse.tasv")
        {
            bool result = false;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream output = File.OpenWrite(fileName))
                {
                    bf.Serialize(output, curObj);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                //throw;
            }

            return result;
        }

        static ASVRequest LoadMyRequestObjectXML(string fileName = "lastrequest.req")
        {
            ASVRequest result = null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ASVRequest));
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (ASVRequest)formatter.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        static ASVResponse LoadMyASVObject(string fileName = "lastresponse.tasv")
        {
            ASVResponse result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (ASVResponse)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

    }
}
