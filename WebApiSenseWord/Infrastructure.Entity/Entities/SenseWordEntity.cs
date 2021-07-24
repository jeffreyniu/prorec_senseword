using System;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Entity.Entities
{
    [ExcludeFromCodeCoverage]
    public class SenseWordEntity : IEntity
    {
        public string Word { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
