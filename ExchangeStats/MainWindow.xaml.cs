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
using ExchangeStats.Views.Authentication;
using ExchangeStats.Views.Users;

namespace ExchangeStats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Site> _sites;

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

        public List<Site> GetSitesList()
        {
            return _sites;
        }

        public void SetSignedInUser(User user)
        {
            this.UserFrame.Children.Clear();
            this.UserFrame.Children.Add(new UserSidebar(user));
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
                this.ChangeContent(new ErrorReport("An error occurred while fetching site names.", App.GetExceptionMessage("LoadingSites"), App.GetExceptionTechDetails("LoadingSites")));
                return;
            }

            _sites = response.Sites.ToList<Site>();

            foreach (Site site in response.Sites.OrderBy(x => x.Name))
            {
                this.SitesPanel.Children.Add(this.CreateSiteDisplay(site));
            }

            if (App.SignedInUser != null)
            {
                this.UserFrame.Children.Add(new UserSidebar(App.SignedInUser));
            }
            else
            {
                App.AuthenticatingGuid = Guid.NewGuid();
                this.UserFrame.Children.Add(new AuthSidebar(string.Format("https://stackexchange.com/oauth/dialog?client_id={0}&scope={1}&redirect_uri={2}&state={3}",
                                                                          App.ClientId, "read_inbox,no_expiry,write_access,private_info",
                                                                          "https://auth.artofcode.co.uk/auth/redirect", App.AuthenticatingGuid)));
            }
        }

        private UIElement CreateSiteDisplay(Site site)
        {
            StackPanel basePanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(7),
                Cursor = Cursors.Hand
            };
            basePanel.MouseDown += SiteDisplay_MouseDown;

            Image logo = new Image
            {
                Source = new BitmapImage(new Uri(site.IconUrl)),
                Height = 28,
                Width = 28
            };

            Label name = new Label
            {
                Content = WebUtility.HtmlDecode(site.Name),
                Foreground = new SolidColorBrush(Color.FromRgb(168, 172, 175))
            };

            basePanel.Children.Add(logo);
            basePanel.Children.Add(name);
            return basePanel;
        }

        private void SiteSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            this.SitesPanel.Children.Clear();
            foreach (Site site in _sites.Where(x => WebUtility.HtmlDecode(x.Name).ToLower().Contains(this.SiteSearch.Text.ToLower())).OrderBy(x => x.Name))
            {
                this.SitesPanel.Children.Add(this.CreateSiteDisplay(site));
            }
        }

        private void SiteDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            Application.Current.MainWindow.Cursor = Cursors.Wait;
            StackPanel parentPanel = (StackPanel)sender;
            string siteName = (string)parentPanel.Children.OfType<Label>().First().Content;
            Site site = _sites.First(x => WebUtility.HtmlDecode(x.Name) == siteName);

            this.ChangeContent(new SiteBase(site, new SiteQuestions(site)));
            Application.Current.MainWindow.Cursor = Cursors.Arrow;
        }
    }
}
