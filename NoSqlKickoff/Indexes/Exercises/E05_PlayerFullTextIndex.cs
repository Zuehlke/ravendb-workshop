using System.Linq;

using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Exercises;

using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E05_PlayerFullTextIndex : AbstractIndexCreationTask<Player>
    {
        public E05_PlayerFullTextIndex()
        {
            //TODO: Create full text index for player (FirstName and LastName)
            Map = players => from player in players
                                 select new
                                            {
                                                player.FirstName, player.LastName
                                            
                                            };

            Index(p => p.FirstName, FieldIndexing.Analyzed);
            Index(p => p.LastName, FieldIndexing.Analyzed);
            Analyze(p => p.FirstName, "StandardAnalyzer");
            Analyze(p => p.LastName, "StandardAnalyzer");
        }
    }
}
