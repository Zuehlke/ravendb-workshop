using System;

namespace NoSqlKickoff.Model
{
    public class PlayerWithTeam
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Nationality Nationality { get; set; }

        public Team Team { get; set; }
    }
}
