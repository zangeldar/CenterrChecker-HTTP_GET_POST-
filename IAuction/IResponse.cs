using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    public interface IResponse
    {
        string SiteName { get; }
        IRequest MyRequest { get; }
        IEnumerable<IObject> ListResponse { get; }        
        bool HaveNewRecords(IResponse checkResponse);
        IEnumerable<IObject> NewRecords { get; }
        string NewRecordsOutput(IResponse checkResponse, bool html);
        bool SaveToXml(string fileName = "lastrequest.req");                    // Сохранить в XML (сериализовать)
        IResponse LoadFromXml(string fileName = "lastrequest.req");              // Загрузить из XML (десериализовать)
        Exception LastError { get; }
    }
}
