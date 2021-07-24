using Infrastructure.Entity;
using Infrastructure.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Sql.Statements.Interfaces
{
    public interface ISenseWordStatement
    {
        SenseWordEntity Get(int userId, string tableName, int id);
    }
}
