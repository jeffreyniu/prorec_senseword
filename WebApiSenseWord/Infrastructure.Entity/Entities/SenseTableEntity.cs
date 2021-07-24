using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Entity.Entities
{
    [ExcludeFromCodeCoverage]
    public class SenseTableEntity : IEntity
    {
        public string UserId { get; set; }

        public string TableName { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
