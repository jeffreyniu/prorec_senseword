using Application.Adapter.SenseWord.Criteria;
using Application.Adapter.SenseWord.Interfaces;
using Infrastructure.Entity.Entities;
using Infrastructure.Sql.Statements.Interfaces;
using System;

namespace Application.Adapter.SenseWord
{
    public class SenseWordAdapter : ISenseWordAdapter
    {
        #region Fields

        private readonly ISenseWordStatement _senseWordStatement;
        #endregion

        #region Constructor

        public SenseWordAdapter(ISenseWordStatement senseWordStatement)
        {
            _senseWordStatement = senseWordStatement;
        }

        #endregion

        #region Methods

        public SenseWordEntity Get(GetCriterion criterion)
        {
            return _senseWordStatement.Get(criterion.UserId, criterion.TableName, criterion.WordId);
        }

        #endregion
    }
}
