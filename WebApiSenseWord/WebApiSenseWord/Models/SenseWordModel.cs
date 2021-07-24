using System;
using System.Diagnostics.CodeAnalysis;

namespace WebApiSenseWord.Models
{
    [ExcludeFromCodeCoverage]
    public class SenseWordModel
    {
        public int ID { get; set; }

        public string Word { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
