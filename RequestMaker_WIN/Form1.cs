using CenterRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorgiASV;
using ASVorgRU;
using SberbankAST;
using B2B;
using ZakupkiGov;
using UTender;
using ETP_GPB;
using TekTorg;
using RosElTorg;
using RTSTender;
using LotOnline;
using LotOnline.Gz;
using LotOnline.Sales;
using LotOnline.Tender;

namespace RequestMaker_WIN
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            MyInitialize();
        }

        

        private void MyInitialize()
        {
            foreach (KeyValuePair<string, ETPStruct> item in MyConst.ETP)
            {
                cBoxType.Items.Add(item.Key);
            }
            /*
            cBoxType.Items.Add(ASVorgRU);                                                   //  00
            cBoxType.Items.Add(B2B);                                                        //  01
            cBoxType.Items.Add(CenterRu);                                                   //  02
            cBoxType.Items.Add(ETP_GPB);                                                    //  03
            cBoxType.Items.Add(RosElTorg);                                                  //  04
            cBoxType.Items.Add(RTSTender);                                                  //  05
            cBoxType.Items.Add(SberbankAST);                                                //  06
            cBoxType.Items.Add(TekTorg);                                                    //  07
            cBoxType.Items.Add(TorgiASV);                                                   //  08
            cBoxType.Items.Add(UTender);                                                    //  09
            cBoxType.Items.Add(ZakupkiGov);                                                 //  10
            //cBoxType.Items.Add(LotOnline);                                                  //  11    // сам LotOnline не отдает результаты, он запрашивает их у сторонныих площадок. Может, имеет смысл сделать в этом запросе запросы по всем дочкам, разбирать ответы и давать объекты кастованные родительским классом  LotOnline
            cBoxType.Items.Add(LotOnlineArrested);                                          //  12
            cBoxType.Items.Add(LotOnlineConfiscate);                                        //  13
            cBoxType.Items.Add(LotOnlineFish);                                              //  14
            cBoxType.Items.Add(LotOnlineLease);                                             //  15
            cBoxType.Items.Add(LotOnlinePrivatization);                                     //  16
            cBoxType.Items.Add(LotOnlineRad);                                               //  17
            cBoxType.Items.Add(LotOnlineTrade);                                             //  18
            cBoxType.Items.Add(LotOnlineZalog);                                             //  19            
            cBoxType.Items.Add(LotOnlineGz);                                                //  20
            cBoxType.Items.Add(LotOnlineSales);                                             //  21
            cBoxType.Items.Add(LotOnlineTender);                                            //  22
            */
            cBoxType.SelectedIndex = 0;                               
            searchBox.Text = "";
            logBox.Text = "";
            // debug
            /*
            cBoxType.SelectedIndex = 5;
            searchBox.Text = "техническая жидкость";
            Button_Click(btnResp, EventArgs.Empty);
            */
        }

        private IRequest curRequest;
        private IResponse curResponse;

        private void Button_Click(object sender, EventArgs e)
        {
            WebProceed(cBoxType.Text, searchBox.Text, (sender as Button).Name == "btnResp");
        }

        private void AddLog(string inpStr, bool newStr=true, bool clear=false)
        {
            if (clear)
                logBox.Text = "";
            if (newStr)
                inpStr += Environment.NewLine;
            //logBox.Text += inpStr;            
            logBox.AppendText(inpStr);
        }
        
        private void WebProceed(string typeStr, string searchStr, bool needResponse)
        {
            
            if (searchStr == "")
            {
                AddLog("Введите поисковой запрос!");
                return;
            }

            bool error = true;

            AddLog("Создаем запрос к \"" + typeStr + "\"..", false);
            /*
            switch (typeStr)
            {

                //  00
                case RosElTorg:
                    curRequest = new RosElTorgRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new RosElTorgResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  01
                case ASVorgRU:
                    curRequest = new ASVorgRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new ASVorgResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  02
                case TorgiASV:                    
                    curRequest = new TorgASVRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new TorgASVResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }                        
                    break;
                //  03
                case CenterRu:                    
                    curRequest = new CenterrRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new CenterrResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");                        
                    }                        
                    break;
                //  04
                case SberbankAST:
                    curRequest = new SberbankAstRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new SberbankAstResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  05
                case B2B:
                    curRequest = new B2BRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new B2BResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  06
                case ETP_GPB:
                    curRequest = new GPBRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new GPBResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  07
                case UTender:
                    curRequest = new UTenderRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new UTenderResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  08
                case ZakupkiGov:
                    curRequest = new ZakupkiGovRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new ZakupkiGovResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  09
                case TekTorg:
                    curRequest = new TekTorgRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new TekTorgResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  10
                case RTSTender:
                    curRequest = new RTSTenderRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new RTSTenderResponse(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
            */
                //  11
                /*
                case LotOnline:
                    curRequest = new LotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                    */
            /*
                //  12
                case LotOnlineArrested:
                    curRequest = new ArrestedLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  13
                case LotOnlineConfiscate:
                    curRequest = new ConfiscateLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  14
                case LotOnlineFish:
                    curRequest = new FishLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  15
                case LotOnlineLease:
                    curRequest = new LeaseLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  16
                case LotOnlinePrivatization:
                    curRequest = new PrivatizationLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  17
                case LotOnlineRad:
                    curRequest = new RadLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  18
                case LotOnlineTrade:
                    curRequest = new TradeLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  19
                case LotOnlineZalog:
                    curRequest = new ZalogLotOnlineRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  20
                case LotOnlineGz:
                    curRequest = new LotOnlineGzRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineGzResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  21
                case LotOnlineSales:
                    curRequest = new LotOnlineSalesRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineSalesResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;
                //  22
                case LotOnlineTender:
                    curRequest = new LotOnlineTenderRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new LotOnlineTenderResponse(curRequest);
                        //curResponse = LotOnlineResponse.FactoryMethod(curRequest);
                        error = false;
                        AddLog("УСПЕШНО!");
                    }
                    break;                
                default:
                    error = true;
                    AddLog("ОШИБКА: Неизвестная площадка!");
                    return;
                    //break;
            }
            */

            if (!MyConst.ETP.ContainsKey(typeStr))
            {
                AddLog("НЕИЗВЕСТНАЯ ПЛОЩАДКА: " + typeStr);
                return;
            }

            System.Reflection.ConstructorInfo ci = MyConst.ETP[typeStr].RequestType.GetConstructor(new Type[] {  typeof(string) });
            curRequest = (IRequest)ci.Invoke(new object[] { searchBox.Text });
            error = false;
            AddLog("УСПЕШНО!");


            if (needResponse)
            {
                AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                ci = MyConst.ETP[typeStr].ResponseType.GetConstructor(new Type[] { typeof(IRequest) });
                curResponse = (IResponse)ci.Invoke(new object[] { curRequest });
                error = false;
                AddLog("УСПЕШНО!");

                if (curResponse == null)
                {
                    AddLog("Ответ Не получен!");
                    return;
                } else if (curResponse.ListResponse == null)
                {
                    AddLog("Ответ не корректен. Ошибка: " + curResponse.LastError().Message);
                    return;
                }

                AddLog("Получено результатов: " + curResponse.ListResponse.Count());                
                int c = 0;
                AddLog("----------");
                foreach (IObject item in curResponse.ListResponse)
                {
                    c++;                    
                    AddLog(c+". "+ item.LotNameStr);
                    AddLog("----------");
                }                
            }

            AddLog("Сохраняем ЗАПРОС в файл..", false);
            if (curRequest.SaveToXml((curRequest.SiteName + "_" + curRequest.SearchString).Replace(" ", "") + ".req"))
                AddLog("УСПЕШНО!");
            if (needResponse)
            {
                AddLog("Сохраняем ОТВЕТ в файл..", false);
                if (curResponse.SaveToXml((curRequest.SiteName + "_" + curRequest.SearchString).Replace(" ", "") + ".resp"))
                    AddLog("УСПЕШНО!");
            }

            if (error)
            {
                AddLog("ОШИБКА: Произошла неизвестная ошибка!");
                return;
            }
        }
    }
}
