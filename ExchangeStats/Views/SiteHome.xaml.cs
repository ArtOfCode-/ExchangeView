using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ExchangeStats.Contracts;

namespace ExchangeStats.Views
{
    /// <summary>
    /// Interaction logic for SiteHome.xaml
    /// </summary>
    public partial class SiteHome : UserControl
    {
        public Site DisplaySite { get; private set; }

        public SiteHome(Site displaySite)
        {
            InitializeComponent();
            DisplaySite = displaySite;

            this.Loaded += SiteHome_Loaded;
        }

        private void SiteHome_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitializeSiteDetails();
            this.InitializeQuestions();
        }

        private void InitializeSiteDetails()
        {
            if (!string.IsNullOrEmpty(this.DisplaySite.IconUrl))
            {
                this.SiteLogo.Source = new BitmapImage(new Uri(this.DisplaySite.IconUrl));
            }
            else
            {
                this.SiteLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Static/DefaultIcon.png"));
            }

            this.SiteName.Content = WebUtility.HtmlDecode(this.DisplaySite.Name);
            this.SiteName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#242729"));
        }

        private void InitializeQuestions()
        {
            string questionsUri = StackApi.CreateRequestUri("questions", new Dictionary<string, string>
            {
                { "site", this.DisplaySite.ApiSiteParameter },
                { "pagesize", "50" }
            });
            QuestionResponse response = StackApi.FireRequest<QuestionResponse>(questionsUri);

            foreach (Question question in response.Questions)
            {
                this.QuestionsPanel.Children.Add(new QuestionPartial(question, this.DisplaySite));
            }
        }
    }
}
