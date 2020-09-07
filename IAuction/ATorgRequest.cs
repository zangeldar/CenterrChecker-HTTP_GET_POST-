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


        /// <summary>
        /// Тип запроса (идентификатор, маркер)
        /// Используется для распознавания типа запроса из XML
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Пользовательское название сайта
        /// </summary>
        public abstract string SiteName { get; }

        /// <summary>
        /// Базовый URL сайта
        /// </summary>
        public abstract string ServiceURL { get; }

        /// <summary>
        /// Строка для поиска
        /// </summary>
        public abstract string SearchString { get; set; }

        //public abstract string GetResponse { get; }        
        //public abstract string GetRequestStringPrintable();        
        //public abstract IRequest LoadFromXML(string fileName = "lastrequest.req");

        /// <summary>
        /// Создать результат запроса
        /// </summary>
        /// <returns></returns>
        public abstract IResponse MakeResponse();
        //public abstract ATorgResponse MakeResponse();
        //public abstract void ResetParameters();
        //public abstract bool SaveToXml(string fileName = "lastrequest.req");

        /////////////////////////////
        //часть абстрактного класса
        /////////////////////////////

        /// <summary>
        /// Спец.словарь параметров запроса
        /// </summary>
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
        /// Здесь регулируется переменная initialise
        /// </summary>
        /// <returns>Возвращает переменную успеха инициализации</returns>
        protected abstract bool Initialize(); // 
        /// <summary>
        /// Функция выполнения пустого запроса без параметров. 
        /// Как правило, используется в процедуре инициализации для получения уникальных идентификаторов сеанса, если этого требуется. 
        /// Функция должна устанавливать значение внутренней переменной initialise.
        /// </summary>
        /// <returns>Возвращает результат запросав виде строки</returns>
        protected abstract string getBlankResponse();   // получить пустой ответ для инициализации параметров
        /// <summary>
        /// Функция для получения тела запроса из параметров поиска
        /// </summary>
        /// <returns>Возвращает тело запроса в виде строки</returns>
        protected virtual string myRawPostData() 
        {
            string result = "";
            bool first = true;
            foreach (KeyValuePair<string, string> item in MyParameters)
            {
                if (item.Value != "")
                {
                    if (first)
                    {
                        result += "?";
                        first = false;
                    }
                    else
                        result += "&";
                    result += item.Key + "=" + item.Value;
                }
            }

            return result;
        }
        /// <summary>
        /// Создает и выполняет запрос из параметров поиска.
        /// </summary>
        /// <param name="postData">тело запроса строкой. По-умолчанию пусто.</param>
        /// <returns>Возвращает строку результата запроса</returns>
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

        /// <summary>
        /// Требуется для работы с HTTPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Получить результат запроса, инициализировава, при необходимости
        /// </summary>
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
        
        /// <summary>
        /// Вспомогательная функция для получения всех параметров поиска в одну строку
        /// Используется при создании имени файла
        /// </summary>
        /// <param name="separator">(НЕОБЯЗАТЕЛЬНО) Строка-разделитель</param>
        /// <returns>Строка параметров поиска</returns>
        public virtual string AllParametersInString(string separator = "")
        {
            string parSet = "";

            foreach (string item in this.MyParameters.Values)
                if (item.Length > 0)
                    parSet += separator + item;

            if (parSet.Length > separator.Length)
                parSet = parSet.Remove(0, separator.Length);

            parSet = parSet.Replace(":", "").Replace("\\", "").Replace("/", "");

            return parSet;
        }
        /// <summary>
        /// Статическая функция сохранения запроса в файл
        /// </summary>
        /// <param name="curObj">Объект ATorgRequest для сохранения</param>
        /// <param name="fileName">Имя фалйа для сохранения объекта</param>
        /// <returns>Возвращает результат сохранения</returns>
        static public bool SaveMyRequestObjectXML(ATorgRequest curObj, string fileName = "lastrequest.req")
        {
            bool result = false;

            fileName = GetRandomFileName(fileName);

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
        /// <returns>Возвращает результат загрузки</returns>
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
        
        /// <summary>
        /// Сбрасывает параметры поиска на параметры по-умолчанию
        /// </summary>
        public void ResetParameters()
        {
            InitialiseParameters();
        }

        /// <summary>
        /// Сбрасывает значение переменной инициализации
        /// </summary>
        public void ResetInit()
        {
            initialised = false;
        }

        /// <summary>
        /// Загружает запрос из файла XML (Десериализация)
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Загруженный запрос</returns>
        public IRequest LoadFromXML(string fileName = "lastrequest.req")
        {
            return LoadMyRequestObjectXML(this, fileName);
        }

        /// <summary>
        /// Сохраняет запрос в файл XML (Сериализация)
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Результат загрузки</returns>
        public bool SaveToXml(string fileName = "lastrequest.req")
        {
            return SaveMyRequestObjectXML(this, fileName);
        }

        /// <summary>
        /// Генерирует произвольное имя файла, чтобы исключить перезапись
        /// </summary>
        /// <param name="fileName">Исходное имя файла</param>
        /// <returns>Модифицированная строка имени файла</returns>
        static protected string GetRandomFileName(string fileName)
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
    }
}
