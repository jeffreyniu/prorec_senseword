using Application.Adapter.SenseWord;
using Application.Adapter.SenseWord.Interfaces;
using CustomCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapter.Tests
{
    [TestClass]
    public class ApplicationAdapterStartupTest
    {
        private ServiceCollection _serviceCollection = new ServiceCollection();

        [TestInitialize]
        public void Initialize()
        {
            ApplicationAdapterStartup.ConfigureServices(_serviceCollection);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void ApplicationAdapterStartup_IOC_SenseWordAdapter_registered_as_ISenseWordAdapter()
        {
            // Arrange
            var serviceEnumerator = _serviceCollection.GetEnumerator();

            // Act
            while (serviceEnumerator.MoveNext())
            {
                if (serviceEnumerator.Current.ServiceType == typeof(ISenseWordAdapter))
                {
                    // Assert
                    Assert.AreEqual(typeof(SenseWordAdapter), serviceEnumerator.Current.ImplementationType);
                    return;
                }
            }

            Assert.Fail($"Failed to find implementation for {nameof(ISenseWordAdapter)}");
        }
    }
}
