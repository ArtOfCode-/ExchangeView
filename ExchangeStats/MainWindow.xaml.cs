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
using ExchangeStats.Views;

namespace ExchangeStats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Site> sites;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        public void ChangeContent(UserControl newContent)
        {
            this.ContentFrame.Children.Clear();
            this.ContentFrame.Children.Add(newContent);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ChangeContent(App.StartupView);

            string sitesUri = StackApi.CreateRequestUri("sites", new Dictionary<string, string>
            {
                { "pagesize", "999" }
            });
            SiteResponse response = StackApi.FireCacheableRequest<SiteResponse>(sitesUri, 3600 * 24);

            if (response == null)
            {
                // TODO: Handle that somehow... Error report.
                return;
            }

            sites = response.Sites.ToList<Site>();

            foreach (Site site in response.Sites.OrderBy(x => x.Name))
            {
                this.SitesPanel.Children.Add(this.CreateSiteDisplay(site));
            }
        }

        private UIElement CreateSiteDisplay(Site site)
        {
            StackPanel basePanel = new StackPanel();
            basePanel.Orientation = Orientation.Horizontal;
            basePanel.Margin = new Thickness(7);
            basePanel.MouseDown += SiteDisplay_MouseDown;

            Image logo = new Image();
            logo.Source = new BitmapImage(new Uri(site.IconUrl));
            logo.Height = 28;
            logo.Width = 28;

            Label name = new Label();
            name.Content = WebUtility.HtmlDecode(site.Name);
            name.Foreground = new SolidColorBrush(Color.FromRgb(168, 172, 175));

            basePanel.Children.Add(logo);
            basePanel.Children.Add(name);
            return basePanel;
        }

        private void SiteSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            this.SitesPanel.Children.Clear();
            foreach (Site site in sites.Where(x => WebUtility.HtmlDecode(x.Name).ToLower().Contains(this.SiteSearch.Text.ToLower())).OrderBy(x => x.Name))
            {
                this.SitesPanel.Children.Add(this.CreateSiteDisplay(site));
            }
        }

        private void SiteDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            StackPanel parentPanel = (StackPanel)sender;
            string siteName = (string)parentPanel.Children.OfType<Label>().First().Content;
            Site site = sites.Where(x => WebUtility.HtmlDecode(x.Name) == siteName).First();

            this.ChangeContent(new SiteHome(site));
        }
    }
}
