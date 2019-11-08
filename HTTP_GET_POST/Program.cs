using CenterrRu;
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


    }
}
