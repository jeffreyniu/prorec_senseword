using CustomCore;
using Infrastructure.Entity.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiSenseWord.Mappers;
using WebApiSenseWord.Mappers.Interfaces;

namespace WebApiSenseWord.Tests.Mappers
{
    [TestClass]
    public class SenseWordMapperTest
    {
        private SenseWordEntity _senseWordEntity;
        private ISenseWordMapper _senseWordMapper;

        [TestInitialize]
        public void Initialize()
        {
            _senseWordEntity = new SenseWordEntity
            {
                ID = 1,
                Word = "word",
                Enabled = true
            };

            _senseWordMapper = new SenseWordMapper();
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordMapper_Map_given_entity_when_executed_then_return_valid_model()
        {
            // Arrange

            // Act
            var model = _senseWordMapper.Map(_senseWordEntity);

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.ID);
            Assert.AreEqual("word", model.Word);
        }
    }
}
