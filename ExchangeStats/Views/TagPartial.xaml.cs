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
    /// Interaction logic for TagPartial.xaml
    /// </summary>
    public partial class TagPartial : UserControl
    {
        public Tag DisplayTag { get; private set; }

        public TagWiki Wiki { get; private set; }

        public Site ParentSite { get; private set; }

        public TagPartial(Tag displayTag, TagWiki wiki, Site parentSite)
        {
            InitializeComponent();
            this.DisplayTag = displayTag;
            this.Wiki = wiki;
            this.ParentSite = parentSite;
            this.Loaded += TagPartial_Loaded;
        }

        private void TagPartial_Loaded(object sender, RoutedEventArgs e)
        {
            Label tagElement = this.TagElement;
            tagElement.Content = this.DisplayTag.Name;
            tagElement.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.ParentSite.Style.TagForegroundColor));
            tagElement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.ParentSite.Style.TagBackgroundColor));

            this.QuestionCount.Content = "x" + this.DisplayTag.QuestionCount.ToString();

            if (this.Wiki.Excerpt != null)
            {
                this.Description.Text = this.Wiki.Excerpt;
            }
            else
            {
                this.Description.Text = App.GetResourceObject("TagNoExcerpt").ToString();
            }
        }
    }
}
