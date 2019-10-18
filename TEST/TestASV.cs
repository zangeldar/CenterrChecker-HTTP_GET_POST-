using System;
using System.Collections.Generic;
using System.Text;
using TorgiASV;

namespace TEST
{
    class TestASV : ITest
    {
        public string GetTest(string testData)
        {
            ASVRequest myAsvReq = new ASVRequest(testData);
            string answer = myAsvReq.GetResponse;
            return answer;
        }     
        
    }
}
