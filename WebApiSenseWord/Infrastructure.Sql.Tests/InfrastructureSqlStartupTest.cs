using CustomCore;
using Infrastructure.Sql.Statements.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Sql.Tests
{
    [TestClass]
    public class InfrastructureSqlStartupTest
    {
        private ServiceCollection _serviceCollection = new ServiceCollection();

        [TestInitialize]
        public void Initialize()
        {
            InfrastructureSqlStartup.ConfigureServices(_serviceCollection);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void InfrastructureSqlStartup_IOC_SenseWordStatement_registered_as_ISenseWordStatement()
        {
            // Arrange
            var serviceEnumerator = _serviceCollection.GetEnumerator();

            // Act
            while (serviceEnumerator.MoveNext())
            {
                if (serviceEnumerator.Current.ServiceType == typeof(ISenseWordStatement))
                {
                    // Assert
                    Assert.AreEqual(typeof(SenseWordStatement), serviceEnumerator.Current.ImplementationType);
                    return;
                }
            }

            Assert.Fail($"Failed to find implementation for {nameof(ISenseWordStatement)}");
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void InfrastructureSqlStartup_IOC_DatabaseContext_registered_as_IDatabaseContext()
        {
            // Arrange
            var serviceEnumerator = _serviceCollection.GetEnumerator();

            // Act
            while (serviceEnumerator.MoveNext())
            {
                if (serviceEnumerator.Current.ServiceType == typeof(IDatabaseContext<>))
                {
                    // Assert
                    Assert.AreEqual(typeof(DatabaseContext<>), serviceEnumerator.Current.ImplementationType);
                    return;
                }
            }

            Assert.Fail($"Failed to find implementation for IDatabaseContext<>");
        }
    }
}
