using MyHTMLParser;
using System;
using System.Collections.Generic;
using System.Text;


namespace TEST
{
    class TestHtml : ITest
    {
        string ITest.GetTest(string testData, bool cached = true)
        {            
            Tag myPar = new Tag(testData);



            return "";
        }
    }
}
