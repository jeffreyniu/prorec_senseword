using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThirdParties.HttpHandlers.Interface;
using ThirdParties.OperationContracts;

namespace ThirdParties.HttpHandlers
{
    public class HttpPostHandler : IHttpPostHandler
    {
        public async Task<string> PostUserAuthSearchAsync(string baseAddress, string url, RequestHeaderContract contract, UserAuthSearch body)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseAddress);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("userId", contract.UserId.ToString());
                request.Headers.Add("apiKey", contract.ApiKey);
                request.Headers.Add("apiSecret", contract.ApiSecret);

                request.Content = new ObjectContent<UserAuthSearch>(body, new JsonMediaTypeFormatter());

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
