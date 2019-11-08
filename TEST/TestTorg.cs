using CenterrRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TorgiASV;

namespace TEST
{
    class TestTorg : ITest
    {
        public string GetTest(string testData, string etp, bool cached = true)
        {     
            IResponse myResp;

            switch (etp)
            {
                case "cntr":
                    myResp = new CenterrResponse(testData);
                    break;
                case "tasv":
                    myResp = new ASVResponse(testData);
                    break;
                default:
                    return "Unknown ETP";
                    break;
            }

            return myResp.NewRecordsOutput(null, false);            
        }

        public string GetTest(string testData, bool cached = true)
        {
            throw new NotImplementedException();
        }
    }
}
