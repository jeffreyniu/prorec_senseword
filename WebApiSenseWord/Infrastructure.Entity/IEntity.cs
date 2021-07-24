using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Entity
{
    [ExcludeFromCodeCoverage]
    public class IEntity
    {
        public int ID { get; set; }
    }
}
