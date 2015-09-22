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
    public class E10_NationalityWithAverageSalaryIndex : AbstractIndexCreationTask<Employment, E10_NationalityWithAverageSalaryIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string Nationality { get; set; }

            public decimal[] Salaries { get; set; }

            public decimal AverageSalary { get; set; }
        }

        public E10_NationalityWithAverageSalaryIndex()
        {
            // TODO: implement Map property
            
            // TODO: implement Reduce property
        }
    }
}
