using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Employment Index for E06_EmploymentInSeparateCollection
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/indexing-related-documents
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/storing-data-in-index
    /// </remarks>
    /// <see cref="Player_Index_R05"/>
    /// <see cref="Player_Index_R09"/>
    public class E06_EmploymentIndexWithStore : AbstractIndexCreationTask<Employment, E06_EmploymentIndexWithStore.IndexEntry>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }
            public string Season { get; set; }
        }
        
        public E06_EmploymentIndexWithStore()
        {
            // TODO: Create Map property for Employment Index
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 let player = LoadDocument<Player>(employment.PlayerId)
                                 select new IndexEntry
                                            {
                                                Season = employment.Season,
                                                TeamName = team.Name
                                            };

            Store(e => e.TeamName, FieldStorage.Yes);
        }
    }
}
