using System.Collections.Generic;

namespace NoSqlKickoff.Model.Exercises
{
    public class Player
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nationality Nationality { get; set; }
    }
}