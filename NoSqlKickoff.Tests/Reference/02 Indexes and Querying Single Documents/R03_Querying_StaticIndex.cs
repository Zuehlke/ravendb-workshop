using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    /// <summary>
    /// Use Case: Simple querying against a statically defined index
    /// Goal: Query the Player collection
    /// </summary>
    public class R03_Querying_StaticIndex : RavenTestBase
    {
        private IDocumentStore _store;

        private List<Player> _players;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            // We first have to create the static indexes
            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

            _players = DataGenerator.CreatePlayerList();

            // Store some players in the database
            using (var session = _store.OpenSession())
            {
                foreach (var player in _players)
                {
                    session.Store(player);
                }

                session.SaveChanges();
            }

            // Let's wait for indexing to happen
            // this method is part of RavenTestBase and thus should only be used in tests
            WaitForIndexing(_store);
        }

        [Test]
        public void SimpleQuery_AllPlayers()
        {
            using (var session = _store.OpenSession())
            {
                var allPlayers = session.Query<Player, Player_Index_R03>().ToList();

                Assert.That(allPlayers.Count(), Is.EqualTo(_players.Count));
            }
        }

        [Test]
        public void SimpleQuery_WithFilter()
        {
            using (var session = _store.OpenSession())
            {
                var filteredResults = session.Query<Player, Player_Index_R03>()
                    .Where(p => p.FirstName.StartsWith("C"))
                    .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(1));
            }
        }
    }
}
