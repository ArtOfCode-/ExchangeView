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

using ExchangeStats.Contracts;

namespace ExchangeStats.Views
{
    /// <summary>
    /// Interaction logic for SiteQuestions.xaml
    /// </summary>
    public partial class SiteQuestions : UserControl
    {
        public Site DisplaySite { get; private set; }

        public SiteQuestions(Site displaySite)
        {
            InitializeComponent();
            this.DisplaySite = displaySite;

            this.Loaded += SiteQuestions_Loaded;
        }

        private void SiteQuestions_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitializeQuestions();
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
