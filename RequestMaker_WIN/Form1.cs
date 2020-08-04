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
            cBoxType.Items.Add("АСВ сайт");
            cBoxType.Items.Add("Торги АСВ");
            cBoxType.Items.Add("Центр Реализации");
            cBoxType.Items.Add("Сбербанк-АСТ");
            cBoxType.Items.Add("B2B центр");
            cBoxType.Items.Add("ЭТП ГПБ");
            cBoxType.Items.Add("Ю-Тендер");
            cBoxType.Items.Add("ГосЗакупки");
            cBoxType.SelectedIndex = 0;
            searchBox.Text = "";
            logBox.Text = "";
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
                case "АСВ сайт":
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
                case "Торги АСВ":                    
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
                case "Центр Реализации":                    
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
                case "Сбербанк-АСТ":
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
                case "B2B центр":
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
                case "ЭТП ГПБ":
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
                case "Ю-Тендер":
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
                case "ГосЗакупки":
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
            if (curRequest.SaveToXml(curRequest.SiteName.Replace(" ", "") + ".req"))
                AddLog("УСПЕШНО!");
            if (needResponse)
            {
                AddLog("Сохраняем ОТВЕТ в файл..", false);
                if (curResponse.SaveToXml(curResponse.SiteName.Replace(" ", "") + ".resp"))
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
