using System;
using System.Collections.Generic;
using System.Text;
using HTMLParserNew;


namespace TEST
{
    class TestHtml : ITest
    {
        string ITest.GetTest(string testData)
        {            
            Tag myPar = new Tag(testData);



            return "";
        }
    }
}
