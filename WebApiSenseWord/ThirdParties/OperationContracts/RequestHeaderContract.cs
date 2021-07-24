using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdParties.OperationContracts
{
    [ExcludeFromCodeCoverage]
    public class RequestHeaderContract
    {
        public int UserId { get; set; }

        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }
    }
}
