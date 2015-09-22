using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Reference
{
    public class Player_Index_R03 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }
        }

        public Player_Index_R03()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 FirstName = player.FirstName
                             };
        }
    }
}
