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

namespace RequestMaker_WIN
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            MyInitialize();
        }

        private void MyInitialize()
        {
            foreach (KeyValuePair<string, ETPStruct> item in MyConst.ETP)
            {
                chListBox.Items.Add(item.Key);
            }
        }

        private IRequest curRequest;
        private IResponse curResponse;

        private void AddLog(string inpStr, bool newStr = true, bool clear = false)
        {
            if (clear)
                logBox.Text = "";
            if (newStr)
                inpStr += Environment.NewLine;
            //logBox.Text += inpStr;            
            logBox.AppendText(inpStr);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            foreach (var item in chListBox.CheckedItems)
            {
                WebProceed(item.ToString(), searchBox.Text, (sender as Button).Name == "btnResp");
            }
         
            
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

            if (!MyConst.ETP.ContainsKey(typeStr))
            {
                AddLog("НЕИЗВЕСТНАЯ ПЛОЩАДКА: " + typeStr);
                return;
            }

            System.Reflection.ConstructorInfo ci = MyConst.ETP[typeStr].RequestType.GetConstructor(new Type[] { typeof(string) });
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
                }
                else if (curResponse.ListResponse == null)
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
                    AddLog(c + ". " + item.LotNameStr);
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
