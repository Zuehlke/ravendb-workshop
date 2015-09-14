using System.Linq;

using NoSqlKickoff.Model;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes
{
    public class Player_Index_R05 : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FullName { get; set; }
        }

        public Player_Index_R05()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 FullName = player.FirstName + " " + player.LastName
                             };

            // We have to store the index field, otherwise we cannot use it for Projection
            // By default, Raven runs an analyzer over the Index fields, and only stores the analyzed/scrambled version.
            Store("FullName", FieldStorage.Yes);
        }
    }
}
