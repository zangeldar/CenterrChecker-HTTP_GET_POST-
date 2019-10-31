using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace IAuction
{
    [Serializable]
    public abstract class ATorgRequest : IRequest
    {
        /// <summary>
        /// Часть интерфейса
        /// </summary>
        ///         
        public abstract string Type { get; }
        public abstract string SiteName { get; }
        public abstract string ServiceURL { get; }
        public abstract string SearchString { get; set; }

        //public abstract string GetResponse { get; }        
        //public abstract string GetRequestStringPrintable();        
        //public abstract IRequest LoadFromXML(string fileName = "lastrequest.req");
        public abstract IResponse MakeResponse();
        //public abstract void ResetParameters();
        //public abstract bool SaveToXml(string fileName = "lastrequest.req");

        /////////////////////////////
        ///часть абстрактного класса
        ///
        public SerializableDictionary<string, string> MyParameters { get; set; }        

        public ATorgRequest()
        {
            InitialiseParameters();
            SearchString = "";
        }

        public ATorgRequest(string searchStr)
        {
            InitialiseParameters();
            SearchString = searchStr;
        }

        protected abstract void InitialiseParameters(); // очистить параметры поиска
        protected abstract bool Initialize(); // здесь регулируется переменная initialise
        protected abstract string getBlankResponse();   // получить пустой ответ для инициализации параметров
        protected abstract string myRawPostData();  // создается тело запроса из параметров поиска
        protected abstract string MakePost(string postData = "");   // отправить запрос

        protected bool initialised = false;
        protected string lastAnswer;
        public string GetResponse
        {
            get
            {
                if (!initialised)       // initialized already?
                    if (!Initialize())  // if not then Initialize now!
                        return null;    // if not success then break
                return MakePost(myRawPostData());
            }
        }

        public virtual string AllParametersInString(string separator = "")
        {
            string parSet = "";

            foreach (string item in this.MyParameters.Values)
                if (item.Length > 0)
                    parSet += separator + item;

            if (parSet.Length > separator.Length)
                parSet = parSet.Remove(0, separator.Length);

            return parSet;
        }

        static public bool SaveMyRequestObjectXML(ATorgRequest curObj, string fileName = "lastrequest.req")
        {
            bool result = false;
            
            try
            {
                XmlSerializer formatter = new XmlSerializer(curObj.GetType());

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

        static public IRequest LoadMyRequestObjectXML(ATorgRequest curObj, string fileName = "lastrequest.req")
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(curObj.GetType());

                using (Stream input = File.OpenRead(fileName))
                {
                    curObj = (ATorgRequest)formatter.Deserialize(input);
                }
                
            }
            catch (Exception e)
            {
                return null;
                //throw;
            }
            return curObj;
        }

        /////////////
        ///часть реализации интерфейса
        ///
        protected Exception lastError;
        public Exception LastError() { return lastError; }
        
        public void ResetParameters()
        {
            InitialiseParameters();
        }

        public void ResetInit()
        {
            initialised = false;
        }

        public IRequest LoadFromXML(string fileName = "lastrequest.req")
        {
            return LoadMyRequestObjectXML(this, fileName);
        }

        public bool SaveToXml(string fileName = "lastrequest.req")
        {
            return SaveMyRequestObjectXML(this, fileName);
        }
    }
}
