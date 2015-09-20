using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
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
