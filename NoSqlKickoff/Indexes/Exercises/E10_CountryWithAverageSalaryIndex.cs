using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E10_CountryWithAverageSalaryIndex : AbstractIndexCreationTask<Employment, E10_CountryWithAverageSalaryIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string Country { get; set; }

            public decimal[] Salaries { get; set; }

            public decimal AverageSalary { get; set; }
        }

        public E10_CountryWithAverageSalaryIndex()
        {
            // TODO: implement map property
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 select new IndexEntry
                                 {
                                     Salaries = new[] { employment.Salary },
                                     Country = team.Country,
                                     AverageSalary = employment.Salary
                                 };

            Reduce = entries => from entry in entries
                                group entry by entry.Country
                                into g
                                let salaries = g.SelectMany(e => e.Salaries).ToArray()
                                let average = g.Average(e => e.AverageSalary)
                                select new IndexEntry
                                {
                                    Country = g.Key,
                                    Salaries = salaries,
                                    AverageSalary = average
                                };
        }
    }
}
