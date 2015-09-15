using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    using NoSqlKickoff.Model;

    public class E06_EmploymentIndex : AbstractIndexCreationTask<Employment>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public string Season { get; set; }
        }
        
        public E06_EmploymentIndex()
        {
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 select new IndexEntry
                                            {
                                                Season = employment.Season,
                                                TeamName = team.Name
                                            };
        }
    }
}
