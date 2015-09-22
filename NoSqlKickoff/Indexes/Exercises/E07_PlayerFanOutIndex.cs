using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Fan Out Index for E07_EmploymentEmbeddedInPlayer
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/indexing-hierarchical-data
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/fanout-indexes
    /// </remarks>
    public class E07_PlayerFanOutIndex : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public string Season { get; set; }
        }

        public E07_PlayerFanOutIndex()
        {
            // TODO: implement map property
            
            // TODO: configure the fan-out maximum
        }
    }
}
