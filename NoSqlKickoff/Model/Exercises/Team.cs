using System.Collections.Generic;

namespace NoSqlKickoff.Model.Exercises
{
    public class Team
    {
        public Team()
        {
            EmploymentCopies = new List<EmploymentCopyInTeam>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public List<EmploymentCopyInTeam> EmploymentCopies { get; set; } 
    }
}
