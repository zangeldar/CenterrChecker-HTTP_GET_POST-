using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;


namespace TEST
{
    class TestHtml : ITest
    {
        public string GetTest(string testData, string etp, bool cached = true)
        {
            throw new NotImplementedException();
        }

        string ITest.GetTest(string testData, bool cached = true)
        {            
            Tag myPar = new Tag(testData, testData);



            return "";
        }
    }
}
