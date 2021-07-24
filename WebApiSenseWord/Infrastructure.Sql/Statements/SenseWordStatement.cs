using Infrastructure.Entity;
using Infrastructure.Entity.Entities;
using Infrastructure.Sql.Statements.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Sql
{
    public class SenseWordStatement : ISenseWordStatement
    {
        private readonly IDatabaseContext<SenseWordEntity> _databaseContext;

        #region Constructor

        public SenseWordStatement(IDatabaseContext<SenseWordEntity> databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #endregion

        #region Methods

        public SenseWordEntity Get(int userId, string tableName, int id)
        {
            var paras = new List<MySqlParameter>();
            paras.Add(new MySqlParameter("@iUserId", userId));
            paras.Add(new MySqlParameter("@iTableName", tableName));
            paras.Add(new MySqlParameter("@iID", id));

            var entities = _databaseContext.ReadWithStoredProcedure("sp_get_sense_word", paras, GetSenseWordEntity);

            return entities.FirstOrDefault();
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

        #endregion
    }
}
