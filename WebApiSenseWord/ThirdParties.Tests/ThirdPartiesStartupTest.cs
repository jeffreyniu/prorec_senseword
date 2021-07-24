using CustomCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdParties.Authentication;
using ThirdParties.Authentication.Interfaces;
using ThirdParties.HttpHandlers;
using ThirdParties.HttpHandlers.Interface;

namespace ThirdParties.Tests
{
    [TestClass]
    public class ThirdPartiesStartupTest
    {
        private ServiceCollection _serviceCollection = new ServiceCollection();

        [TestInitialize]
        public void Initialize()
        {
            ThirdPartiesStartup.ConfigureServices(_serviceCollection);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void InfrastructureSqlStartup_IOC_TokenAuth_registered_as_ITokenAuth()
        {
            // Arrange
            var serviceEnumerator = _serviceCollection.GetEnumerator();

            // Act
            while (serviceEnumerator.MoveNext())
            {
                if (serviceEnumerator.Current.ServiceType == typeof(ITokenAuth))
                {
                    // Assert
                    Assert.AreEqual(typeof(TokenAuth), serviceEnumerator.Current.ImplementationType);
                    return;
                }
            }

            Assert.Fail($"Failed to find implementation for {nameof(ITokenAuth)}");
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void InfrastructureSqlStartup_IOC_HttpPostHandler_registered_as_IHttpPostHandler()
        {
            // Arrange
            var serviceEnumerator = _serviceCollection.GetEnumerator();

            // Act
            while (serviceEnumerator.MoveNext())
            {
                if (serviceEnumerator.Current.ServiceType == typeof(IHttpPostHandler))
                {
                    // Assert
                    Assert.AreEqual(typeof(HttpPostHandler), serviceEnumerator.Current.ImplementationType);
                    return;
                }
            }

            Assert.Fail($"Failed to find implementation for {nameof(IHttpPostHandler)}");
        }
    }
}
