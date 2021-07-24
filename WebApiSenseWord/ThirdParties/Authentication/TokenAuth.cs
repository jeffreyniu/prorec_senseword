using System;
using System.Text.Json;
using ThirdParties.Authentication.Interfaces;
using ThirdParties.HttpHandlers.Interface;
using ThirdParties.OperationContracts;

namespace ThirdParties.Authentication
{
    public class TokenAuth : ITokenAuth
    {
        private readonly IHttpPostHandler _httpPostHandler;
        public TokenAuth(IHttpPostHandler httpPostHandler)
        {
            _httpPostHandler = httpPostHandler;
        }

        public string BaseAddress { get; set; }
        public string AuthenticationUrl { get; set; }

        public bool IsAuthenticated(RequestHeaderContract contract)
        {
            try
            {
                var userAuthSearch = new UserAuthSearch { UserId = contract.UserId };
                var response = _httpPostHandler.PostUserAuthSearchAsync(BaseAddress, AuthenticationUrl, contract, userAuthSearch);
                response.Wait();

                if (response != null && !string.IsNullOrEmpty(response.Result))
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new UnauthorizedAccessException("Token is invalid.", ex);
            }

            return false;
        }
    }
}
