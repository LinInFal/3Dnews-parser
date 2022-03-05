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
        int page = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        void PanoramaNews()
        {
            NewsBlock.Text = "";
            news_text = "";
            

            var html = $"https://www.opennet.ru/opennews/index.shtml?skip={page}";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var news = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"title2\"]");
            var links = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"s2\"]");

            for (int n = 0; n < news.Count; n++)
            {
                string link = $"https://www.opennet.ru{links[n].Attributes["href"].Value}";
                news_text = $"{n + 1}. {news[n].InnerText} ";
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
            PanoramaNews();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("By OneEyedDancer\nMIT License", "О программе");
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (page - 1 >= 0)
            {
                page--;
                PageLabel.Content = page;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            page++;
            PageLabel.Content = page;
        }
    }
}
