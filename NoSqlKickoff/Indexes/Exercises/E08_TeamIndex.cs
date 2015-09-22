using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Team Index for E08_EmploymentCopyEmbeddedInPlayerAndTeam
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-indexes
    /// </remarks>
    public class E08_TeamIndex : AbstractIndexCreationTask<Team>
    {
        public E08_TeamIndex()
        {
            // TODO: implement map property
        }
    }
}
