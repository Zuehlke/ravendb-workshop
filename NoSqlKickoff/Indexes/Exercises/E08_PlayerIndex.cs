using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Index for E08_EmploymentCopyEmbeddedInPlayerAndTeam
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-indexes
    /// </remarks>
    public class E08_PlayerIndex : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public E08_PlayerIndex()
        {
            Map = players => from player in players
                             select new IndexEntry
                                        {
                                            FirstName = player.FirstName,
                                            LastName = player.LastName
                                        };
        }
    }
}
