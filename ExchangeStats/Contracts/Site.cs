using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class Site
    {
        [DataMember(Name = "aliases", IsRequired = false)]
        public string[] Aliases { get; set; }

        [DataMember(Name = "api_site_parameter")]
        public string ApiSiteParameter { get; set; }

        [DataMember(Name = "audience")]
        public string Audience { get; set; }

        [DataMember(Name = "closed_beta_date", IsRequired = false)]
        public string ClosedBetaDate { get; set; }

        [DataMember(Name = "favicon_url")]
        public string FaviconUrl { get; set; }

        [DataMember(Name = "high_resolution_icon_url", IsRequired = false)]
        public string HighResIconUrl { get; set; }

        [DataMember(Name = "icon_url")]
        public string IconUrl { get; set; }

        [DataMember(Name = "launch_date")]
        public string LaunchDate { get; set; }

        [DataMember(Name = "logo_url")]
        public string LogoUrl { get; set; }

        [DataMember(Name = "markdown_extensions", IsRequired = false)]
        public string[] MarkdownExtensions { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "open_beta_date", IsRequired = false)]
        public string OpenBetaDate { get; set; }

        [DataMember(Name = "related_sites", IsRequired = false)]
        public RelatedSite[] RelatedSites { get; set; }

        [DataMember(Name = "site_state")]
        public string SiteState { get; set; }

        [DataMember(Name = "site_type")]
        public string SiteType { get; set; }

        [DataMember(Name = "site_url")]
        public string SiteUrl { get; set; }

        [DataMember(Name = "styling")]
        public Styling Style { get; set; }

        [DataMember(Name = "twitter_account", IsRequired = false)]
        public string TwitterAccount { get; set; }
    }
}
