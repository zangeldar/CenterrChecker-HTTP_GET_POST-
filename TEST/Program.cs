using System;
using TorgiASV;
using System.Collections.Generic;
using System.IO;
using MyHTMLParser;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StringUri url11 = new StringUri() { ItemString = "string1", ItemUri = "uri1" };
            StringUri url12 = new StringUri() { ItemString = "string1", ItemUri = "uri2" };
            StringUri url21 = new StringUri() { ItemString = "string2", ItemUri = "uri1" };
            StringUri url22 = new StringUri() { ItemString = "string2", ItemUri = "uri2" };
            StringUri url00 = new StringUri() { ItemString = "string1", ItemUri = "uri1" };

            Console.WriteLine(url11.Equals(url00));
            Console.WriteLine(url11.Equals(url12));
            Console.WriteLine(url11.Equals(url21));
            Console.WriteLine(url11.Equals(url22));



            //string testStr = @"<ul class=""menu__list menu__list--table width-full>";
            //TESTfillAttr(testStr);



            //Parser.MyParse(File.ReadAllText("20191018_HTML.txt"));



            //Console.WriteLine("Hello World!");

            //HTTP_GET_POST.Program.SendMailRemind(HTTP_GET_POST.Program.PrepareMailBody(null, "", 20));            
            string testData;
            ITest curTest;
            string result = "";

            //testData = @"/search/?q=g";
            //testData = @"/search/?show=lot&q=g";
            testData = @"/search/?show=lot&q=пирит";            
            testData = @"/search/?q=пирит";
            testData = @"/search/?q=g";            
            testData = @"g";
            testData = @"пирит";

            //curTest = new TestASV();
            //result = curTest.GetTest(testData, false);

            curTest = new TestTorg();

            result = curTest.GetTest(testData,"cntr");
            Console.WriteLine(result);
            File.WriteAllText("cntr.result.csv", result, System.Text.Encoding.UTF8);

            result = curTest.GetTest(testData, "tasv");
            Console.WriteLine(result);
            File.WriteAllText("tasv.result.csv", result, System.Text.Encoding.UTF8);
        }
    }
}
