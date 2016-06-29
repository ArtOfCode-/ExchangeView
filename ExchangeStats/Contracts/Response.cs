using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "has_more")]
        public bool HasMore { get; set; }

        [DataMember(Name = "quota_max")]
        public int QuotaMax { get; set; }

        [DataMember(Name = "quota_remaining")]
        public int QuotaRemaining { get; set; }
    }

    [DataContract]
    public class QuestionResponse : Response
    {
        [DataMember(Name = "items")]
        public Question[] Questions { get; set; }
    }

    [DataContract]
    public class SiteResponse : Response
    {
        [DataMember(Name = "items")]
        public Site[] Sites { get; set; }
    }
}
