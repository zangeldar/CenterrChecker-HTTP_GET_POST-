using System;

namespace IAuction
{
    public interface IRequest
    {
        string GetResponse { get; }                                             // Получить сырую строку ответа
        SerializableDictionary<string, string> MyParameters { get; set; }       // Доступ к параметрам запроса
        void ResetParameters();                                                 // Очистить параметры запроса
        string CreateFileName(bool request = false);                            // Сгенерировать имя фалйа из параметров запроса, как для запроса, так и для результата
        bool SaveToXml(string fileName = "lastrequest.req");                    // Сохранить в XML (сериализовать)
        IRequest LoadFromXML(string fileName = "lastrequest.req");              // Загрузить из XML (десериализовать)

        string GetRequestStringPrintable();
    }   
}
