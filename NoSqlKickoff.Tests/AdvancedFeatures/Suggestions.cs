using System;
using System.Linq;
using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.AdvancedFeatures;
using NoSqlKickoff.Model.Reference;
using NoSqlKickoff.Tests.Reference;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.AdvancedFeatures
{
    [TestFixture]
    public class Suggestions : RavenTestBase
    {
        private IDocumentStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);
        }

        /// <summary>
        /// Try settings search term to 'margo' or 'chrigi' and check the output
        /// </summary>
        /// <remarks>
        /// More documentation: http://ravendb.net/docs/article-page/3.0/csharp/indexes/querying/suggestions
        /// </remarks>
        [Test]
        public void Suggestions_Demo()
        {
            InsertPlayers();

            const string searchTerm = "chrigi";

            using (var session = _store.OpenSession())
            {
                var query = session.Query<Player, Player_Index_Suggestions>().Where(p => p.FirstName == searchTerm);
                var player = query.FirstOrDefault();

                if (player == null)
                {
                    Console.WriteLine("Nobody found for '{0}'", searchTerm);
                    var suggestions = query.Suggest();

                    Console.WriteLine("Did you mean?");

                    foreach (var suggestion in suggestions.Suggestions)
                    {
                        Console.WriteLine("  " + suggestion);
                    }
                }
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
