using Infrastructure.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Sql
{
    public interface IDatabaseContext<TEntity>
    {
        IList<TEntity> ReadWithStoredProcedure(string spName, List<MySqlParameter> paras, Func<MySqlDataReader, TEntity> func);
    }
}
