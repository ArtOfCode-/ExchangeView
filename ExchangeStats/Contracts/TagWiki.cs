using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class TagWiki
    {
        [DataMember(Name = "body", IsRequired = false)]
        public string Body { get; set; }

        [DataMember(Name = "body_last_edit_date", IsRequired = false)]
        public string BodyLastEditDate { get; set; }

        [DataMember(Name = "excerpt", IsRequired = false)]
        public string Excerpt { get; set; }

        [DataMember(Name = "excerpt_last_edit_date", IsRequired = false)]
        public string ExcerptLastEditDate { get; set; }

        [DataMember(Name = "last_body_editor", IsRequired = false)]
        public ShallowUser LastBodyEditor { get; set; }

        [DataMember(Name = "last_excerpt_editor", IsRequired = false)]
        public ShallowUser LastExcerptEditor { get; set; }

        [DataMember(Name = "tag_name")]
        public string TagName { get; set; }
    }
}
