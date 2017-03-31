using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class Styling
    {
        [DataMember(Name = "link_color")]
        public string LinkColor { get; set; }

        [DataMember(Name = "tag_background_color")]
        public string TagBackgroundColor { get; set; }

        [DataMember(Name = "tag_foreground_color")]
        public string TagForegroundColor { get; set; }
    }
}
