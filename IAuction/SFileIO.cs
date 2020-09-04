using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace IAuction
{
    public static class SFileIO
    {
        public static bool SaveMyResponse(IResponse curObj, string fileName = "temp.resp", bool overwrite = false)
        {
            bool result = false;

            if (!overwrite)
                fileName = GetRandomFileName(fileName);
            else
            {
                if (File.Exists(fileName))
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Can't delete the old file: " + fileName);
                        Console.WriteLine("Will be overwritten " + fileName);
                        //throw;
                    }                    
            }
                
            
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

        public static IResponse LoadMyResponse(string fileName = "temp.resp")
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

        static private string GetRandomFileName(string fileName)
        {
            string result = fileName;

            int pointExt = fileName.IndexOf('.');

            string fileN = "";
            string fileExt = "";

            if (pointExt < 0)
            {
                fileN = fileName;
                fileExt = "req";
            }
            else
            {
                fileN = fileName.Substring(0, pointExt);
                fileExt = fileName.Substring(pointExt + 1);
            }
            while (File.Exists(result))
            {
                result = fileN + "_" + (new Random().Next(0, 65536)).ToString() + "." + fileExt;
            }

            return result;
        }

        /*
        static public bool SaveMyRequestObjectXML(IRequest curObj, string fileName = "lastrequest.req") // не работает от интерфейса
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

        static public IRequest LoadMyRequestObjectXML(string fileName = "lastrequest.req") // не работает от интерфейса
        {
            IRequest result = null;
            
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(IRequest));
                //XmlSerializer formatter = new XmlSerializer(Type.GetType(checkType));
                //XmlSerializer formatter = new XmlSerializer(Type.GetType("ASVRequest"));

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
        */

        public static bool SaveMyObject(Object curObj, string fileName = "temp.obj", bool overwrite = false)
        {
            bool result = false;

            if (!overwrite)
                fileName = GetRandomFileName(fileName);
            else
            {
                if (File.Exists(fileName))
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Can't delete the old file: " + fileName);
                        Console.WriteLine("Will be overwritten " + fileName);
                        //throw;
                    }
            }


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

        public static Object LoadMyObject(string fileName = "temp.obj")
        {
            Object result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (Object)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        public static string ArrayToString(IEnumerable<string> inpArray, string separator = "")
        {
            string parSet = "";

            foreach (string item in inpArray)
                if (item.Length > 0)
                    parSet += separator + item;

            if (parSet.Length > separator.Length)
                parSet = parSet.Remove(0, separator.Length);

            //parSet = parSet.Replace(":", "").Replace("\\", "").Replace("/", "");

            return parSet;
        }
    }
}
