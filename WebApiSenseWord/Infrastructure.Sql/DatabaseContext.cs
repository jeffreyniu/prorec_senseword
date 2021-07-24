using CustomCore;
using Infrastructure.Entity;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Sql
{
    public class DatabaseContext<TEntity> : IDatabaseContext<TEntity>
    {
        private readonly IOptions<AppSettings> _appSettings;

        public DatabaseContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_appSettings.Value.MySqlConnectionString);
        }

        public IList<TEntity> ReadWithStoredProcedure(string spName, List<MySqlParameter> paras, Func<MySqlDataReader, TEntity> func)
        {
            var entities = new List<TEntity>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(spName, conn);

                cmd.Parameters.AddRange(paras.ToArray());
                cmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(func(reader));
                    }
                }
            }

            return entities;
        }
    }
}
