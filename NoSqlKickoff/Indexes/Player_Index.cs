using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index : AbstractIndexCreationTask<Player>
    {
        public class Result
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string MiddleName { get; set; }

            public string TeamName { get; set; }

            public string Country { get; set; }
        }

        public Player_Index()
        {
            Map = players => from player in players
                             let team = LoadDocument<Team>(player.TeamId)
                             select new Result
                             {
                                 FirstName = player.FirstName,
                                 LastName = player.LastName,
                                 MiddleName = player.MiddleName,
                                 TeamName = team.Name,
                                 Country = team.Country
                             };

            Store("TeamName", FieldStorage.Yes);
            Index("TeamName", FieldIndexing.No);
        }
    }
}
