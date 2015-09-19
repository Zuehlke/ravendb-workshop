using System;
using System.Linq;
using System.Text;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.AdvancedFeatures;

using NoSqlKickoff.Model.Reference;
using NoSqlKickoff.Tests.Reference;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.AdvancedFeatures
{
    public class Highlighting : RavenTestBase
    {
        private IDocumentStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);
        }

        [Test]
        public void Highlighting_Demo()
        {
            InsertPlayers();

            using (var session = _store.OpenSession())
            {
                FieldHighlightings highlightings;

                var results = session
                    .Advanced
                    .DocumentQuery<Player, Player_Index_Suggestions>()
                    .Highlight("FirstName", 18, 1, out highlightings)
                    .SetHighlighterTags("<b>", "</b>")
                    .Search("FirstName", "ma*")
                    .ToList();

                var builder = new StringBuilder().AppendLine("<ul>");

                foreach (var result in results)
                {
                    var fragments = highlightings.GetFragments(result.Id);
                    builder.AppendLine(string.Format("  <li>{0}</li>", fragments.First()));
                }

                string ul = builder.AppendLine("</ul>").ToString();
                
                Console.WriteLine(ul);
            }
        }

        private void InsertPlayers()
        {
            using (var session = _store.OpenSession())
            {
                var players = PlayerGenerator.GetPlayers();
                foreach (var player1 in players)
                {
                    session.Store(player1);
                }

                session.SaveChanges();
            }

            WaitForIndexing(_store);
        }
    }
}