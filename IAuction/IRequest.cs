using System;

namespace IAuction
{
    public interface IRequest
    {
        /// <summary>
        /// Текстовый идентификатор площадки
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Текстовое представление названия площадки
        /// </summary>
        string SiteName { get; }
        /// <summary>
        /// URL площадки
        /// </summary>
        string ServiceURL { get; }
        /// <summary>
        /// Поле доступа к строке поиска по умолчанию (параметр "название лота")
        /// </summary>
        string SearchString { get; set; }
        /// <summary>
        /// Поле для получения результата запроса в виде строки
        /// </summary>
        string GetResponse { get; }                                             // Получить сырую строку ответа
        /// <summary>        
        /// Сериализуемый словарь параметров
        /// </summary>
        SerializableDictionary<string, string> MyParameters { get; set; }       // Доступ к параметрам запроса
        /// <summary>
        /// Функция сброса параметров поиска на значения по-умолчанию
        /// </summary>
        void ResetParameters();                                                 // Очистить параметры запроса  
        /// <summary>
        /// Функция сброса статуса инициализации
        /// </summary>
        void ResetInit();                                                       // Сбросить статус инициализации
        /// <summary>
        /// Функция сохранения этого объекта в файл (Сериализация XML)
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения</param>
        /// <returns>Результат операции</returns>
        bool SaveToXml(string fileName = "lastrequest.req");                    // Сохранить в XML (сериализовать)
        /// <summary>
        /// Функция загрузки объекта из файла (Десериализация XML)
        /// </summary>
        /// <param name="fileName">Имя файла объекта</param>
        /// <returns>Загруженный объект</returns>
        IRequest LoadFromXML(string fileName = "lastrequest.req");              // Загрузить из XML (десериализовать)
        /// <summary>
        /// Функция представления параметров запроса в виде строки
        /// </summary>
        /// <param name="separator">Разделитель параметров запроса.
        /// По умолчанию пусто.</param>
        /// <returns>Возвращает строковое представление параметров запроса</returns>
        string AllParametersInString(string separator = "");
        /// <summary>
        /// Функция для инициирования получения результата из запроса
        /// </summary>
        /// <returns>Возвращает новый объект Результата запроса</returns>
        IResponse MakeResponse();
        /// <summary>
        /// Функция получения последней ошибки запроса
        /// </summary>
        /// <returns>Возвращает последнее исключение</returns>
        Exception LastError();
    }   
}
