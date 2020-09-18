using System;
using TorgiASV;
using System.Collections.Generic;
using System.IO;
using HtmlParser;
using IAuction;
using CenterRu;
using SberbankAST;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using B2B;
using ETP_GPB;
using ASVorgRU;
using UTender;
using ZakupkiGov;
using LotOnline;
using LotOnline.Sales;
using LotOnline.Gz;
using LotOnline.Tender;
using TekTorg;

namespace TEST
{
    class Program
    {
        static private void DoTest(IRequest testReq, ref IResponse testResp)
        {
            string ResultStr = "";
            //string fileName = testReq.Type + "_" + testReq.AllParametersInString();
            string fileName = testReq.Type + "_" + testReq.SearchString.Replace(" ", "").Replace(":","").Replace("\\","").Replace("/","").Replace("*","");
            testReq.SaveToXml(fileName + ".req");
                        
            testResp = testReq.MakeResponse();            
            testResp.SaveToXml(fileName + ".resp");

            ResultStr = testResp.NewRecordsOutput(null, false);            
            File.WriteAllText(fileName + ".result.csv", ResultStr, System.Text.Encoding.UTF8);
        }

        static void Main(string[] args)
        {
            //Test0();
            Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            //Test55();
            //Test6();
            //Test65();
        }

        static void Test0()
        {
            ASVorgRequest myReq = new ASVorgRequest("пирит");
            ASVorgResponse myResp = (ASVorgResponse)myReq.MakeResponse();                        
        }

        static void Test1()
        {

            IRequest testReq;
            IResponse testResp = null;
            //testReq = new TorgASVRequest("пирит");

            /*
            testReq = new ZakupkiGovRequest("техническая жидкость");
            DoTest(testReq, ref testResp);

            testReq = new UTenderRequest("pajero");
            DoTest(testReq, ref testResp);

            testReq = new ASVorgRequest("пирит");
            DoTest(testReq, ref testResp);

            testReq = new SberbankAstRequest("техническая жидкость");
            DoTest(testReq, ref testResp);

            testReq = new GPBRequest("техническая жидкость");
            DoTest(testReq, ref testResp);

            testReq = new CenterrRequest("пирит");
            DoTest(testReq, ref testResp);

            testReq = new B2BRequest("панда");
            DoTest(testReq, ref testResp);

            testReq = new B2BRequest("техническая жидкость");
            DoTest(testReq, ref testResp);

            testReq = new TorgASVRequest("торг");
            DoTest(testReq, ref testResp);
            
            testReq = new LotOnlineRequest("техническ жидкост");
            testReq = new RadLotOnlineRequest("техническ жидкост");
            DoTest(testReq, ref testResp);
            */

            /*
            testReq = new LotOnlineSalesRequest("пирит");
            DoTest(testReq, ref testResp);
            */

            /*
            testReq = new LotOnlineGzRequest("техническ* жидкост");
            DoTest(testReq, ref testResp);
            */
            /*
            testReq = new LotOnlineTenderRequest("техническ");
            DoTest(testReq, ref testResp);
            */

            testReq = new TekTorgRequest("дом");
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
                Console.WriteLine(new SberbankAst(hit, myReq).ToString(false));
                Console.WriteLine(new SberbankAst(hit, myReq).ToString(true));
            }
        }

        static void Test5()
        {
            string fileName = "20200729_B2B_Resp.txt";                        

            B2B.B2BRequest myReq = new B2BRequest("техническая жидкость");

            File.WriteAllText(fileName, myReq.GetResponse);

            //string testStr = myReq.GetResponse;

            B2BResponse myResp = new B2BResponse(myReq);            
        }

        static void Test55()
        {
            // <img src="/images/desc_order.gif" width=7 height=7 border=0 alt="Отсортировано по убыванию">
            string testStr = "<img src=\"/images/desc_order.gif\" width=7 height=7 border=0 alt=\"Отсортировано по убыванию\">";
            List<Tag> testDoc = HTMLParser.Parse(testStr);
        }

        /*
        static void Test6()
        {
            string fileName = "ETP_GPB.txt";
            string testStr = "";
            if (!File.Exists(fileName))
            {
                GPBRequest myReq = new GPBRequest("техническая жидкость");

                //testStr = myReq.GetResponse;
                File.WriteAllText(fileName, myReq.GetResponse);
            }
            testStr = File.ReadAllText(fileName);
                        
            myHTMLParser myhp = new myHTMLParser();
            //dynamic tmp = myhp.getTags(testStr, "html");
            int startParsingFrom = 0;
            int endParsingTo = 0;
            //startParsingFrom = testStr.IndexOf("<div class=\"proceduresList proceduresList--big proceduresList--with-block-links\"");
            startParsingFrom = testStr.IndexOf("<div class=\"proceduresList proceduresList--big proceduresList--with-block-links\" data-selector=\"proceduresList\"");
            if (startParsingFrom < 0)
                startParsingFrom = testStr.IndexOf("<div class=\"proceduresList hidden proceduresList--big proceduresList--with-block-links\" data-selector=\"proceduresList\"");
            endParsingTo = testStr.IndexOf("<div class=\"hiddenPagination hiddenPagination--mb\" data-selector=\"paginationWithKeybordControls\"");
            dynamic tmp = myhp.getTags(testStr.Substring(startParsingFrom, endParsingTo - startParsingFrom), "div");
            // Попробуем отслеживать доп содержание тега
            tmp = myhp.getTags(testStr.Substring(startParsingFrom, endParsingTo-startParsingFrom), "div class=\"procedure\"");
        }
        */

        static void Test65()
        {
            string fileName = "ETP_GPB_20200727.txt";
            string testStr = "";
            if (!File.Exists(fileName))
            {
                GPBRequest myReq = new GPBRequest("техническая жидкость");

                //testStr = myReq.GetResponse;
                File.WriteAllText(fileName, myReq.GetResponse);
            }
            testStr = File.ReadAllText(fileName);

            
            List<Tag> HTMLDoc = new List<Tag>();
            /*
            Tag myTag;
            string workStr = testStr;

            workStr = workStr.Substring(workStr.IndexOf("<div class=\"proceduresList"));

            while (workStr.Length > 0)
            {
                myTag = new Tag(workStr, null);
                if (myTag.IsProto)
                    HTMLDoc.Add((ProtoTag)myTag);
                else
                    HTMLDoc.Add(myTag);
                workStr = myTag.CutOffAfter;

                while (workStr.StartsWith("</"))
                {
                    workStr = workStr.Substring(workStr.IndexOf(">")+1);
                    //continue;
                }
            }
            */

            string workStr = testStr;

            workStr = workStr.Substring(workStr.IndexOf("<div class=\"proceduresList"));

            HTMLDoc = HTMLParser.Parse(testStr);

            List<Tag> SearchResult = new List<Tag>();
            List<ProtoTag> SearchProtoResult = new List<ProtoTag>();
            foreach (ProtoTag item in HTMLDoc)
            {
                if (!item.IsProto)
                {
                    SearchResult.AddRange((IEnumerable<Tag>)((Tag)item).LookForChildTag("div", true, new KeyValuePair<string, string>("class", "procedure")));
                    SearchProtoResult.AddRange((IEnumerable<Tag>)((Tag)item).LookForChildTag(null, true));
                }
                else
                    SearchProtoResult.Add(item);
            }            

            List<GPB> GPBList = new List<GPB>();
            foreach (Tag item in SearchResult)
            {
                GPBList.Add(new GPB(item, new GPBRequest()));
            }


            HTMLDoc = HTMLParser.Parse(workStr);
        }
    }
}
