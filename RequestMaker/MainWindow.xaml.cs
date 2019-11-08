using CenterrRu;
using IAuction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TorgiASV;

namespace RequestMaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyInitialize();
        }

        private void MyInitialize()
        {
            cBoxType.Items.Add("Торги АСВ");
            cBoxType.Items.Add("Центр Реализации");
            cBoxType.SelectedIndex = 0;
            searhBox.Text = "";
            logBox.Text = "";            
        }

        private IRequest curRequest;
        private IResponse curResponse;

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            if (searhBox.Text == "")
            {
                logBox.Text += "Введите поисковой запрос!";
                return;
            }

            bool needResponse = false;
            if (sender is Button)
                if ((sender as Button).Name == "btnResp")
                    needResponse = true;

            switch (cBoxType.SelectedItem)
            {
                case "Торги АСВ":
                    curRequest = new ASVRequest(searhBox.Text);
                    if (needResponse)
                        curResponse = new ASVResponse(curRequest);
                    break;
                case "Центр Реализации":
                    curRequest = new CenterrRequest(searhBox.Text);
                    if (needResponse)
                        curResponse = new CenterrResponse(curRequest);
                    break;
                default:
                    break;
            }

            curRequest.SaveToXml(curRequest.SiteName.Replace(" ", "") + ".req");
            if (needResponse)
                curResponse.SaveToXml(curResponse.SiteName.Replace(" ", "") + ".resp");
                
        }
    }
}
