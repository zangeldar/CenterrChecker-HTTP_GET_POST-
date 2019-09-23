using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace HTTP_GET_POST
{
    class Program
    {
        static List<string> MailRecipients = new List<string>();
        static void Main(string[] args)
        {
            if (args.Length > 0)
                foreach (string argItem in args)
                    if (argItem == "test")
                    {
                        SendMailRemind("TEST body sending mail", "[TEST] subj", MailRecipients);
                        return;
                    }                        
                    else if (argItem.Contains('@')
                        & argItem.Contains('.') 
                        & argItem.IndexOf('@')>0 
                        & argItem.IndexOf('@')+1 < argItem.IndexOf('.', argItem.IndexOf('@')))
                        MailRecipients.Add(argItem);

            MyClass myObject = new MyClass();
            string checkDate;
            
            //  Запрос АСВ по имуществу ПРБ в отношении ПИРИТ
            myObject.MyParameters["Party_contactName"] = "асв";
            myObject.MyParameters["vPurchaseLot_fullTitle"] = "прб";
            myObject.MyParameters["vPurchaseLot_lotTitle"] = "пирит";
            checkDate = "19.12.2017 14:00";   // для ПИРИТ по ПРБ
            DoOneCheck(myObject, checkDate);

            //  Запрос АСВ по имуществу СОЮЗНЫЙ
            myObject.ResetParameters();
            myObject.MyParameters["Party_contactName"] = "асв";
            myObject.MyParameters["vPurchaseLot_fullTitle"] = "союзный";
            myObject.MyParameters["vPurchaseLot_lotTitle"] = "";
            checkDate = "";   // для СОЮЗНЫЙ
            DoOneCheck(myObject, checkDate);

            myObject.ResetParameters();
            myObject.MyParameters["Party_contactName"] = "асв";
            myObject.MyParameters["vPurchaseLot_fullTitle"] = "";
            myObject.MyParameters["vPurchaseLot_lotTitle"] = "";

            CenterrRequestResponseObject checkData = null;// = LoadMyCenterrObject(GenerateFileName(myObject));
            if (File.Exists(GenerateFileName(myObject)))
                checkData = LoadMyCenterrObject(GenerateFileName(myObject));

            DoOneCheck(myObject, checkData);

            Console.WriteLine("Well done!");
            //Console.ReadKey();
        }
        
        static bool DoOneCheck(MyClass myObject, string checkDate)
        {
            string myWorkAnswer;
            string resultString;
            //string checkDate;

            myWorkAnswer = myObject.getResponse;

            //  Разбор результатов
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!
            List<List<StringUri>> myLT = GetResultTableAsList(myTable);
            List<CenterrTableRowItem> myCrObjects = GetResultTableAsListOfMyObjects(myLT);
            DataTable myDT = GetTableAsDT(myLT);

            // Проверка на наличие новых сообщений и отправка почты            
            resultString = GetNewRowsString(myLT, checkDate);
            if (resultString.Length > 0)
                return SendMailRemind(PrepareMailBody(myObject, resultString));
            // Конец запроса

            return false;
        }

        static bool DoOneCheck(MyClass myObject, CenterrRequestResponseObject checkData=null)
        {
            string myWorkAnswer;
            string resultString;
            //string checkDate;            
            CenterrRequestResponseObject CurrentObj;

            myWorkAnswer = myObject.getResponse;

            //  Разбор результатов
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!
            List<List<StringUri>> myLT = GetResultTableAsList(myTable);
            List<CenterrTableRowItem> myCrObjects = GetResultTableAsListOfMyObjects(myLT);

            CurrentObj = new CenterrRequestResponseObject(myObject, myCrObjects[0]);
            if (checkData != null)
                //if (myObject.MyParameters == checkData.MyRequest.MyParameters)
                if (Enumerable.SequenceEqual(myObject.MyParameters, checkData.MyRequest.MyParameters))
                    if (CurrentObj.LastResponse.ToString() == checkData.LastResponse.ToString())
                        return false;

            // сохранить сериализованный CurrentObj для дальнейших проверок
            SaveMyCenterrObject(CurrentObj, GenerateFileName(CurrentObj.MyRequest));

            return SendMailRemind(PrepareMailBody(myObject, CurrentObj.LastResponse.ToString()));
        }

        static string GenerateFileName(MyClass inpObj)
        {
            string result = "";

            foreach (var item in inpObj.MyParameters)
            {
                if (item.Value == "" || item.Value == "10,11,12,111,13")
                    continue;
                
                result += item.Value;
            }

            return result + ".bcntr";
        }

        static bool SaveMyCenterrObject(CenterrRequestResponseObject curObj, string fileName = "lastresponse.bcntr")
        {
            bool result = false;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream output = File.OpenWrite(fileName))
                {
                    bf.Serialize(output, curObj);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                //throw;
            }

            return result;
        }

        static CenterrRequestResponseObject LoadMyCenterrObject(string fileName = "lastresponse.bcntr")
        {
            CenterrRequestResponseObject result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (CenterrRequestResponseObject)bf.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }
        
        static string GetNewRowsString(List<List<StringUri>> inpLT, string checkDate)
        {
            string outRows = "";
            for (int i = 1; i < inpLT.Count - 1; i++)   // первая строка - заголовки колонок
            {
                if (!HaveNewRecords(inpLT[i], checkDate))
                    break;
                foreach (StringUri item in inpLT[i])
                    outRows += "\t|\t" + item.ItemString;
                outRows += "\n";
            }
            //if (outRows.Length == 0)
            //    return null;
            return outRows;
        }
        static bool HaveNewRecords(List<StringUri> checkRow, string lastKnownDate)
        {
            string checkDate = checkRow[6].ItemString;
            if (checkDate != lastKnownDate)
                return true;
            return false;
        }

        static string PrepareMailBody(MyClass myObject, string inpRows)
        {
            string messageBody;

            string parSet = "";
            foreach (string item in myObject.MyParameters.Values)
            {
                if (item.Length > 0 & item != "10,11,12,111,13")
                    parSet += ", " + item;
            }
            if (parSet.Length > 2)
                parSet = parSet.Remove(0, 2);

            messageBody = String.Format(@"По Вашему запросу: ""{0}"" были обнаружены новые записи:\n{1}", parSet, inpRows);

            return messageBody;
        }

        static bool SendMailRemind(string outText, string outSubj="[Центр Реализации] Появилось новое предложение по Вашему запросу!", List<string> recpList=null)
        {
            string mailFrom = "bot@nazmi.ru";

            SmtpClient mySmtp = new SmtpClient("smtp.yandex.ru", 25);            
            mySmtp.EnableSsl = true;
            mySmtp.Credentials = new NetworkCredential(mailFrom, "p@ssw0rd");

            MailMessage myMessage = new MailMessage();            
            myMessage.From = new MailAddress(mailFrom);
            myMessage.To.Add(new MailAddress("eldar@nazmi.ru"));
            if (recpList != null)
                foreach (string item in recpList)
                    myMessage.To.Add(new MailAddress(item));
            myMessage.Subject = outSubj;
            myMessage.Body = outText;            

            try
            {
                mySmtp.Send(myMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when sending: " + e.Message);
                //throw;
                return false;
            }
        }

        static void OutWholeTree(Tag inpTag)
        {
            Console.WriteLine(inpTag.ToString());
            if (inpTag.HasInnerTags)
            {
                Console.Write("\n\t");
                foreach (Tag innTag in inpTag.InnerTags)
                    OutWholeTree(innTag);
            }
        }

        static List<List<StringUri>> GetResultTableAsList(List<List<StringUri>> inpList)
        {
            List<List<StringUri>> resList = new List<List<StringUri>>();

            // 1. Calculate MAX columns count
            int colCount = 0;
            foreach (List<StringUri> itemList in inpList)
            {
                colCount = Math.Max(colCount, itemList.Count);
            }

            // 2. Fill result rows
            foreach (List<StringUri> itemListRows in inpList)
            {
                if (itemListRows.Count != colCount)     // Skip all rows that have not another count of columns instead MAX columns count
                    continue;

                resList.Add(itemListRows);
            }

            return resList;
        }

        static List<CenterrTableRowItem> GetResultTableAsListOfMyObjects(List<List<StringUri>> inpList)
        {
            List<CenterrTableRowItem> resList = new List<CenterrTableRowItem>();

            for (int i = 1; i < inpList.Count; i++)
            {
                resList.Add(new CenterrTableRowItem(inpList[i]));
            }

            return resList;
        }

        static DataTable GetTableAsDT(List<List<StringUri>> inpTable)
        {
            DataTable resDT = new DataTable();

            foreach (StringUri item in inpTable[0])
                resDT.Columns.Add(item.ItemString);

            for (int i = 1; i < inpTable.Count-1; i++)
                resDT.Rows.Add(inpTable[i]);

            return resDT;
        }

    }
}
