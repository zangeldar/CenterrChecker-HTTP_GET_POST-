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
            string testStr = @"<ul class=""menu__list menu__list--table width-full>";
            TESTfillAttr(testStr);



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
            result = curTest.GetTest(testData, true);

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
            List<Tag> myList = myParser.getTags(result, "ul");
            List<Tag> resList = new List<Tag>();

            bool found = false;
            foreach (Tag item in myList)
            {
                foreach (tagAttribute atItem in item.Attributes)
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
            foreach (Tag item in resList[0].InnerTags)
            {
                resASV.Add(new ASV(item.InnerTags));
            }

            Console.WriteLine(resASV);

        }

        static private void TESTfillAttr(string tagNameContent)
        {
            List<tagAttribute> tagAttrList = new List<tagAttribute>();

            int endOFTagName = tagNameContent.IndexOf('>');
            string onlyTagName = tagNameContent.Substring(1, endOFTagName - 1);

            int c;
            string attrName = "";
            string attrValue = "";
            //foreach (string item in tagNameContent.Split(' '))

            ////////////
            /// ПЕРЕРАБОТАТЬ:
            /// ////////////
            /// Работает некорректно для случая, когд
            /// onlyTagName = "ul class="menu__list menu__list--table width-full""
            /// потому что пробел содержится между кавычек
            /// 
            string tagName = "";
            string attName = "";
            string attValue = "";

            bool openQuotes = false;
            bool nextAtt = false;
            bool nextValue = false;

            string currentStr = "";
            foreach (char item in onlyTagName)
            {
                if ((item == ' ') & !openQuotes)
                {
                    if (!nextAtt)
                        tagName = currentStr;
                    else
                        attName = currentStr; // Точно ли имя аттрибута?? 
                    currentStr = "";

                    nextAtt = true;
                    continue;
                }
                else if ((item == '=') & !openQuotes)
                {
                    attName = currentStr;
                    currentStr = "";
                    nextValue = true;                    
                    continue;
                }
                else if (item == '"')
                {
                    openQuotes = !openQuotes;
                }
                currentStr += item;
            }


            foreach (string item in onlyTagName.Split(' '))
            {
                c = 0;
                foreach (string itemAtt in item.Split('='))
                {
                    switch (c)
                    {
                        case 0:
                            attrName = itemAtt;
                            break;
                        case 1:
                            attrValue = itemAtt;//.Replace('\"','');
                            break;
                        default:
                            break;
                    }
                    c++;
                }
                if ((c > 1) & (attrName.Length > 0) & (attrValue.Length > 0))
                    tagAttrList.Add(new tagAttribute(attrName, attrValue));
            }
        }
    }
}
