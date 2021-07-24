using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Entity.Entities;
using Application.Adapter.SenseWord;
using Application.Adapter.SenseWord.Interfaces;
using Infrastructure.Sql.Statements.Interfaces;
using Moq;
using Application.Adapter.SenseWord.Criteria;
using CustomCore;

namespace Application.Adapter.Tests.SenseWord
{
    [TestClass]
    public class SenseWordAdapterTest
    {
        private SenseWordEntity _senseWordEntity;
        private Mock<ISenseWordStatement> _senseWordStatementMock;
        
        private ISenseWordAdapter _senseWordAdapter;

        [TestInitialize]
        public void Initialize()
        {
            _senseWordEntity = new SenseWordEntity { ID = 1 };

            _senseWordStatementMock = new Mock<ISenseWordStatement>();
            _senseWordStatementMock.Setup(s => s.Get(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).Returns(_senseWordEntity);

            _senseWordAdapter = new SenseWordAdapter(_senseWordStatementMock.Object);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordAdapter_Get_given_id_when_executed_then_return_valid_entity()
        {
            // Arrange
            var getCriterion = new GetCriterion
            {
                UserId = 1,
                TableName = "tableName",
                WordId = 1
            };

            // Act
            var entity = _senseWordAdapter.Get(getCriterion);

            // Assert
            Assert.IsNotNull(entity);
            Assert.AreEqual(1, entity.ID);
        }
    }
}
