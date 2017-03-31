using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class Question
    {
        [DataMember(Name = "answer_count")]
        public int AnswerCount { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "bounty_amount", IsRequired = false)]
        public int BountyAmount { get; set; }

        [DataMember(Name = "bounty_closes_date", IsRequired = false)]
        public string BountyClosesDate { get; set; }

        [DataMember(Name = "bounty_user", IsRequired = false)]
        public ShallowUser BountyUser { get; set; }

        [DataMember(Name = "close_vote_count")]
        public int CloseVoteCount { get; set; }

        [DataMember(Name = "closed_date", IsRequired = false)]
        public string ClosedDate { get; set; }

        [DataMember(Name = "closed_reason", IsRequired = false)]
        public string ClosedReason { get; set; }

        [DataMember(Name = "community_owned_date", IsRequired = false)]
        public string CommunityOwnedDate { get; set; }

        [DataMember(Name = "creation_date")]
        public string CreationDate { get; set; }

        [DataMember(Name = "is_answered")]
        public bool IsAnswered { get; set; }

        [DataMember(Name = "last_activity_date")]
        public string LastActivityDate { get; set; }

        [DataMember(Name = "last_edit_date", IsRequired = false)]
        public string LastEditDate { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "locked_date", IsRequired = false)]
        public string LockedDate { get; set; }

        [DataMember(Name = "owner", IsRequired = false)]
        public ShallowUser Owner { get; set; }

        [DataMember(Name = "protected_date", IsRequired = false)]
        public string ProtectedDate { get; set; }

        [DataMember(Name = "question_id")]
        public int QuestionId { get; set; }

        [DataMember(Name = "score")]
        public int Score { get; set; }

        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "view_count")]
        public int ViewCount { get; set; }
    }
}
