using System;

namespace NoSqlKickoff.Model
{
    public class PlayerWithFullName
    {
        public string FullName { get; set; }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Nationality Nationality { get; set; }

        public string TeamId { get; set; }
    }
}
