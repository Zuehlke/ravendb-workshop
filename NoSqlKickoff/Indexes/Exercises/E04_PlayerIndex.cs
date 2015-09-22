using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Index for E04_QueryStaticIndexWithTypeCoercion
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-indexes#combining-multiple-fields-together
    /// </remarks>
    /// <see cref="Player_Index_R04"/>
    public class E04_PlayerIndex : AbstractIndexCreationTask<Player, E04_PlayerIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string FullName { get; set; }

            public string Nationality { get; set; }
        }

        public E04_PlayerIndex()
        {
            // TODO: Implement the Map property of the PlayerIndex
            Map = players => from player in players 
                             select new IndexEntry
                             {
                                 FullName = player.FirstName + " " + player.LastName,
                                 Nationality = player.Nationality.Name
                             };
        }
    }
}
