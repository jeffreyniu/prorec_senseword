using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace ThirdParties.OperationContracts
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public class UserAuthSearch
    {
        [DataMember(Name = "userid")]
        public int UserId { get; set; }
    }
}
