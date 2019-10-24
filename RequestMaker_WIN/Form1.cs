using CenterrRu;
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
            cBoxType.Items.Add("Торги АСВ");
            cBoxType.Items.Add("Центр Реализации");
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
                case "Торги АСВ":                    
                    curRequest = new ASVRequest(searchBox.Text);
                    error = false;
                    AddLog("УСПЕШНО!");
                    if (needResponse)
                    {
                        AddLog("Получаем ответ от \"" + typeStr + "\"..", false);
                        curResponse = new ASVResponse(curRequest);
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
