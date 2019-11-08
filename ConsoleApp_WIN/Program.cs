using IAuction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WIN
{
    class Program
    {
        static List<string> MailRecipients = new List<string>();
        static void Main(string[] args)
        {
            string requestFileName = "lastrequest.req";
            bool debug = false;

            string responseDir = Environment.CurrentDirectory;
            if (args.Length > 0)
                foreach (string argItem in args)
                    if (argItem == "test")
                    {
                        SendMailRemind("TEST body sending mail", "[TEST] subj", MailRecipients);
                        return;
                    }
                    else if (argItem.Contains("@")
                        & argItem.Contains("."))
                    {
                        if (argItem.IndexOf('@') > 0
                        & argItem.IndexOf('@') + 1 < argItem.IndexOf('.', argItem.IndexOf('@')))
                        {
                            MailRecipients.Add(argItem);
                        }
                    }                         
                    
                    else if (argItem.Contains("request="))
                    {
                        requestFileName = argItem.Substring(7);
                    }
                    else if (argItem.Contains("responsedir="))
                    {
                        responseDir = argItem.Substring(11);
                    }
                    else if (argItem == "debug")
                    {
                        debug = true;
                    }
            

            List<IRequest> myReqObjects = new List<IRequest>();
            List<IResponse> myRespObjects = new List<IResponse>();

            if (File.Exists(requestFileName))
            {
                //myReqObjects.Add(FileIO.LoadMyRequestObjectXML(requestFileName));
                // здесь надо загружать объекты запросов из xml. однако, это не работает для интерфейсов.
                //myReqObjects.Add(myRequestObject.LoadFromXML(requestFileName));                
                Console.WriteLine("Processing with REQUEST files is not realized yet! Please, remove ARG: \"request=\" or remove FILE \"lastrequest.req\" from DIR: \"" + responseDir + "\"");
            }
            else
            {
                if (Directory.Exists(responseDir))
                {
                    if (debug)
                        Console.WriteLine("Resp.dir: " + responseDir);
                    foreach (string item in Directory.GetFiles(responseDir, "*.resp"))
                    {
                        if (debug)
                            Console.WriteLine("Resp.found: " + item);
                        IResponse curResp = SFileIO.LoadMyObject(item);
                        if (curResp != null)
                        {
                            myRespObjects.Add(curResp);
                            if (debug)
                                Console.WriteLine("Resp.load: " + item);
                        }
                        else
                        {
                            if (debug)
                                Console.WriteLine("Resp. NOT load: " + item);
                        }
                        

                    }

                    foreach (string item in Directory.GetFiles(responseDir, "*.req"))
                    {
                        if (debug)
                            Console.WriteLine("Req.found: " + item);
                        IRequest curReq = null;
                        //IRequest curReq = myRequestObject.LoadFromXML(item);
                        // здесь надо загружать объекты запросов из xml. однако, это не работает для интерфейсов.
                        //IRequest curReq = FileIO.LoadMyRequestObjectXML(item);
                        if (curReq != null)
                        {
                            myReqObjects.Add(curReq);
                            if (debug)
                                Console.WriteLine("Req.load: " + item);
                        }
                        else
                        {
                            if (debug)
                                Console.WriteLine("Req. NOT load: " + item);
                        }

                    }
                }                
            }


            if (myRespObjects.Count == 0)
            {
                if (myReqObjects.Count == 0)
                {
                    Console.WriteLine("I can't work without any Requests or Responses! Bye..");
                    return;
                }
                else
                {
                    Console.WriteLine("Will check " + myReqObjects.Count + " items..");
                    int c = 0;
                    foreach (IRequest item in myReqObjects)
                    {
                        c++;
                        IResponse newResp = item.MakeResponse();
                        newResp.SaveToXml(newResp.SiteName.Replace(" ", "") + ".resp");
                        SendMailRemind(newResp.NewRecordsOutput(null, true), "[" + newResp.SiteName + "] Появились новые предложения!", MailRecipients);
                    }
                }

            }
            else
            {
                foreach (IResponse oldItem in myRespObjects)
                {
                    if(debug)
                        Console.WriteLine("Checking " + oldItem.SiteName + "..");
                    
                    IResponse newResp = oldItem.MakeFreshResponse;                   
                    
                    if (newResp.ListResponse == null)
                    {
                        string msg = "ERROR! \"" + newResp.SiteName + "\"";
                        if (newResp.LastError() != null)
                            msg += " : " + newResp.LastError().Message;
                        if (newResp.MyRequest.LastError() != null)
                            msg += " : " + newResp.MyRequest.LastError().Message;
                        Console.WriteLine(msg);                        
                        SendMailRemind("Ошибка получения результатов запроса от площадки! Обратитесь к разработчику!" + Environment.NewLine 
                            + "Сообщение об ошибке: " + Environment.NewLine
                            + msg, "[" + newResp.SiteName + "] ОШИБКА!", MailRecipients);
                    }
                    else if (newResp.ListResponse.Count() < 1)
                    {
                        string msg = "WARNING! \"" + newResp.SiteName + "\"";
                        if (newResp.LastError() != null)
                            msg += " : " + newResp.LastError().Message;
                        if (newResp.MyRequest.LastError() != null)
                            msg += " : " + newResp.MyRequest.LastError().Message;
                        Console.WriteLine(msg);
                        SendMailRemind("Ответ получен, но не содержит результатов! Возможно, по Вашему запросу теперь ничего не найдено. Если на сайте \""
                            + newResp.SiteName + "\" по запросу \"" + newResp.MyRequest.AllParametersInString("_")
                            + "\" есть результаты, тогда обратитесь к разработчику!" + Environment.NewLine
                            + "Сообщение об ошибке: " + Environment.NewLine
                            + msg, "[" + newResp.SiteName + "] ВНИМАНИЕ! Получен пустой ответ.", MailRecipients);
                    }
                    else if (newResp.HaveNewRecords(oldItem))
                    {
                        Console.WriteLine("Found new records for \"" + newResp.SiteName + "\"!");
                        newResp.SaveToXml(newResp.SiteName.Replace(" ", "") + ".resp");
                        SendMailRemind(newResp.NewRecordsOutput(oldItem, true), "[" + newResp.SiteName + "] Появились новые предложения!", MailRecipients);
                    }
                    else
                    {
                        if (debug)
                            Console.WriteLine("Recieved " + newResp.ListResponse.Count() + " items");
                        Console.WriteLine(newResp.SiteName + ": Nothing new..");
                    }
                }                    
            }                
            Console.WriteLine("Well done!");
        }

        static bool SendMailRemind(string outText, string outSubj = "[Торговая площадка] Появились новые предложения по Вашему запросу!", List<string> recpList = null)
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
            myMessage.Subject = outSubj + " [" + Environment.MachineName + "]";
            myMessage.Body = outText;
            myMessage.IsBodyHtml = true;
            

            try
            {
                mySmtp.Send(myMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when sending E-MAIL: " + e.Message);
                File.AppendAllText("error.log", DateTime.Now.ToString("yyyy.MM.dd|hh:mm:ss") 
                    + "\t:\tНе удалось отправить писмо по причине:"  + Environment.NewLine
                    + e.Message + Environment.NewLine
                    + "========================-Содержимое письма-========================" + Environment.NewLine
                    //+ "Содержимое письма:" + Environment.NewLine
                    + "------------------------=----= ТЕМА =-----=------------------------" + Environment.NewLine
                    //+ "Тема:\t" 
                    + myMessage.Subject + Environment.NewLine
                    + "------------------------=--= СООБЩЕНИЕ =--=------------------------" + Environment.NewLine
                    //+ "Сообщение:\t" 
                    + myMessage.Body + Environment.NewLine
                    + "===================================================================" + Environment.NewLine
                    + Environment.NewLine);
                //throw;
                return false;
            }
        }
    }
}
