using System;
using TorgiASV;
using HTTP_GET_POST;
using System.Collections.Generic;
using HTMLParserNew;
using System.IO;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parser.MyParse(File.ReadAllText("20191018_HTML.txt"));



            //Console.WriteLine("Hello World!");

            //HTTP_GET_POST.Program.SendMailRemind(HTTP_GET_POST.Program.PrepareMailBody(null, "", 20));            
            string testData;
            ITest curTest;
            string result = "";

            //testData = @"/search/?q=g";
            //testData = @"/search/?show=lot&q=g";
            testData = @"/search/?show=lot&q=пирит";
            testData = @"/search/?q=g";
            testData = @"/search/?q=пирит";
            
            curTest = new TestASV();
            result = curTest.GetTest(testData, false);

            /*
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
            */

            myHTMLParser myParser = new myHTMLParser();
            List<HTTP_GET_POST.Tag> myList = myParser.getTags(result, "ul");
            List<HTTP_GET_POST.Tag> resList = new List<HTTP_GET_POST.Tag>();

            bool found = false;
            foreach (HTTP_GET_POST.Tag item in myList)
            {
                foreach (HTTP_GET_POST.tagAttribute atItem in item.Attributes)
                {
                    if (atItem.Name == "class" & atItem.Value == "\"component-list")
                    {
                        found = true;
                        break;
                    }                    
                }
                if (found)
                {
                    resList.Add(item);
                    break;
                }                    
            }

            //Console.WriteLine(result);
            Console.WriteLine(resList);

            List<ASV> resASV = new List<ASV>();
            foreach (HTTP_GET_POST.Tag item in resList[0].InnerTags)
            {
                resASV.Add(new ASV(item.InnerTags));
            }

            Console.WriteLine(resASV);

        }
    }
}
