using System;

namespace IAuction
{
    public interface IRequest
    {
        string Type { get; }
        string SiteName { get; }
        string ServiceURL { get; }
        string SearchString { get; set; }
        string GetResponse { get; }                                             // Получить сырую строку ответа
        SerializableDictionary<string, string> MyParameters { get; set; }       // Доступ к параметрам запроса
        void ResetParameters();                                                 // Очистить параметры запроса        
        bool SaveToXml(string fileName = "lastrequest.req");                    // Сохранить в XML (сериализовать)
        IRequest LoadFromXML(string fileName = "lastrequest.req");              // Загрузить из XML (десериализовать)
        string AllParametersInString(string separator = "");
        IResponse MakeResponse();
        Exception LastError();
    }   
}
