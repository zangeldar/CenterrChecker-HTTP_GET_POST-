using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TorgiASV;

namespace TEST
{
    class TestASV : ITest
    {
        public string GetTest(string testData, bool cached = true)
        {            
            string cacheFile = "cached.htm";
            
            ASVRequest myAsvReq = new ASVRequest();

            //myAsvReq.MyParameters["q"] = testData;
            myAsvReq.SearchString = testData;

            string answer;
            if (!File.Exists(cacheFile) || !cached)
            {
                answer = myAsvReq.GetResponse;
                //            myHTMLParser myHtmlParser = new myHTMLParser();
                //            List<Tag> myTagRes = myHtmlParser.getTags(answer, "div");
                File.WriteAllText(cacheFile, answer);
            }
            else
                answer = File.ReadAllText(cacheFile);
                                   

            return answer;
        }

        public string GetTest(string testData, string etp, bool cached = true)
        {
            throw new NotImplementedException();
        }
    }
}
