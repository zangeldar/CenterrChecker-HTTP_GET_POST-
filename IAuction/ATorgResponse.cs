//using MyHTMLParser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAuction
{
    [Serializable]
    abstract public class ATorgResponse : IResponse
    {/*
        /// <summary>
        ////Часть интерфейса
        /// </summary>
        //public abstract IResponse MakeFreshResponse { get; }
        ///

            */

        /// <summary>
        /// Имя сайта, выводимое пользователю
        /// User-friendly SiteName
        /// </summary>
        public virtual string SiteName { get; protected set; }
        //public abstract IRequest MyRequest { get; }
        //public abstract IEnumerable<IObject> ListResponse { get; private set; }
        //public abstract IEnumerable<IObject> NewRecords { get; }

        //public abstract bool HaveNewRecords(IResponse checkResponse);
        //public abstract Exception LastError();
        /// <summary>
        /// Функция загрузки объекта результата запроса из файла (Deserialize)
        /// </summary>
        /// <param name="fileName">Имя файла для загрузки</param>
        /// <returns></returns>
        public virtual IResponse LoadFromXml(string fileName = "lastrequest.req")
        {
            return SFileIO.LoadMyResponse(fileName);
        }
        //public abstract string NewRecordsOutput(IResponse checkResponse, bool html);

        /// <summary>
        /// Функция выгрузки объекта результата запроса в файл (Serialize)
        /// </summary>
        /// <param name="fileName">Имя файладля сохранения</param>
        /// <param name="overwrite">Перезаписывать при необходимости</param>
        /// <returns></returns>
        public virtual bool SaveToXml(string fileName = "lastrequest.req", bool overwrite=false)
        {
            return SFileIO.SaveMyResponse(this, fileName, overwrite);
        }

        /*
        //////////////////////
        ///часть абстрактного класса
        ///
        */
        /// <summary>
        /// Массив строк заголовка таблицы вывода результатов. Используется в функции CreateTableForMailing(bool)
        /// </summary>
        public string[] tableHead { get; protected set; }
        /// <summary>
        /// Возвращающает данные для отправки на электронную почту пользователю
        /// </summary>
        /// <param name="html">Задает формат вывода HTML(истина), или CSV(ложь\не указано)</param>
        /// <returns>Результат запроса в виде таблицы</returns>
        protected virtual string CreateTableForMailing(bool html = true)
        {
            string result;
            string rowStart;
            string rowEnd;
            string rowSeparatorSt;
            string rowSeparatorEn;
            if (html)
            {
                rowStart = @"<tr>";
                rowEnd = @"</tr>";
                rowSeparatorSt = @"<th>";
                rowSeparatorEn = @"</th>";
                result = @"<table border=""1"">";
            }
            else
            {
                rowStart = @"";
                rowEnd = "\n";
                rowSeparatorSt = @"";
                rowSeparatorEn = @";";
                result = "";
            }

            result += rowStart;
            foreach (string item in tableHead)
                result += rowSeparatorSt + item + rowSeparatorEn;

            result += rowSeparatorEn;

            foreach (ATorg item in NewRecords)
                result += item.ToString(html);

            if (html)
                result += @"</table>";

            return result;
        }
        /// <summary>
        /// Подготавливает тело письма для отправки на электронную почту
        /// </summary>
        /// <param name="inpTableStr">Таблица результатов запроса</param>
        /// <param name="needCheckOnSite">Признак максимально возможного количества результатов. Нужен для добавления фразы "Возможно, есть и дургие результаты, проверьте на сайте"</param>
        /// <param name="html">Формат вывода HTML(истина/не указано), или просто текст(ложь)</param>
        /// <returns>Строка тела письма для отправки по электронной почте</returns>
        protected string PrepareMailBody(string inpTableStr, bool needCheckOnSite=false, bool html = true)
        {
            string messageBody = "";
            string parSet = "";
            string newLine = Environment.NewLine;
                        
            parSet = this.MyRequest.AllParametersInString(", ");

            if (html)
            {
                newLine = "<br>";
                messageBody += String.Format(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.=w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">");
                messageBody += String.Format(@"<html xmlns=""http://www.w3.org/1999/xhtml"">");
                messageBody += String.Format(@"<head>");
                messageBody += String.Format(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-=1"" />");
                messageBody += String.Format(@"</head>");
                messageBody += String.Format(@"<body marginwidth=""0"" marginheight=""0"" leftmargin=""0"" topmargin=""0"" style=""width: 100 % !important"">");
            }
            messageBody += String.Format("По Вашему запросу: \"{0}\" были обнаружены новые записи:{2}{1}", parSet, inpTableStr, newLine);

            if (needCheckOnSite)
                messageBody += newLine + newLine + " Возможно, есть и другие новые записи! Обязательно проверьте на сайте!!";
            if (html)
            {
                messageBody += String.Format(@"</body>");
                messageBody += "</html>";
            }
            return messageBody;
        }
        /// <summary>
        /// Конструктор для получения Результата по новому Запросу из строки
        /// </summary>
        /// <param name="searchStr">Строка запроса</param>
        public ATorgResponse(string searchStr)
        {
            // need to make an response
            //this.MyRequest = IRequest(searchStr);
            //this.MyRequest = ATorgRequest(searchStr);
            //FillListResponse();
        }
        /// <summary>
        /// Конструктор для получения нового Результата по имеющемуся Запросу
        /// </summary>
        /// <param name="myReq">Имеющийся Запрос</param>
        public ATorgResponse(IRequest myReq)
        {
            if (!(myReq is IRequest))
                return;
            this.MyRequest = myReq;
            this.SiteName = myReq.SiteName;
            this.MyRequest.ResetInit();
            FillListResponse();
        }
        /// <summary>
        /// Конструктор для создания готового Результата из Запроса и списка Записей Результата
        /// </summary>
        /// <param name="myReq"></param>
        /// <param name="listResp"></param>
        public ATorgResponse(ATorgRequest myReq, List<IObject> listResp)
        {
            this.MyRequest = myReq;
            this.SiteName = myReq.SiteName;
            this.ListResponse = listResp;
            freshResponse = false;
        }
        /// <summary>
        /// Признак нового ответа
        /// </summary>
        protected bool freshResponse = false;
        protected string lastAnswer = "";
        /// <summary>
        /// Заполняет объекты торга по результатам запроса
        /// </summary>
        protected virtual bool FillListResponse()
        {
            lastAnswer = MyRequest.GetResponse;
            if (lastAnswer == null)
            {
                if (lastError == null)
                    lastError = MyRequest.LastError();
                return false;
            }                
            return true;
        }
        
        /// <summary>
        /// (НЕ ИСПОЛЬЗУЕТСЯ) Выравнивает список списков по максимальным значениям, чтобы получить таблицу
        /// </summary>
        /// <param name="inpList">Входящий список списков</param>
        /// <returns></returns>
        static protected List<List<string>> GetResultTableAsList(List<List<string>> inpList)
        {
            List<List<string>> resList = new List<List<string>>();

            // 1. Calculate MAX columns count
            int colCount = 0;
            foreach (List<string> itemList in inpList)
                colCount = Math.Max(colCount, itemList.Count);

            // 2. Fill result rows
            foreach (List<string> itemListRows in inpList)
            {
                if (itemListRows.Count != colCount)     // Skip all rows that have not another count of columns instead MAX columns count
                    continue;
                resList.Add(itemListRows);
            }

            return resList;
        }
        
        /*
        static protected List<List<StringUri>> GetResultTableAsList(List<List<StringUri>> inpList)
        {
            List<List<StringUri>> resList = new List<List<StringUri>>();

            // 1. Calculate MAX columns count
            int colCount = 0;
            foreach (List<StringUri> itemList in inpList)
                colCount = Math.Max(colCount, itemList.Count);

            // 2. Fill result rows
            foreach (List<StringUri> itemListRows in inpList)
            {
                if (itemListRows.Count != colCount)     // Skip all rows that have not another count of columns instead MAX columns count
                    continue;
                resList.Add(itemListRows);
            }

            return resList;
        }
        */
        /////////////////////////////
        ///часть реализации интерфейса
        ///

        protected Exception lastError;
        /// <summary>
        /// Содержит последнюю ошибку
        /// </summary>
        /// <returns></returns>
        public Exception LastError() { return lastError; }
        public IEnumerable<IObject> ListResponse { get; protected set; }
        /// <summary>
        /// Текущий запрос
        /// </summary>
        public IRequest MyRequest { get; protected set; }
        /// <summary>
        /// Список новых результатов
        /// </summary>
        public IEnumerable<IObject> NewRecords { get; private set; }
        /// <summary>
        /// Получает новые результаты запроса
        /// </summary>
        public abstract IResponse MakeFreshResponse { get; }
        /// <summary>
        /// Указывает максимально возможное количество результатов за один запрос (на 1 страницу)
        /// </summary>
        public abstract int MaxItemsOnPage { get; }
        //public IResponse MakeFreshResponse { get; protected set; }
        private bool haveNewRecords;
        /// <summary>
        /// Проверка на наличие новых результатов
        /// </summary>
        /// <param name="checkResponse">Старый запрос для сравнения</param>
        /// <returns>Истина - есть новые результаты, Ложь - если новых результатов нет</returns>
        public bool HaveNewRecords(IResponse checkResponse)
        {
            haveNewRecords = false;
            if (!haveNewRecords)
            {
                NewRecords = DoOneCheck((IResponse)checkResponse, true);
                if (NewRecords != null)
                    if (NewRecords.Count() > 0)
                        haveNewRecords = true;
            }
            return haveNewRecords;
        }
        /// <summary>
        /// Представить новые результаты запроса для отправки по почте
        /// </summary>
        /// <param name="checkResponse">(Необязательно)Предыдущий запрос для поиска новых результатов</param>
        /// <param name="html">Формат вывода - HTML или CSV</param>
        /// <returns></returns>
        public string NewRecordsOutput(IResponse checkResponse = null, bool html = true)
        {
            if (!haveNewRecords)
            {
                if (!HaveNewRecords(checkResponse))
                {
                    if (MyRequest.LastError() != null)
                        return "ERROR: " + MyRequest.LastError().Message;
                    else
                        return "";
                }
            }

            string itemsTable = CreateTableForMailing(html);
            return PrepareMailBody(itemsTable, (NewRecords.Count()>=MaxItemsOnPage), html);
        }
        /// <summary>
        /// Выполняет сравнение новых результатов со старыми
        /// </summary>
        /// <param name="checkData">(НЕОБЯЗАТЕЛЬНО)Старые результаты запроса</param>
        /// <param name="detail">ЛОЖЬ(по-умолчанию) - поиск первого старого результат среди новых. ИСТИНА - Сравнение каждого нового результата с каждым старым. </param>
        /// <returns>Возвращает список новых результатов</returns>
        private IEnumerable<IObject> DoOneCheck(IResponse checkData = null, bool detail = false)
        {
            IEnumerable<IObject> curListResponse = this.ListResponse;
            if (checkData != null)
                if (Enumerable.SequenceEqual(this.MyRequest.MyParameters, checkData.MyRequest.MyParameters)) // если запросы одинаковые, то
                    if (detail)                                                                     // если нужна детальная проверка, тогда
                        //return GetListOfNewRecords((List<IObject>)checkData.ListResponse); // получаем все строки, исключая строки из последнего сохраненного результата
                        return GetListOfNewRecords((IEnumerable<IObject>)checkData.ListResponse); // получаем все строки, исключая строки из последнего сохраненного результата
                    else
                        return GetListOfNewRecords((IObject)checkData.ListResponse.ToList()[0]); // получаем все строки, пока не наткнемся на первую из последнего сохраненного результата

            return curListResponse;
        }
        /// <summary>
        /// Выполняет поиск переданного объекта среди новых результатов запроса
        /// </summary>
        /// <param name="checkRowItem">Передаваемый объект для поиска</param>
        /// <returns>Список новых результатов</returns>
        private List<IObject> GetListOfNewRecords(IObject checkRowItem) // проверка только по одной записи
        {
            IEnumerable<IObject> inpList = this.ListResponse;
            List<IObject> result = new List<IObject>();

            if (inpList == null)
            {
                lastError = MyRequest.LastError();
                return result;
            }

            for (int i = 0; i < inpList.Count(); i++)
            {
                //if (inpList[i].ToString() == checkRowItem.ToString())
                if (inpList.ElementAt(i).Equals(checkRowItem))
                    break;
                result.Add(inpList.ElementAt(i));
            }

            return result;
        }
        /// <summary>
        /// Выполняет проверку каждого результата с каждым объектом из переданного списка
        /// </summary>
        /// <param name="checkRows">Передаваемый список для сравнения</param>
        /// <returns>Список новых результатов, не найденных в списке старых результатов</returns>
        private List<IObject> GetListOfNewRecords(IEnumerable<IObject> checkRows)  // проверка по всем записям - ДОЛЬШЕ
        {
            IEnumerable<IObject> inpList = this.ListResponse;
            List<IObject> result = new List<IObject>();
            if (inpList == null)
            {
                lastError = MyRequest.LastError();
                return result;
            }
            if (checkRows == null)  // Если старый список пуст (такое возможно, если был таймаут при получении первых результатов), 
                return inpList.ToList();  // то возвращать список нового запроса.
            bool needBreak = false;
            for (int i = 0; i < inpList.Count(); i++)
            {
                for (int j = 0; j < checkRows.Count(); j++)
                {
                    //if (inpList[i].ToString() == checkRows[j].ToString())
                    if (inpList.ElementAt(i).Equals(checkRows.ElementAt(j)))
                    {
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak)
                    continue;
                result.Add(inpList.ElementAt(i));
            }
            return result;
        }        
    }
}
