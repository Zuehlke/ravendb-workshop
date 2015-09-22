using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Index for E03_QueryStaticIndexWithFilter
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-indexes
    /// </remarks>
    /// <see cref="Player_Index_R03"/>
    public class E03_PlayerIndex : AbstractIndexCreationTask<Player>
    {
        public E03_PlayerIndex()
        {
            // TODO: Implement the Map property
            // HINT: players => from player in players
        }
    }
}
