using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
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
            // TODO: implement map property
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 select new IndexEntry
                                 {
                                     TeamId = employment.TeamId,
                                     Salaries = new[] { employment.Salary },
                                     TeamName = team.Name,
                                     AverageSalary = employment.Salary
                                 };

            Reduce = entries => from entry in entries
                                group entry by entry.TeamId
                                into g
                                let salaries = g.SelectMany(e => e.Salaries).ToArray()
                                let average = g.Average(e => e.AverageSalary)
                                select new IndexEntry
                                {
                                    TeamId = g.Key,
                                    TeamName = g.First().TeamName,
                                    Salaries = salaries,
                                    AverageSalary = average
                                };
        }
    }
}
