using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace IAuction
{
    public static class FileIO
    {
        public static bool SaveMyObject(IResponse curObj, string fileName = "temp.resp")
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

        public static IResponse LoadMyObject(string fileName = "temp.resp")
        {
            IResponse result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (IResponse)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        static public bool SaveMyRequestObjectXML(IRequest curObj, string fileName = "lastrequest.req")
        {
            bool result = false;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(IRequest));

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

        static public IRequest LoadMyRequestObjectXML(string fileName = "lastrequest.req")
        {
            IRequest result = null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(IRequest));
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (IRequest)formatter.Deserialize(input);
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
