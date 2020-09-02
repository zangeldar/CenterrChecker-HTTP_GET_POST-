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

namespace RequestMaker_WIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyInitialize();
        }

        const string ASVorgRU       =   "АСВ сайт";                         //  0
        const string B2B            =   "B2B центр";                        //  1
        const string CenterRu       =   "Центр Реализации";                 //  2
        const string ETP_GPB        =   "ЭТП ГПБ";                          //  3
        const string RosElTorg      =   "РосЭлТорг";                        //  4
        const string RTSTender      =   "РТС-тендер";                       //  5
        const string SberbankAST    =   "Сбербанк-АСТ";                     //  6
        const string TekTorg        =   "ТЭК-Торг *";                       //  7
        const string TorgiASV       =   "Торги АСВ";                        //  8
        const string UTender        =   "Ю-Тендер";                         //  9
        const string ZakupkiGov     =   "ГосЗакупки";                       //  10
        const string LotOnline      =   "Лот-Онлайн";                       //  11

        private void MyInitialize()
        {
            cBoxType.Items.Add(ASVorgRU);
            cBoxType.Items.Add(B2B);
            cBoxType.Items.Add(CenterRu);
            cBoxType.Items.Add(ETP_GPB);
            cBoxType.Items.Add(RosElTorg);
            cBoxType.Items.Add(RTSTender);
            cBoxType.Items.Add(SberbankAST);
            cBoxType.Items.Add(TekTorg);
            cBoxType.Items.Add(TorgiASV);
            cBoxType.Items.Add(UTender);
            cBoxType.Items.Add(ZakupkiGov);
            cBoxType.Items.Add(LotOnline);
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
            logBox.Text += inpStr;
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
            switch (typeStr)
            {
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
                default:
                    error = true;
                    AddLog("ОШИБКА: Неизвестная площадка!");
                    return;
                    break;
            }
            
            if (needResponse)
            {
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
