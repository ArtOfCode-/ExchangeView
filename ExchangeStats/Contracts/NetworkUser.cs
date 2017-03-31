using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class NetworkUser
    {
        [DataMember(Name = "account_id")]
        public int AccountId { get; set; }

        [DataMember(Name = "answer_count")]
        public int AnswerCount { get; set; }

        [DataMember(Name = "badge_counts")]
        public BadgeCount BadgeCounts { get; set; }

        [DataMember(Name = "creation_date")]
        public string CreationDate { get; set; }

        [DataMember(Name = "last_access_date")]
        public string LastAccessDate { get; set; }

        [DataMember(Name = "question_count")]
        public int QuestionCount { get; set; }

        [DataMember(Name = "reputation")]
        public int Reputation { get; set; }

        [DataMember(Name = "site_name")]
        public string SiteName { get; set; }

        [DataMember(Name = "site_url")]
        public string SiteUrl { get; set; }
        
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }
    }
}
