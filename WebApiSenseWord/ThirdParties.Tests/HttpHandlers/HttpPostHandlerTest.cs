using CustomCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ThirdParties.HttpHandlers;
using ThirdParties.HttpHandlers.Interface;
using ThirdParties.OperationContracts;

namespace ThirdParties.Tests.HttpHandlers
{
    [TestClass]
    public class HttpPostHandlerTest
    {
        private IHttpPostHandler _httpPostHandler;

        [TestInitialize]
        public void Initialize()
        {
            _httpPostHandler = new HttpPostHandler();
        }

        [TestMethod, TestCategory(TestCategories.ManualTest)]
        public void HttpPostHandler_PostAsync_given_valid_header_and_body_when_executed_then_return_valid_response_system_test()
        {
            // Arrange
            var baseAddress = "http://go.iniutoolbox.cn";
            var authUrl = "api/getauth";

            var requestHeaderContract = new RequestHeaderContract
            {
                UserId = 2,
                ApiKey = "keyc799519845606dcf0c04e92cf019bd80",
                ApiSecret = "secretf5737bbc11fe25f6b48c34dd1f48f422"
            };

            var userAuthSearch = new UserAuthSearch
            {
                UserId = 2
            };

            // Act
            var response = _httpPostHandler.PostUserAuthSearchAsync(baseAddress, authUrl, requestHeaderContract, userAuthSearch);
            response.Wait();
            // Assert
            Assert.IsNotNull(response);
        }
    }
}
