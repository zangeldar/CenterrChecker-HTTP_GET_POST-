using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    public interface IResponse
    {
        /// <summary>
        /// Поле получения нового результата старого запроса
        /// </summary>
        IResponse MakeFreshResponse { get; }
        /// <summary>
        /// URL площадки 
        /// </summary>
        string SiteName { get; }
        /// <summary>
        /// Исходный запрос
        /// </summary>
        IRequest MyRequest { get; }
        /// <summary>
        /// Список результатов запроса
        /// </summary>
        IEnumerable<IObject> ListResponse { get; }
        /// <summary>
        /// Функция проверки наличия новых результатов по сравнению с другим результатом запроса
        /// </summary>
        /// <param name="checkResponse">Старый результат запроса</param>
        /// <returns>Индикатор наличия</returns>
        bool HaveNewRecords(IResponse checkResponse);
        /// <summary>
        /// Список новых результатов
        /// </summary>
        IEnumerable<IObject> NewRecords { get; }
        /// <summary>
        /// Функция вывода новых результатов
        /// </summary>
        /// <param name="checkResponse">Старый результат для проверки</param>
        /// <param name="html">Формат вывода HTML</param>
        /// <returns>Строковое представление новых результатов</returns>
        string NewRecordsOutput(IResponse checkResponse, bool html);
        /// <summary>
        /// Функция сохранения этого объекта в файл
        /// Сериализация XML 
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Результат операции</returns>
        bool SaveToXml(string fileName = "lastrequest.req", bool overwrite = false);                    // Сохранить в XML (сериализовать)
        /// <summary>
        /// Функция загрузки объекта из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Объект результата запроса</returns>
        IResponse LoadFromXml(string fileName = "lastrequest.req");              // Загрузить из XML (десериализовать)
        /// <summary>
        /// Функция последней ошибки результата запроса
        /// </summary>
        /// <returns>Последнее исключение</returns>
        Exception LastError();
    }
}
