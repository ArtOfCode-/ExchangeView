using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class BadgeCount
    {
        [DataMember(Name = "bronze")]
        public int Bronze { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }

        [DataMember(Name = "silver")]
        public int Silver { get; set; }
    }
}
