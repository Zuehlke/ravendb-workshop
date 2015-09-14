using System.Linq;

using NoSqlKickoff.Model;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_R11 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string CountryOfTeam { get; set; }

            public Team Team { get; set; }
        }

        public Player_Index_R11()
        {
            Map = players => from player in players
                             let team = LoadDocument<Team>(player.TeamId)
                             select new IndexEntry
                             {
                                 CountryOfTeam = team.Country,
                                 Team = team
                             };

            // we store the team in the index
            Store("Team", FieldStorage.Yes);

            // but we do not run any analyzers on it, because we don't want to search on it
            Index("Team", FieldIndexing.No);
        }
    }
}
