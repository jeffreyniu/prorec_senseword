using CustomCore;
using Infrastructure.Entity;
using Infrastructure.Entity.Entities;
using Infrastructure.Sql.Statements.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Infrastructure.Sql.Tests.Statements
{
    [TestClass]
    public class SenseWordStatementTest
    {
        private SenseWordEntity _senseWordEntity;

        private Mock<IDatabaseContext<SenseWordEntity>> _databaseContextMock;

        private ISenseWordStatement _senseWordStatement;

        [TestInitialize]
        public void Initialize()
        {
            _senseWordEntity = new SenseWordEntity { ID = 1 };

            _databaseContextMock = new Mock<IDatabaseContext<SenseWordEntity>>();

            _databaseContextMock.Setup(d => d.ReadWithStoredProcedure(It.IsAny<string>(), It.IsAny<List<MySqlParameter>>(), It.IsAny<Func<MySqlDataReader, SenseWordEntity>>()))
                .Returns(new List<SenseWordEntity> { _senseWordEntity });

            _senseWordStatement = new SenseWordStatement(_databaseContextMock.Object);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordStatement_Get_given_id_when_executed_then_return_valid_entity()
        {
            // Arrange

            // Act
            var entity = _senseWordStatement.Get(1, "tableName", 1);

            // Assert
            Assert.IsNotNull(entity);
            Assert.AreEqual(1, entity.ID);
        }
    }
}
