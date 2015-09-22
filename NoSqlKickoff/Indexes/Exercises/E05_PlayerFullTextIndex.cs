using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Player Full Text Index for E05_QueryFullTextIndex
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/using-analyzers
    /// </remarks>
    public class E05_PlayerFullTextIndex : AbstractIndexCreationTask<Player>
    {
        public E05_PlayerFullTextIndex()
        {
            Map = players => from player in players
                                 select new
                                            {
                                                player.FirstName, 
                                                player.LastName
                                            };

            Index(p => p.FirstName, FieldIndexing.Analyzed);
            Index(p => p.LastName, FieldIndexing.Analyzed);
        }
    }
}
