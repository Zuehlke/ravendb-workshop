using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E06_EmploymentIndex2 : AbstractIndexCreationTask<Employment, E06_EmploymentIndex2.IndexEntry>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }
            public string Season { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        
        public E06_EmploymentIndex2()
        {
            // TODO: Create Map property for Employment Index
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 let player = LoadDocument<Player>(employment.PlayerId)
                                 select new IndexEntry
                                            {
                                                Season = employment.Season,
                                                TeamName = team.Name,
                                                FirstName = player.FirstName,
                                                LastName = player.LastName
                                            };

            Index(e => e.FirstName, FieldIndexing.No);
            Index(e => e.LastName, FieldIndexing.No);
            Store(e => e.FirstName, FieldStorage.Yes);
            Store(e => e.LastName, FieldStorage.Yes);
        }
    }
}
