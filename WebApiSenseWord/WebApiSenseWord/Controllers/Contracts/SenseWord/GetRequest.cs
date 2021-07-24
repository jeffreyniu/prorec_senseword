using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSenseWord.Controllers.Contracts.SenseWord
{
    [ExcludeFromCodeCoverage]
    public class GetRequest
    {
        public int UserId { get; set; }

        public string TableName { get; set; }

        public int WordId { get; set; }
    }
}
