using System;
using TorgiASV;
using System.Collections.Generic;
using System.IO;
using MyHTMLParser;
using IAuction;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            ATorgRequest testReq;
            testReq = new CenterrRequestNew("пирит");
            string fileName = testReq.Type + "_" + testReq.AllParametersInString() + ".req";
            testReq.SaveToXml(fileName);

            testReq = new ASVRequestNew();
            testReq = (ASVRequestNew)testReq.LoadFromXML(fileName);
            
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
