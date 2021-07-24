using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Application.Adapter.SenseWord.Criteria
{
    [ExcludeFromCodeCoverage]
    public class GetCriterion
    {
        public int UserId { get; set; }

        public string TableName { get; set; }

        public int WordId { get; set; }
    }
}
