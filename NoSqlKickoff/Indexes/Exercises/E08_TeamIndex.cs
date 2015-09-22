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
            Map = teams => from team in teams
                           select new
                           {
                               team.Name
                           };

        }
    }
}
