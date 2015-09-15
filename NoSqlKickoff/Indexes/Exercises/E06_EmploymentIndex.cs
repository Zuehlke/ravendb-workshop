using System.Linq;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E06_EmploymentIndex : AbstractIndexCreationTask<Employment>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }
            public string Season { get; set; }
        }
        
        public E06_EmploymentIndex()
        {
            // TODO: Create Map property for Employment Index
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 select new IndexEntry
                                            {
                                                Season = employment.Season,
                                                TeamName = team.Name
                                            };
        }
    }
}
