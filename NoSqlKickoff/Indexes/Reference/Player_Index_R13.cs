using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_R13 : AbstractIndexCreationTask<Player, Player_Index_R13.IndexEntry>
    {
        public class IndexEntry
        {
            public string Country { get; set; }

            public int NumberOfPlayers { get; set; }

            public List<string> Players { get; set; }
        }

        public Player_Index_R13()
        {
            Map = players => from player in players
                             let team = LoadDocument<Team>(player.TeamId)
                             select new IndexEntry
                             {
                                 NumberOfPlayers = 1,
                                 Players = new List<string> { player.FirstName + " " + player.LastName },
                                 Country = team.Country
                             };

            Reduce = indexEntries => from indexEntry in indexEntries
                                     group indexEntry by indexEntry.Country
                                     into g
                                     select
                                         new IndexEntry
                                             {
                                                 Country = g.Key,
                                                 Players = g.SelectMany(e => e.Players).ToList(),
                                                 NumberOfPlayers = g.Sum(e => e.NumberOfPlayers)
                                             };
        }
    }
}
