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

        public SiteTags(Site displaySite)
        {
            InitializeComponent();
            this.DisplaySite = displaySite;
            this.InitializeTagList();
        }

        private void InitializeTagList()
        {
            string tagUri = StackApi.CreateRequestUri("tags", new Dictionary<string, string>
            {
                { "site", this.DisplaySite.ApiSiteParameter },
                { "pagesize", "20" }
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
                TagWiki wiki = wikiResponse.TagWikis.Where(x => x.TagName == tag.Name).First();
                UserControl tagElement = new TagPartial(tag, wiki, this.DisplaySite);
                this.TagsPanel.Children.Add(tagElement);
            }
        }
    }
}
