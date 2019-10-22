using CenterrRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using TorgiASV;

namespace ConsoleApp
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
                        & argItem.IndexOf('@') > 0
                        & argItem.IndexOf('@') + 1 < argItem.IndexOf('.', argItem.IndexOf('@')))
                    {
                        MailRecipients.Add(argItem);
                    }
                    else if (argItem.Contains("request="))
                    {
                        requestFileName = argItem.Substring(7);
                    }

            IRequest myRequestObject = null;
            //string checkDate;

            if (File.Exists(requestFileName))
                myRequestObject = myRequestObject.LoadFromXML(requestFileName);               

            if (myRequestObject == null)
            {
                // Сделать новый запрос по умолчанию, или сообщить об отсутствии запроса и выйти.
                myRequestObject = new CenterrRequest();
                //Запрос АСВ по имуществу ПРБ в отношении ПИРИТ
                myRequestObject.ResetParameters();
                myRequestObject.MyParameters["Party_contactName"] = "асв";
                myRequestObject.MyParameters["vPurchaseLot_fullTitle"] = "";
                myRequestObject.MyParameters["vPurchaseLot_lotTitle"] = "";
                //myRequestObject.SaveToXml(myRequestObject.CreateFileName(true));
                myRequestObject.SaveToXml(myRequestObject.ToString() + ".req");
            }
            //SaveMyRequestObjectXML(myRequestObject, "lastrequest.req");
            myRequestObject.SaveToXml("lastrequest.req");

            IResponse curData;
            IResponse checkData = null;// = LoadMyCenterrObject(GenerateFileName(myRequestObj));
            if (File.Exists(myRequestObject.ToString() + ".obj"))
                checkData = checkData.LoadFromXml(myRequestObject.ToString() + ".obj");
                        
            if (checkData is CenterrResponse)
                curData = new CenterrResponse((CenterrRequest)myRequestObject);
            else if (checkData is ASVResponse)
                curData = new ASVResponse((ASVRequest)myRequestObject);
            else
                curData = null;

            if (curData != null)
                if (curData.HaveNewRecords(checkData))
                    SendMailRemind(curData.NewRecordsOutput(true), "[curData.SiteName] Появились новый предложения!");

            Console.WriteLine("Well done!");
            //Console.ReadKey();
        }






        static bool SendMailRemind(string outText, string outSubj = "[Центр Реализации] Появились новые предложения по Вашему запросу!", List<string> recpList = null)
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


    }
}
