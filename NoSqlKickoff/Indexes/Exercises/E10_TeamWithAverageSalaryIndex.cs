using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Employment Map Reduce Index for E10_Salaries
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-reduce-indexes
    /// </remarks>
    /// <see cref="Player_Index_R13"/>
    public class E10_TeamWithAverageSalaryIndex : AbstractIndexCreationTask<Employment, E10_TeamWithAverageSalaryIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public decimal[] Salaries { get; set; }

            public string TeamId { get; set; }

            public decimal AverageSalary { get; set; }
        }

        public E10_TeamWithAverageSalaryIndex()
        {
            // TODO: implement Map property
            
            // TODO: implement Reduce property
        }
    }
}
