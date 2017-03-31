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
    /// Interaction logic for TagQuestions.xaml
    /// </summary>
    public partial class TagQuestions : UserControl
    {
        public Tag DisplayTag { get; private set; }
        public Site ParentSite { get; private set; }

        private int currentPage = 1;

        public TagQuestions(Tag displayTag, Site parentSite)
        {
            this.DisplayTag = displayTag;
            this.ParentSite = parentSite;
            InitializeComponent();
            this.InitializeQuestions(this.currentPage.ToString());

            this.TagDetails.Content = "Questions tagged " + this.DisplayTag.Name;
        }

        private void InitializeQuestions(string page)
        {
            string uri = StackApi.CreateRequestUri("questions", new Dictionary<string, string>
            {
                { "pagesize", "50" },
                { "page", page },
                { "tagged", this.DisplayTag.Name },
                { "site", this.ParentSite.ApiSiteParameter }
            });
            QuestionResponse response = StackApi.FireRequest<QuestionResponse>(uri);

            foreach (Question question in response.Questions)
            {
                this.QuestionsPanel.Children.Add(new QuestionPartial(question, this.ParentSite));
            }
        }
    }
}
