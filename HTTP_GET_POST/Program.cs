using Centerr_ru;
using MyHTMLParser;
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
using System.Xml.Serialization;

namespace HTTP_GET_POST
{
    class Program
    {
        static List<string> MailRecipients = new List<string>();
        static void Main(string[] args)
        {
            string requestFileName = "lastrequest.req";
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
                    {
                        MailRecipients.Add(argItem);
                    }
                    else if (argItem.Contains("request="))
                    {
                        requestFileName = argItem.Substring(7);
                    }                        
            

            CenterrRequest myRequestObject = null;
            string checkDate;

            /*
            //  Запрос АСВ по имуществу ПРБ в отношении ПИРИТ
            myRequestObj.MyParameters["Party_contactName"] = "асв";
            myRequestObj.MyParameters["vPurchaseLot_fullTitle"] = "прб";
            myRequestObj.MyParameters["vPurchaseLot_lotTitle"] = "пирит";
            checkDate = "19.12.2017 14:00";   // для ПИРИТ по ПРБ
            DoOneCheck(myRequestObj, checkDate);

            //  Запрос АСВ по имуществу СОЮЗНЫЙ
            myRequestObj.ResetParameters();
            myRequestObj.MyParameters["Party_contactName"] = "асв";
            myRequestObj.MyParameters["vPurchaseLot_fullTitle"] = "союзный";
            myRequestObj.MyParameters["vPurchaseLot_lotTitle"] = "";
            checkDate = "";   // для СОЮЗНЫЙ
            DoOneCheck(myRequestObj, checkDate);
            */

            if (File.Exists(requestFileName))
                myRequestObject = LoadMyRequestObjectXML(requestFileName);

            if (myRequestObject == null)
            {
                myRequestObject = new CenterrRequest();
                //Запрос АСВ по имуществу ПРБ в отношении ПИРИТ
                myRequestObject.ResetParameters();
                myRequestObject.MyParameters["Party_contactName"] = "асв";
                myRequestObject.MyParameters["vPurchaseLot_fullTitle"] = "";
                myRequestObject.MyParameters["vPurchaseLot_lotTitle"] = "";                
                SaveMyRequestObjectXML(myRequestObject, GenerateFileName(myRequestObject, true));
            }
            SaveMyRequestObjectXML(myRequestObject, "lastrequest.req");

            CenterrResponse checkData = null;// = LoadMyCenterrObject(GenerateFileName(myRequestObj));
            if (File.Exists(GenerateFileName(myRequestObject)))
                checkData = LoadMyCenterrObject(GenerateFileName(myRequestObject));

            //DoOneCheck(myRequestObject, checkData);
            CenterrResponse curData = new CenterrResponse(myRequestObject);
            List<CenterrTableRowItem> newResultsList = DoOneCheck(curData, checkData);
            SaveMyCenterrObject(curData, GenerateFileName(curData.MyRequest));

            if (newResultsList.Count > 0)
            {
                string itemsTableHtml = CreateTableForMailing(newResultsList);
                string mailText = PrepareMailBody(curData.MyRequest, itemsTableHtml, newResultsList.Count);
                SendMailRemind(mailText);
            }            

            Console.WriteLine("Well done!");
            //Console.ReadKey();
        }
        
        static bool DoOneCheck(CenterrRequest myRequestObj, string checkDate)
        {
            string myWorkAnswer;
            string resultString;
            //string checkDate;

            myWorkAnswer = myRequestObj.GetResponse;

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
                return SendMailRemind(PrepareMailBody(myRequestObj, resultString, 1));
            // Конец запроса

            return false;
        }

        static bool DoOneCheck(CenterrRequest myRequestObj, CenterrResponse checkData=null)
        {
                       
            CenterrResponse CurrentObj;

            string myWorkAnswer = myRequestObj.GetResponse;

            //  Разбор результатов
            myHTMLParser myHtmlParser = new myHTMLParser();
            List<Tag> myTagRes = myHtmlParser.getTags(myWorkAnswer, "table");
            List<List<StringUri>> myTable = new List<List<StringUri>>();
            foreach (var item in myTagRes)
                myTable = myHtmlParser.getOutTable(item);
            // LAST myTable - is RIGHT table!            
            //List<CenterrTableRowItem> myCrObjects = GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));
            List<CenterrTableRowItem> myCrObjects = CenterrResponse.GetResultTableAsListOfMyObjects(GetResultTableAsList(myTable));

            List<CenterrTableRowItem> newItems = new List<CenterrTableRowItem>();

            CurrentObj = new CenterrResponse(myRequestObj, myCrObjects);
            
            if (checkData != null)
                //if (myRequestObj.MyParameters == checkData.MyRequest.MyParameters)
                if (Enumerable.SequenceEqual(myRequestObj.MyParameters, checkData.MyRequest.MyParameters))
                {
                    newItems = GetListOfNewRecords(CurrentObj.ListResponse, checkData.ListResponse[0]);
                    if (newItems.Count < 1)
                        return false;
                } else
                    newItems = myCrObjects;
            else
                newItems = myCrObjects;            

            // сохранить сериализованный CurrentObj для дальнейших проверок
            SaveMyCenterrObject(CurrentObj, GenerateFileName(CurrentObj.MyRequest));

            return SendMailRemind(PrepareMailBody(myRequestObj, CreateTableForMailing(newItems), newItems.Count));
        }

        static List<CenterrTableRowItem> DoOneCheck(CenterrResponse newData, CenterrResponse checkData=null)
        {
            if (checkData != null)
                if (Enumerable.SequenceEqual(newData.MyRequest.MyParameters, checkData.MyRequest.MyParameters))
                    return GetListOfNewRecords(newData.ListResponse, checkData.ListResponse[0]);

            return newData.ListResponse;
        }

        static List<CenterrTableRowItem> GetListOfNewRecords(List<CenterrTableRowItem> inpList, CenterrTableRowItem checkRowItem)
        {
            List<CenterrTableRowItem> result = new List<CenterrTableRowItem>();

            for (int i = 0; i < inpList.Count; i++)
            {
                if (inpList[i].ToString() == checkRowItem.ToString())
                    break;
                result.Add(inpList[i]);
            }

            return result;
        }

        static string CreateTableForMailing(List<CenterrTableRowItem> inpListResponses)
        {
            string result = @"<table border=""1"">";

            result += String.Format(@"<tr><th>" +
                @"{0}" + "</th><th>" +
                @"{1}" + "</th><th>" +
                @"{2}" + "</th><th>" +
                @"{3}" + "</th><th>" +
                @"{4}" + "</th><th>" +
                @"{5}" + "</th><th>" +
                @"{6}" + "</th><th>" +
                @"{7}" + "</th><th>" +
                @"{8}" + "</th><th>" +
                @"{9}" + "</th><th>" +
                @"{10}" + "</th></tr>",
                "№",
                "Торги",
                "№ лота",
                "Лот",
                "Нач.цена",
                "Организатор",
                "Дата приема заявок",
                "Дата проведения",
                "Статус",
                "Победитель",
                "Тип торга"
                );

            foreach (CenterrTableRowItem item in inpListResponses)
                result += item.ToString();

            result += @"</table>";

            return result;
        }

        static string GenerateFileName(CenterrRequest inpObj, bool request = false)
        {
            string result = "";

            foreach (var item in inpObj.MyParameters)
            {
                if (item.Value == "" || item.Value == "10,11,12,111,13")
                    continue;
                
                result += item.Value;
            }
            if (request)
                return result + ".req";

            return result + ".bcntr";
        }

        static bool SaveMyRequestObjectXML(CenterrRequest curObj, string fileName = "lastrequest.req")
        {
            bool result = false;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(CenterrRequest));
                
                using (Stream output = File.OpenWrite(fileName))
                {
                    formatter.Serialize(output, curObj);
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

        static bool SaveMyCenterrObject(CenterrResponse curObj, string fileName = "lastresponse.bcntr")
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

        static CenterrRequest LoadMyRequestObjectXML(string fileName = "lastrequest.req")
        {
            CenterrRequest result = null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(CenterrRequest));
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (CenterrRequest)formatter.Deserialize(input);
                }
            }
            catch (Exception e)
            {
                result = null;
                //throw;
            }
            return result;
        }

        static CenterrResponse LoadMyCenterrObject(string fileName = "lastresponse.bcntr")
        {
            CenterrResponse result = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(fileName))
                {
                    result = (CenterrResponse)bf.Deserialize(input);
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

        static string PrepareMailBody(CenterrRequest myObject, string inpRows, int rowsCount)
        {
            string messageBody;

            string parSet = "";
            if (myObject != null)
            {
                foreach (string item in myObject.MyParameters.Values)
                    if (item.Length > 0 & item != "10,11,12,111,13")
                        parSet += ", " + item;
                if (parSet.Length > 2)
                    parSet = parSet.Remove(0, 2);
            }
            
            //
            messageBody = String.Format(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.=w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">");

            messageBody += String.Format(@"<html xmlns=""http://www.w3.org/1999/xhtml"">");

            messageBody += String.Format(@"<head>");
            messageBody += String.Format(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-=1"" />");
            messageBody += String.Format(@"</head>");

            messageBody += String.Format(@"<body marginwidth=""0"" marginheight=""0"" leftmargin=""0"" topmargin=""0"" style=""width: 100 % !important"">");
            messageBody += String.Format("По Вашему запросу: \"{0}\" были обнаружены новые записи:\n{1}", parSet, inpRows);
            if (rowsCount >= 20)
                messageBody += "\n\n<br> Возможно, есть и другие новые записи! Обязательно проверьте на сайте!!";
            messageBody += String.Format(@"</body>");

            messageBody += "</html>";
            return messageBody;
        }

        static bool SendMailRemind(string outText, string outSubj="[Центр Реализации] Появились новые предложения по Вашему запросу!", List<string> recpList=null)
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
            myMessage.IsBodyHtml = true;

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
                colCount = Math.Max(colCount, itemList.Count);

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
                resList.Add(new CenterrTableRowItem(inpList[i]));

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
