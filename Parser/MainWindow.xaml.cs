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
using HtmlAgilityPack;
using System.Diagnostics;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string news_text;

        public MainWindow()
        {
            InitializeComponent();
        }

        void News()
        {
            NewsBlock.Text = "";
            news_text = "";
            

            var html = $"https://3dnews.ru";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var news = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"allnews\"]/div[2]/div/ul/li/a");
            var links = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"allnews\"]/div[2]/div/ul/li/a");

            for (int n = 0; n < news.Count; n++)
            {
                string link = $"https://3dnews.ru{links[n].Attributes["href"].Value}";
                news_text = $"{news[n].InnerText}\n";
                Hyperlink hyperLink = new Hyperlink()
                {
                    NavigateUri = new Uri(link)
                };
                hyperLink.Inlines.Add($"{news_text}\n");
                hyperLink.RequestNavigate += Hyperlink_RequestNavigate;
                NewsBlock.Inlines.Add(hyperLink);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            News();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
