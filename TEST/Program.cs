using System;
using TorgiASV;
using System.Collections.Generic;
using System.IO;
using MyHTMLParser;
using IAuction;
//using CenterrRu;
using CenterRu;
using SberbankAST;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using B2B;
using ETP_GPB;

namespace TEST
{
    class Program
    {
        static private void DoTest(IRequest testReq, ref IResponse testResp)
        {
            string ResultStr = "";
            string fileName = testReq.Type + "_" + testReq.AllParametersInString();
            testReq.SaveToXml(fileName + ".req");
                        
            testResp = testReq.MakeResponse();            
            testResp.SaveToXml(fileName + ".resp");

            ResultStr = testResp.NewRecordsOutput(null, false);            
            File.WriteAllText(fileName + ".result.csv", ResultStr, System.Text.Encoding.UTF8);
        }

        static void Main(string[] args)
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            Test6();
        }

        static void Test1()
        {

            IRequest testReq;
            IResponse testResp = null;

            testReq = new CenterrRequest("пирит");
            DoTest(testReq, ref testResp);
            testReq = new TorgASVRequest("пирит");
            DoTest(testReq, ref testResp);

            Console.WriteLine("Well done.");
            Console.ReadKey();

            /*
            testReq = new ASVRequest();
            testReq = (ASVRequestNew)testReq.LoadFromXML(fileName);
            */
            /*
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
            */
        }

        static void Test2()
        {
            Elasticrequest myTest = new Elasticrequest();
            myTest.Filters = new Filters();
            myTest.Filters.MainSearchBar = new MainSearchBar { Value = "техническая жидкость", Type = "best_fields", Minimum_should_match = "100%" };

            string testStr = myTest.ToString();

            SberbankAST.SberbankAstRequest myReq = new SberbankAST.SberbankAstRequest("техническая жидкость");
            testStr = myReq.GetResponse;            

            //testStr = Regex.Escape(testStr);
            string testStrUn = Regex.Unescape(testStr);
            File.WriteAllText("response.json", testStr);
            File.WriteAllText("responseUnescape.json", testStrUn);
            /*
            byte[] bytesIn = Encoding.Unicode.GetBytes(testStr);
            byte[] bytesOut = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, bytesIn);
            testStr = Encoding.UTF8.GetString(bytesOut);        
            */

            testStrUn = JsonConvert.DeserializeObject<string>(testStrUn);
            testStr = JsonConvert.DeserializeObject<string>(testStr);          


        }

        static void Test3()
        {
            string testStr = File.ReadAllText("response.json");
            string testStrUn = File.ReadAllText("responseUnescape.json");

            //Newtonsoft.Json.Linq.JObject testRoot = JsonConvert.DeserializeObject<dynamic>(testStr);

            SberbankAST.JsonResponse myResponse = JsonConvert.DeserializeObject<SberbankAST.JsonResponse>(testStr);
            string myUnescape = Regex.Unescape(myResponse.data);

            SberbankAST.JsonRoot myRoot = JsonConvert.DeserializeObject<SberbankAST.JsonRoot>(myResponse.data);

            /*
            string dataStr = testRoot.Last.Last.ToString();
            //dataStr = Regex.Unescape(dataStr);
            //dataStr = JsonConvert.DeserializeObject<string>(dataStr);
            Newtonsoft.Json.Linq.JObject testData = JsonConvert.DeserializeObject<dynamic>(dataStr);
            SberbankAST.Datarow myData = JsonConvert.DeserializeObject<SberbankAST.Datarow>(dataStr);
            */

            //string tookJson = myRoot.data;
            object dataJsonObj = JsonConvert.DeserializeObject<dynamic>(myRoot.data);
            SberbankAST.JsonResponseData dataJson = JsonConvert.DeserializeObject<SberbankAST.JsonResponseData>(myRoot.data);


            XmlSerializer ser = new XmlSerializer(typeof(SberbankAST.MyDataRow));
            SberbankAST.MyDataRow myDataRow = (SberbankAST.MyDataRow)ser.Deserialize(new StringReader(myRoot.tableXml));             
            
        }

        static void Test4()
        {
            //Создаем запрос
            SberbankAST.SberbankAstRequest myReq = new SberbankAST.SberbankAstRequest("техническая жидкость");
            //SberbankAST.SberbankAstRequest myReq = new SberbankAST.SberbankAstRequest("");

            // получаем ответ строкой
            string testStr = myReq.GetResponse;

            // парсим первый уровень ответа (JSON)
            SberbankAST.JsonResponse myResponse = JsonConvert.DeserializeObject<SberbankAST.JsonResponse>(testStr);

            // парсим второй уровень ответа (JSON)
            SberbankAST.JsonRoot myRoot = JsonConvert.DeserializeObject<SberbankAST.JsonRoot>(myResponse.data);

            // парсим третий уровень (JSON)
            SberbankAST.JsonResponseData dataJson = JsonConvert.DeserializeObject<SberbankAST.JsonResponseData>(myRoot.data);

            // парсим третий уровень (XML)
            XmlSerializer ser = new XmlSerializer(typeof(SberbankAST.MyDataRow));
            SberbankAST.MyDataRow myDataRow = (SberbankAST.MyDataRow)ser.Deserialize(new StringReader(myRoot.tableXml));

            foreach (Hit hit in myDataRow.Hits)
            {
                Console.WriteLine(new SberbankAst(hit).ToString(false));
                Console.WriteLine(new SberbankAst(hit).ToString(true));
            }
        }

        static void Test5()
        {
            B2B.B2BRequest myReq = new B2BRequest("техническая жидкость");

            //string testStr = myReq.GetResponse;

            B2BResponse myResp = new B2BResponse(myReq);
        }

        static void Test6()
        {
            GPBRequest myReq = new GPBRequest("техническая жидкость");

            string testStr = myReq.GetResponse;
            testStr = testStr.Replace(">", " >");
            myHTMLParser myhp = new myHTMLParser();
            dynamic tmp = myhp.getTags(testStr, "div");
        }

    }
}
