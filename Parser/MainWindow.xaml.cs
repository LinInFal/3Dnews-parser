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

        void PanoramaNews()
        {
            news_text = "";

            var category = new Dictionary<string, string>()
            {
                { "Политика", "politics"},
                { "Общество", "society"},
                { "Наука", "science"},
                { "Экономика", "economics"},
                { "Статьи", "articles"},
                { "Книги", "books"},
            };

            var html = $"https://panorama.pub/{category[categoryBox.Text]}"; //сосаити

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var news = htmlDoc.DocumentNode.SelectNodes("//h3");
            var links = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"entry big\"]");

            for (int n = 0; n < news.Count; n++)
            {
                news_text += $"{n + 1}. {news[n].InnerText} (https://panorama.pub{links[n].Attributes["href"].Value})\n";
            }
            newsBox.Text = news_text;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PanoramaNews();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("By OneEyedDancer\nBeerWare License", "О программе");
        }
    }
}
