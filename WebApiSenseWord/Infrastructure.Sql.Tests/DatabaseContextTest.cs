using CustomCore;
using Infrastructure.Entity.Entities;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Sql.Tests
{
    [TestClass]
    public class DatabaseContextTest
    {
        private SenseWordEntity _senseWordEntity;

        private IOptions<AppSettings> _appSettings;

        private IDatabaseContext<SenseWordEntity> _databaseContext;

        [TestInitialize]
        public void Initialize()
        {
            _senseWordEntity = new SenseWordEntity { ID = 1 };

            _appSettings = Options.Create<AppSettings>(new AppSettings
            {
                MySqlConnectionString = "server=127.0.0.1;port=3306;database=prorec_SenseWord;user=root;password=sql1234;"
            });

            _databaseContext = new DatabaseContext<SenseWordEntity>(_appSettings);
        }

        [TestMethod, TestCategory(TestCategories.ManualTest)]
        public void DatabaseContext_ReadWithStoredProcedure_given_valid_parameters_when_executed_then_return_valid_entities_system_test()
        {
            // Arrange
            var paras = new List<MySqlParameter>();
            paras.Add(new MySqlParameter("@iUserId", 1));
            paras.Add(new MySqlParameter("@iTableName", "tableName"));
            paras.Add(new MySqlParameter("@iID", 1));

            // Act
            var entities = _databaseContext.ReadWithStoredProcedure("sp_get_sense_word", paras, GetSenseWordEntity);

            // Assert
            Assert.IsNotNull(entities);
        }

        private SenseWordEntity GetSenseWordEntity(MySqlDataReader reader)
        {
            var senseWordEntity = new SenseWordEntity
            {
                ID = reader.GetInt32("ID"),
                Word = reader.GetString("Word"),
                Enabled = reader.GetBoolean("Enabled"),
                CreatedDate = reader.GetDateTime("CreatedDate"),
                LastUpdatedDate = reader.GetDateTime("LastUpdatedDate")
            };

            return senseWordEntity;
        }
    }
}
