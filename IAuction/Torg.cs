using System;
using System.Collections.Generic;
using System.Text;

namespace IAuction
{
    abstract class TorgResponse : IResponse
    {
        public abstract IResponse MakeFreshResponse { get; }
        public abstract string SiteName { get; }
        public abstract IRequest MyRequest { get; }
        public abstract IEnumerable<IObject> ListResponse { get; }
        public abstract IEnumerable<IObject> NewRecords { get; }

        public abstract bool HaveNewRecords(IResponse checkResponse);
        //public abstract Exception LastError();
        public abstract IResponse LoadFromXml(string fileName = "lastrequest.req");
        public abstract string NewRecordsOutput(IResponse checkResponse, bool html);
        public abstract bool SaveToXml(string fileName = "lastrequest.req");


        /////////////////////////////
        ///

        private Exception lastError;
        public Exception LastError() { return lastError; }
    }
}
