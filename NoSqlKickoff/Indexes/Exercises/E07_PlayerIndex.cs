using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Index for E07_EmploymentEmbeddedInPlayer
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-indexes
    /// </remarks>
    public class E07_PlayerIndex : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public E07_PlayerIndex()
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
