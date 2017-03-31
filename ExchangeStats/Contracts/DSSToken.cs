using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeStats.Contracts
{
    [DataContract]
    public class DSSToken
    {
        [DataMember(Name = "access_token", IsRequired = false)]
        public string AccessToken { get; set; }

        [DataMember(Name = "error_name", IsRequired = false)]
        public string ErrorName { get; set; }

        [DataMember(Name = "error_message", IsRequired = false)]
        public string ErrorMessage { get; set; }
    }
}
