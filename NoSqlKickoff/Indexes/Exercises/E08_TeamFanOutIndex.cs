using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E08_TeamFanOutIndex : AbstractIndexCreationTask<Team, E08_TeamFanOutIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string TeamName { get; set; }

            public string Season { get; set; }
        }

        public E08_TeamFanOutIndex()
        {
            // TODO: implement map property
            Map = teams => from team in teams
                           from employment in team.EmploymentCopies
                           select new IndexEntry
                                        {
                                            TeamName = team.Name,
                                            FirstName = employment.FirstName,
                                            LastName = employment.LastName,
                                            Season = employment.Season
                                        };

            MaxIndexOutputsPerDocument = 30;

            Index(e => e.FirstName, FieldIndexing.No);
            Index(e => e.LastName, FieldIndexing.No);
            Store(e => e.FirstName, FieldStorage.Yes);
            Store(e => e.LastName, FieldStorage.Yes);
        }
    }
}
