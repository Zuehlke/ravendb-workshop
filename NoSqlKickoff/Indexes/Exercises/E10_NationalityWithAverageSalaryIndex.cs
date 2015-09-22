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
            // TODO: implement map property
            Map = employments => from employment in employments
                                 let player = LoadDocument<Player>(employment.PlayerId)
                                 select new IndexEntry
                                 {
                                     Salaries = new[] { employment.Salary },
                                     Nationality = player.Nationality.Name,
                                     AverageSalary = employment.Salary
                                 };

            Reduce = entries => from entry in entries
                                group entry by entry.Nationality
                                into g
                                let salaries = g.SelectMany(e => e.Salaries).ToArray()
                                let average = g.Average(e => e.AverageSalary)
                                select new IndexEntry
                                {
                                    Nationality = g.Key,
                                    Salaries = salaries,
                                    AverageSalary = average
                                };
        }
    }
}
