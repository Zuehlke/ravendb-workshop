using System.Linq;
using NoSqlKickoff.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.AdvancedFeatures
{
    public class Player_Index_Suggestions : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string FirstName { get; set; }
        }

        public Player_Index_Suggestions()
        {
            Map = players => from player in players
                             select new IndexEntry
                             {
                                 FirstName = player.FirstName
                             };

            Indexes.Add(p => p.FirstName, FieldIndexing.Analyzed);

            Store(x => x.FirstName, FieldStorage.Yes);
            Suggestion(p => p.FirstName, new SuggestionOptions());
            
            TermVector(p => p.FirstName, FieldTermVector.WithPositionsAndOffsets); // just used for highlighting
        }
    }
}