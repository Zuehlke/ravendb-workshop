using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E08_PlayerIndex : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public E08_PlayerIndex()
        {
            // TODO: implement map property
            Map = players => from player in players
                             select new IndexEntry
                                        {
                                            FirstName = player.FirstName,
                                            LastName = player.LastName
                                        };
        }
    }
}
