using System;

using Raven.Imports.Newtonsoft.Json;

namespace NoSqlKickoff.Model
{
    public class Employment
    {
        public string PlayerId { get; set; }

        public string TeamId { get; set; }
        
        public string Season { get; set; }

        public decimal Salary { get; set; }
    }
}