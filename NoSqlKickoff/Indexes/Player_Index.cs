using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index : AbstractIndexCreationTask<Player>
    {
        public Player_Index()
        {
            Map = players => from player in players
                             select new
                             {
                                 player.FirstName,
                                 player.LastName,
                                 player.MiddleName
                             };
        }
    }
}
