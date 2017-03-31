using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class RelatedSite
    {
        [DataMember(Name = "api_site_parameter", IsRequired = false)]
        public string ApiSiteParameter { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "relation")]
        public string Relation { get; set; }

        [DataMember(Name = "site_url")]
        public string SiteUrl { get; set; }
    }
}
