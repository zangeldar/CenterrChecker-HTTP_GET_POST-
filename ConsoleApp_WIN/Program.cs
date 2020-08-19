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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string requestFileName = "lastrequest.req";
            bool debug = false;
            bool noDelReq = false;

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
                        requestFileName = argItem.Substring(8);
                    }
                    else if (argItem.Contains("responsedir="))
                    {
                        responseDir = argItem.Substring(12);
                    }
                    else if (argItem == "debug")
                    {
                        debug = true;
                        noDelReq = true;
                    }
                    else if (argItem == "no-del-req")
                    {
                        noDelReq = true;
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
                        IResponse curResp = SFileIO.LoadMyResponse(item);
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

                        string checkType="";
                        
                        using (StreamReader sr = new StreamReader(item))
                        {
                            sr.ReadLine(); //читаем первую строку "<?xml version="1.0"?>"
                            checkType = sr.ReadLine();
                            checkType = checkType.Substring(1, checkType.IndexOf(' ')-1);
                        }
                        
                        //IRequest curReq = myRequestObject.LoadFromXML(item);
                        // здесь надо загружать объекты запросов из xml. однако, это не работает для интерфейсов.
                        //IRequest curReq = FileIO.LoadMyRequestObjectXML(item);
                        //object deserObj = SFileIO.LoadMyRequestObjectXML(item, checkType);                        

                        switch (checkType)
                        {
                            case "TorgASVRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new TorgiASV.TorgASVRequest(), item);
                                break;
                            case "CenterrRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new CenterRu.CenterrRequest(), item);
                                break;
                            case "ASVorgRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new ASVorgRU.ASVorgRequest(), item);
                                break;
                            case "SberbankAstRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new SberbankAST.SberbankAstRequest(), item);
                                break;
                            case "B2BRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new B2B.B2BRequest(), item);
                                break;
                            case "GPBRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new ETP_GPB.GPBRequest(), item);
                                break;
                            case "UTenderRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new UTender.UTenderRequest(), item);
                                break;
                            case "ZakupkiGovRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new ZakupkiGov.ZakupkiGovRequest(), item);
                                break;
                            case "TekTorgRequest":
                                curReq = ATorgRequest.LoadMyRequestObjectXML(new TekTorg.TekTorgRequest(), item);
                                break;
                            default:
                                Console.WriteLine("Unknown request type: " + checkType + " of file: " + item + ". Check necessary DLLs!");
                                break;
                        }
                        if (curReq != null)
                        {
                            if (debug)
                                Console.WriteLine("Req.load: " + item);
                            Console.Write(checkType + " found! Get result..");
                            myReqObjects.Add(curReq);                            
                            IResponse newResp = curReq.MakeResponse();
                            Console.WriteLine(" SUCCESS");
                            //newResp.SaveToXml((newResp.SiteName + "_" + newResp.MyRequest.SearchString).Replace(" ", "") + ".resp");
                            SFileIO.SaveMyResponse(newResp, responseDir + "\\" + (newResp.SiteName + "_" + newResp.MyRequest.SearchString).Replace(" ", "") + ".resp");
                            if (newResp.ListResponse != null)
                            {
                                if (newResp.ListResponse.Count() > 0)
                                    SendMailRemind(newResp.NewRecordsOutput(null, true), "[" + newResp.SiteName + "] Предложения по новым запросам!", MailRecipients);
                                else // ответ есть, но пустой
                                {
                                    SendMailRemind("Ответ получен, но не содержит результатов! Возможно, по Вашему запросу теперь ничего не найдено. Если на сайте \""
                                        + newResp.SiteName + "\" по запросу \"" + newResp.MyRequest.AllParametersInString("_")
                                        + "\" есть результаты, тогда обратитесь к разработчику!" + Environment.NewLine
                                        + "Сообщение об ошибке: " + Environment.NewLine
                                        + "WARNING! \"" + newResp.SiteName + "\"", "[" + newResp.SiteName + "] ВНИМАНИЕ! Результаты для НОВОГО запроса не найдены.", MailRecipients);
                                }
                            }
                            else // ответ поломан
                            {
                                string msg = "ERROR! \"" + newResp.SiteName + "\"";
                                if (newResp.LastError() != null)
                                    msg += " : " + newResp.LastError().Message;
                                if (newResp.MyRequest.LastError() != null)
                                    msg += " : " + newResp.MyRequest.LastError().Message;
                                Console.WriteLine(msg);
                                SendMailRemind("Ошибка получения результатов НОВОГО запроса! Обратитесь к разработчику!" + Environment.NewLine
                                    + "Сообщение об ошибке: " + Environment.NewLine
                                    + msg, "[" + newResp.SiteName + "] ОШИБКА!", MailRecipients);
                            }

                            if (!noDelReq)
                            {
                                try
                                {
                                    File.Delete(item);
                                }
                                catch (Exception e)
                                {
                                    //throw;
                                    Console.WriteLine("ERROR: Couldn't delete unused request file: " + item);
                                }
                            }
                        }
                        else
                        {                            
                            if (debug)
                                Console.WriteLine("Req. NOT load: " + item);
                        }
                    }
                }                
            }

            if ((myReqObjects.Count == 0) & (myRespObjects.Count == 0))
            {
                Console.WriteLine("I can't work without any Requests or Responses! Bye..");
                return;
            }
            //AREA requests check
            {
                foreach (IRequest item in myReqObjects)
                {
                    
                }                
            }
            // AREA responses re-check
            {
                foreach (IResponse oldItem in myRespObjects)
                //foreach (ATorgResponse oldItem in myRespObjects)
                {
                    if(debug)
                        Console.WriteLine("Checking " + oldItem.SiteName + "..");

                    IResponse newResp = oldItem.MakeFreshResponse;                   
                    //ATorgResponse newResp = oldItem.MakeFreshResponse;

                    if (newResp.ListResponse == null)
                    {
                        string msg = "ERROR! \"" + newResp.SiteName + "\"";
                        if (newResp.LastError() != null)
                            msg += " : " + newResp.LastError().Message;
                        if (newResp.MyRequest.LastError() != null)
                            msg += " : " + newResp.MyRequest.LastError().Message;
                        Console.WriteLine(msg);
                        // здесь надо сохранять ошибку, чтобы на случай повтора ошибки не забивать почту уведомлениями
                        // если ошибка была ранее, то не отправлять уведомление
                        // в случае получения нормального ответа в другом месте удалять файл-флаг ошибки
                        SendMailRemind("Ошибка получения результатов запроса от площадки! Обратитесь к разработчику!" + Environment.NewLine 
                            + "Сообщение об ошибке: " + Environment.NewLine
                            + msg, "[" + newResp.SiteName + "] ОШИБКА!", MailRecipients);
                    }
                    /*
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
                    */
                    else if (newResp.HaveNewRecords(oldItem))
                    {
                        Console.WriteLine("Found changes for \"" + newResp.SiteName + "\"!");
                        newResp.SaveToXml(responseDir + "\\" + (newResp.SiteName + "_" + newResp.MyRequest.SearchString).Replace(" ", "") + ".resp", true);
                        
                        if (newResp.ListResponse.Count() < 1)
                        {
                            // исчезли старые записи
                            SendMailRemind("Ответ получен, но не содержит результатов! Возможно, по Вашему запросу теперь ничего не найдено. Если на сайте \""
                            + newResp.SiteName + "\" по запросу \"" + newResp.MyRequest.AllParametersInString("_")
                            + "\" есть результаты, тогда обратитесь к разработчику!" + Environment.NewLine
                            + "Сообщение об ошибке: " + Environment.NewLine
                            + "WARNING! \"" + newResp.SiteName + "\"", "[" + newResp.SiteName + "] ВНИМАНИЕ! Получен пустой ответ.", MailRecipients);
                        }
                        else
                        {
                            // появились новые записи 
                            SendMailRemind(newResp.NewRecordsOutput(oldItem, true), "[" + newResp.SiteName + "] Произошли изменения!", MailRecipients);
                        }                        
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

