using System.Linq;

using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E03_PlayerIndex : AbstractIndexCreationTask<Player>
    {

        public E03_PlayerIndex()
        {
            //TODO: Implement the Map property of the PlayerIndex
            Map = players => from player in players
                             select new
                             {
                                player.FirstName,
                                player.LastName
                             };
        }
    }
}
