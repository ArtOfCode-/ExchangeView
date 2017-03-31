using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "about_me", IsRequired = false)]
        public string AboutMe { get; set; }

        [DataMember(Name = "account_id")]
        public int AccountId { get; set; }

        [DataMember(Name = "badge_counts")]
        public BadgeCount BadgeCounts { get; set; }

        [DataMember(Name = "creation_date")]
        public string CreationDate { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "reputation")]
        public int Reputation { get; set; }

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "user_type")]
        public string UserType { get; set; }
    }
}
