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
    /// Interaction logic for SiteTags.xaml
    /// </summary>
    public partial class SiteTags : UserControl
    {
        public Site DisplaySite { get; private set; }

        private SiteBase parentFrame;
        private int currentPage = 1;

        public SiteTags(Site displaySite, SiteBase frame)
        {
            InitializeComponent();
            this.DisplaySite = displaySite;
            this.parentFrame = frame;
            this.InitializeTagList(this.currentPage.ToString());
        }

        private void InitializeTagList(string page)
        {
            string tagUri = StackApi.CreateRequestUri("tags", new Dictionary<string, string>
            {
                { "site", this.DisplaySite.ApiSiteParameter },
                { "pagesize", "20" },
                { "page", page }
            });
            TagResponse response = StackApi.FireCacheableRequest<TagResponse>(tagUri, 1800);

            string[] tagNames = response.Tags.Select(x => x.Name).ToArray();
            string apiTagString = string.Join(";", tagNames);
            string wikiUri = StackApi.CreateRequestUri("tags/{apiTagString}/wikis", new Dictionary<string, string>
            {
                { "site", this.DisplaySite.ApiSiteParameter },
                { "apiTagString", apiTagString }
            });
            TagWikiResponse wikiResponse = StackApi.FireCacheableRequest<TagWikiResponse>(wikiUri, 86400);

            foreach(Tag tag in response.Tags)
            {
                TagWiki wiki;
                try
                {
                    wiki = wikiResponse.TagWikis.First(x => x.TagName == tag.Name);
                }
                catch (InvalidOperationException)
                {
                    wiki = new TagWiki();
                }
                UserControl tagElement = new TagPartial(tag, wiki, this.DisplaySite, this.parentFrame);
                this.TagsPanel.Children.Add(tagElement);
            }
        }

        private void LoadMore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.currentPage += 1;
            this.InitializeTagList(this.currentPage.ToString());
        }
    }
}
