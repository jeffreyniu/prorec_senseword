using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThirdParties.OperationContracts;

namespace ThirdParties.HttpHandlers.Interface
{
    public interface IHttpPostHandler
    {
        Task<string> PostUserAuthSearchAsync(string baseAddress, string url, RequestHeaderContract contract, UserAuthSearch body);
    }
}
