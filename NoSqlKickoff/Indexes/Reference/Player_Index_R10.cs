using System.Linq;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_R10 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string CountryOfTeam { get; set; }
        }

        public Player_Index_R10()
        {
            Map = players => from player in players
                             let team = LoadDocument<Team>(player.TeamId)
                             select new IndexEntry
                             {
                                 CountryOfTeam = team.Country
                             };
        }
    }
}
