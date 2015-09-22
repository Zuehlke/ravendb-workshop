using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Employment Index for E06_EmploymentInSeparateCollection
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/indexing-related-documents
    /// </remarks>
    /// <see cref="Player_Index_R09"/>
    public class E06_EmploymentIndex : AbstractIndexCreationTask<Employment>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }
            public string Season { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        
        public E06_EmploymentIndex()
        {
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 let player = LoadDocument<Player>(employment.PlayerId)
                                 select new IndexEntry
                                            {
                                                Season = employment.Season,
                                                TeamName = team.Name
                                            };

            // TODO: Add FirstName and LastName
        }
    }
}
