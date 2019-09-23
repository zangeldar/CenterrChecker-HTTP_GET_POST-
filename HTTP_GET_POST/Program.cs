using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            List<List<string>> myTable = new List<List<string>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!
            List<List<string>> myLT = GetResultTableAsList(myTable);
            DataTable myDT = GetTableAsDT(myLT);

            // Проверка на наличие новых сообщений и отправка почты            
            resultString = GetNewRowsString(myLT, checkDate);
            if (resultString.Length > 0)
                return SendMailRemind(PrepareMailBody(myObject, resultString));
            // Конец запроса

            return false;
        }

        static string GetNewRowsString(List<List<string>> inpLT, string checkDate)
        {
            string outRows = "";
            for (int i = 1; i < inpLT.Count - 1; i++)   // первая строка - заголовки колонок
            {
                if (!HaveNewRecords(inpLT[i], checkDate))
                    break;
                foreach (string item in inpLT[i])
                    outRows += "\t|\t" + item;
                outRows += "\n";
            }
            //if (outRows.Length == 0)
            //    return null;
            return outRows;
        }
        static bool HaveNewRecords(List<string> checkRow, string lastKnownDate)
        {
            string checkDate = checkRow[6];
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

        static List<List<string>> GetResultTableAsList(List<List<string>> inpList)
        {
            List<List<string>> resList = new List<List<string>>();

            // 1. Calculate MAX columns count
            int colCount = 0;
            foreach (List<string> itemList in inpList)
            {
                colCount = Math.Max(colCount, itemList.Count);
            }

            // 2. Fill result rows
            foreach (List<string> itemListRows in inpList)
            {
                if (itemListRows.Count != colCount)     // Skip all rows that have not another count of columns instead MAX columns count
                    continue;

                resList.Add(itemListRows);
            }

            return resList;
        }

        static DataTable GetTableAsDT(List<List<string>> inpTable)
        {
            DataTable resDT = new DataTable();

            foreach (string item in inpTable[0])
                resDT.Columns.Add(item);

            for (int i = 1; i < inpTable.Count-1; i++)
                resDT.Rows.Add(inpTable[i]);

            return resDT;
        }

    }
}
