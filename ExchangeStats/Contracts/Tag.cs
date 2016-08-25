using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class Tag
    {
        [DataMember(Name = "count")]
        public int QuestionCount { get; set; }

        [DataMember(Name = "has_synonyms")]
        public bool HasSynonyms { get; set; }

        [DataMember(Name = "is_moderator_only")]
        public bool IsModeratorOnly { get; set; }

        [DataMember(Name = "is_required")]
        public bool IsRequired { get; set; }

        [DataMember(Name = "last_activity_date", IsRequired = false)]
        public string LastActivityDate { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "synonyms")]
        public string[] Synonyms { get; set; }

        [DataMember(Name = "user_id", IsRequired = false)]
        public int UserId { get; set; }
    }
}
