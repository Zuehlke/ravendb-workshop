using System.Linq;

using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E04_PlayerIndex : AbstractIndexCreationTask<Player, E04_PlayerIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string FullName { get; set; }
        }

        public E04_PlayerIndex()
        {
            Map = players => from player in players 
                             select new IndexEntry
                             {
                                 FullName = player.FirstName + " " + player.LastName
                             };
        }
    }
}
