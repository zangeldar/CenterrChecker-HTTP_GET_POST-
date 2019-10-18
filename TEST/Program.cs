using System;
using TorgiASV;
using HTTP_GET_POST;
using System.Collections.Generic;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //HTTP_GET_POST.Program.SendMailRemind(HTTP_GET_POST.Program.PrepareMailBody(null, "", 20));
            string testData;
            ITest curTest;
            string result = "";

            testData = @"/search/?q=пирит";
            curTest = new TestASV();

            testData = " <html>";
            curTest = new TestHtml();

            result = curTest.GetTest(testData);

            testData = "<!Document><html><head><title></title></head><body><div attribute>MiDiv</div>MyBody<table/><a /><p  /></body></html>";
            curTest = new TestHtml();

            result = curTest.GetTest(testData);

            testData = "<html><br></html>";
            curTest = new TestHtml();

            result = curTest.GetTest(testData);

            testData = "<html />";
            curTest = new TestHtml();

            result = curTest.GetTest(testData);

            testData = "<html x y><div/><table /> </html>";
            curTest = new TestHtml();

            result = curTest.GetTest(testData);

            while (true)
            {
                testData = Console.ReadLine();
                curTest = new TestHtml();

                result = curTest.GetTest(testData);
            }

            myHTMLParser myParser = new myHTMLParser();
            List<Tag> myList = myParser.getTags(result, "html");

            Console.WriteLine(result);
        }
    }
}
