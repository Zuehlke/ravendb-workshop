using System.Linq;

using NoSqlKickoff.Model;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_UC05 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string Name { get; set; }
        }

        public Player_Index_UC05()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 Name = player.FirstName + " " + player.LastName
                             };

            // We have to store the index field, otherwise we cannot use it for Projection
            // By default, Raven runs an analyzer over the Index fields, and only stores the analyzed/scrambled version.
            Store("Name", FieldStorage.Yes);
        }
    }
}
