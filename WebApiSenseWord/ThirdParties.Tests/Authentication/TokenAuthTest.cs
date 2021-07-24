using CustomCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using ThirdParties.Authentication;
using ThirdParties.Authentication.Interfaces;
using ThirdParties.HttpHandlers.Interface;
using ThirdParties.OperationContracts;

namespace ThirdParties.Tests.Authentication
{
    [TestClass]
    public class TokenAuthTest
    {
        private Mock<IHttpPostHandler> _httpPostHandlerMock;

        private ITokenAuth _tokenAuth;

        [TestInitialize]
        public void Initialize()
        {
            var httpResponseTask = Task.Factory.StartNew<string>(() => { return "success"; });

            _httpPostHandlerMock = new Mock<IHttpPostHandler>();

            _httpPostHandlerMock.Setup(http => http.PostUserAuthSearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestHeaderContract>(), It.IsAny<UserAuthSearch>())).Returns(httpResponseTask);

            _tokenAuth = new TokenAuth(_httpPostHandlerMock.Object);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void TokenAuth_IsAuthenticated_given_token_when_executed_then_return_true()
        {
            // Arrange
            var requestHeaderContract = new RequestHeaderContract();

            // Act
            var isAuthenticated = _tokenAuth.IsAuthenticated(requestHeaderContract);

            // Assert
            Assert.AreEqual(true, isAuthenticated);
        }
    }
}
