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
        /////////////////////////////
        // Часть интерфейса
        /////////////////////////////

   
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
        //часть абстрактного класса
        /////////////////////////////
        
        public SerializableDictionary<string, string> MyParameters { get; set; }        

        /// <summary>
        /// Конструктор запроса с пустыми параметрами поиска
        /// </summary>
        public ATorgRequest()
        {
            InitialiseParameters();
            SearchString = "";
        }
        /// <summary>
        /// Конструктор запроса по строке поиска
        /// </summary>
        /// <param name="searchStr"></param>
        public ATorgRequest(string searchStr)
        {
            InitialiseParameters();
            SearchString = searchStr;
        }

        /// <summary>
        /// Функция очистки параметров поиска
        /// </summary>
        protected abstract void InitialiseParameters(); // очистить параметры поиска
        /// <summary>
        /// Функция инициализации запроса, для получения уникальных идентификаторов сеанса
        /// </summary>
        /// <returns></returns>
        protected abstract bool Initialize(); // здесь регулируется переменная initialise
        /// <summary>
        /// Функция выполнения пустого запроса без параметров. 
        /// Как правило, используется в процедуре инициализации для получения уникальных идентификаторов сеанса.
        /// Функция должна устанавливать значение внутренней переменной initialise
        /// </summary>
        /// <returns></returns>
        protected abstract string getBlankResponse();   // получить пустой ответ для инициализации параметров
        /// <summary>
        /// Функция для получения тела запроса строкой из параметров поиска
        /// </summary>
        /// <returns></returns>
        protected abstract string myRawPostData();  // создается тело запроса из параметров поиска
        /// <summary>
        /// Функция получения результата запроса.
        /// </summary>
        /// <param name="postData">тело запроса строкой. По-умолчанию пусто.</param>
        /// <returns></returns>
        protected abstract string MakePost(string postData = "");   // отправить запрос
        /// <summary>
        /// Внутренняя переменная, показывающая состояние инициализации запроса.
        /// Иными словами, готов ли запрос к использованию для получения результатов
        /// </summary>
        protected bool initialised = false;
        /// <summary>
        /// Содержит последний ответ сервера в виде строки
        /// </summary>
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
        /// <summary>
        /// Статическая функция сохранения запроса в файл
        /// </summary>
        /// <param name="curObj">Объект ATorgRequest для сохранения</param>
        /// <param name="fileName">Имя фалйа для сохранения объекта</param>
        /// <returns></returns>
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

        /// <summary>
        /// Статическая функция загрузки объекта из файла
        /// </summary>
        /// <param name="curObj">Объект ATorgRequest для загрузки</param>
        /// <param name="fileName">Имя файла загрузки объекта</param>
        /// <returns></returns>
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

        ////////////////////        
        //часть реализации интерфейса
        ////////////////////
        /// <summary>
        /// Поле последней ошибки
        /// </summary>
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
