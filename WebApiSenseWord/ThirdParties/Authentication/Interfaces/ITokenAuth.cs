using System;
using System.Collections.Generic;
using System.Text;
using ThirdParties.OperationContracts;

namespace ThirdParties.Authentication.Interfaces
{
    public interface ITokenAuth
    {
        string BaseAddress { get; set; }
        string AuthenticationUrl { get; set; }

        bool IsAuthenticated(RequestHeaderContract contract);
    }
}
