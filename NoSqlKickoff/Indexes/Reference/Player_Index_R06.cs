using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Reference
{
    public class Player_Index_R06 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }

            public string FullName { get; set; }
        }

        public Player_Index_R06()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 FirstName = player.FirstName,
                                 FullName = player.FirstName + " " + player.LastName
                             };
        }
    }
}
