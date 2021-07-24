using CustomCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThirdParties.OperationContracts;

namespace WebApiSenseWord.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IOptions<AppSettings> AppSettings;

        public BaseApiController(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings;
        }

        protected RequestHeaderContract GetRequestHeaderContract()
        {
            var requestHeaderContract = new RequestHeaderContract();

            if (HttpContext != null)
            {
                var headers = HttpContext.Request.Headers;

                StringValues values;
                if (headers.ContainsKey("userId") && headers.TryGetValue("userId", out values))
                {
                    int userId;
                    if (int.TryParse(values.First(), out userId))
                    {
                        requestHeaderContract.UserId = userId;
                    }
                }

                if (headers.ContainsKey("apiKey") && headers.TryGetValue("apiKey", out values))
                {
                    requestHeaderContract.ApiKey = values.First();
                }

                if (headers.ContainsKey("apiSecret") && headers.TryGetValue("apiSecret", out values))
                {
                    requestHeaderContract.ApiSecret = values.First();
                }
            }

            return requestHeaderContract;
        }
    }
}
