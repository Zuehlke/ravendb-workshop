using System.Collections.Generic;

using NoSqlKickoff.Model.Exercises;

using Raven.Imports.Newtonsoft.Json;

namespace NoSqlKickoff.Model.Reference
{
    public class Player
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nationality Nationality { get; set; }

        public string TeamId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Team TeamNavigationProperty { get; set; }
    }
}
