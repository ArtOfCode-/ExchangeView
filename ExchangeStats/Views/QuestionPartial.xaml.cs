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
    /// Interaction logic for QuestionPartial.xaml
    /// </summary>
    public partial class QuestionPartial : UserControl
    {
        public Question DisplayQuestion { get; private set; }

        public QuestionPartial(Question question, Site parentSite)
        {
            InitializeComponent();
            this.DisplayQuestion = question;
            this.AddDisplayText(parentSite);
        }

        private void AddDisplayText(Site parentSite)
        {
            this.Score.Content = this.DisplayQuestion.Score;
            this.Views.Content = NumberFormatter.FormatNumber(this.DisplayQuestion.ViewCount);
            this.Title.Text = WebUtility.HtmlDecode(this.DisplayQuestion.Title);
            this.Title.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(parentSite.Style.LinkColor));

            foreach (string tag in this.DisplayQuestion.Tags)
            {
                Label tagElement = new Label();
                tagElement.Content = tag;
                tagElement.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(parentSite.Style.TagForegroundColor));
                tagElement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(parentSite.Style.TagBackgroundColor));
                tagElement.Padding = new Thickness(5);
                tagElement.Margin = new Thickness(5, 0, 5, 0);
                tagElement.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tagElement.BorderThickness = new Thickness(1);
                this.Tags.Children.Add(tagElement);
            }
        }
    }
}
