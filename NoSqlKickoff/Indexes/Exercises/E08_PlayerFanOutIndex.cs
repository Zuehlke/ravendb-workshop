using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Fan Out Index for E08_EmploymentCopyEmbeddedInPlayerAndTeam
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/indexing-hierarchical-data
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/fanout-indexes
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/storing-data-in-index
    /// </remarks>
    public class E08_PlayerFanOutIndex : AbstractIndexCreationTask<Player, E08_PlayerFanOutIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public string Season { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public E08_PlayerFanOutIndex()
        {
            // TODO: implement map property
            Map = players => from player in players
                             from employment in player.EmploymentCopies
                             select new IndexEntry
                                        {
                                            FirstName = player.FirstName,
                                            LastName = player.LastName,
                                            TeamName = employment.TeamName, 
                                            Season = employment.Season
                                        };

            // we need to configure the fan-out maximum
            MaxIndexOutputsPerDocument = 30;

            Index(e => e.FirstName, FieldIndexing.No);
            Index(e => e.LastName, FieldIndexing.No);
            Store(e => e.FirstName, FieldStorage.Yes);
            Store(e => e.LastName, FieldStorage.Yes);
        }
    }
}
