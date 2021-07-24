using CustomCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiSenseWord.Mappers;
using WebApiSenseWord.Mappers.Interfaces;

namespace WebApiSenseWord.Tests
{
    [TestClass]
    public class WebApiSenseWordStartupTest
    {
        private ServiceCollection _serviceCollection = new ServiceCollection();

        [TestInitialize]
        public void Initialize()
        {
            WebApiSenseWordStartup.ConfigureServices(_serviceCollection);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void WebApiSenseWordStartup_IOC_SenseWordMapper_registered_as_ISenseWordMapper()
        {
            // Arrange
            var serviceEnumerator = _serviceCollection.GetEnumerator();

            // Act
            while (serviceEnumerator.MoveNext())
            {
                if (serviceEnumerator.Current.ServiceType == typeof(ISenseWordMapper))
                {
                    // Assert
                    Assert.AreEqual(typeof(SenseWordMapper), serviceEnumerator.Current.ImplementationType);
                    return;
                }
            }

            Assert.Fail($"Failed to find implementation for {nameof(ISenseWordMapper)}");
        }
    }
}
