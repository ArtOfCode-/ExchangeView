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
    public partial class SiteBase : UserControl
    {
        public Site DisplaySite { get; private set; }

        private Dictionary<string, UserControl> navigationActions;

        private UserControl initialView;

        public SiteBase(Site displaySite, UserControl initialView)
        {
            InitializeComponent();
            this.DisplaySite = displaySite;
            this.initialView = initialView;

            this.Loaded += SiteHome_Loaded;
        }

        public void ChangeSubContent(UserControl newContent)
        {
            this.SubContent.Children.Clear();
            this.SubContent.Children.Add(newContent);
        }

        private void SiteHome_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitializeSiteDetails();

            this.navigationActions = new Dictionary<string, UserControl>
            {
                { "NavQuestions", new SiteQuestions(this.DisplaySite) },
                { "NavTags", new SiteTags(this.DisplaySite) },
                { "NavUsers", new SiteUsers(this.DisplaySite) }
            };

            this.ChangeSubContent(this.initialView);
        }

        private void InitializeSiteDetails()
        {
            if (!string.IsNullOrEmpty(this.DisplaySite.IconUrl))
            {
                this.SiteLogo.Source = new BitmapImage(new Uri(this.DisplaySite.IconUrl));
            }
            else
            {
                this.SiteLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Static/BetaBlank.png"));
            }

            this.SiteName.Content = WebUtility.HtmlDecode(this.DisplaySite.Name);
            this.SiteName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#242729"));

            foreach (Label elem in this.SitePages.Children)
            {
                elem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.DisplaySite.Style.LinkColor));
            }
        }

        private void Navigation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement source = (FrameworkElement)sender;
            if (this.navigationActions.ContainsKey(source.Name))
            {
                this.ChangeSubContent(this.navigationActions[source.Name]);
            }
            else
            {
                ErrorReport report = new ErrorReport("Action not recognized.", App.GetExceptionMessage("NavActionNotRecognized"), App.GetExceptionTechDetails("NavActionNotRecognized"));
                ((MainWindow)App.Current.MainWindow).ChangeContent(report);
            }
        }
    }
}
