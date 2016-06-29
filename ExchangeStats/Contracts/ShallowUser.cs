using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class ShallowUser
    {
        [DataMember(Name = "display_name", IsRequired = false)]
        public string DisplayName { get; set; }

        [DataMember(Name = "link", IsRequired = false)]
        public string Link { get; set; }

        [DataMember(Name = "profile_image", IsRequired = false)]
        public string ProfileImageUri { get; set; }

        [DataMember(Name = "reputation", IsRequired = false)]
        public int Reputation { get; set; }

        [DataMember(Name = "user_id", IsRequired = false)]
        public int UserId { get; set; }

        [DataMember(Name = "user_type")]
        public string UserType { get; set; }
    }
}
