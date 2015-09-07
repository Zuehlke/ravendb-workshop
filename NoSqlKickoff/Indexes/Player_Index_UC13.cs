using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

using NoSqlKickoff.Model;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_UC13 : AbstractIndexCreationTask<PlayerWithTeam>
    {
        public class IndexEntry
        {
            public string TeamCountryName;

            public string Nationality { get; set; }

            public bool PlaysInHisHomeCountry { get; set; }
        }

        public Player_Index_UC13()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 TeamCountryName = player.Team.Country,
                                 Nationality = player.Nationality.Name,
                                 PlaysInHisHomeCountry = player.Team.Country == player.Nationality.Name
                             };
        }
    }
}
