using System.Linq;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_UC07 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }
        }

        public Player_Index_UC07()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 FirstName = player.FirstName
                             };
        }
    }
}
