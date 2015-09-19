using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model.Exercises;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    [TestFixture]
    public class E04_QueryStaticIndexWithTypeCoercion : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 7
        /// As a user I want to find the player "Christiano Ronaldo" by querying the full name
        /// </summary>
        public Player FindChristiano()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E04_PlayerIndex.IndexEntry, E04_PlayerIndex>()
                    .Where(p => p.FullName == "Christiano Ronaldo")
                    .OfType<Player>()
                    .SingleOrDefault();
            }
        }

        /// <summary>
        /// TODO: Exercise 8
        /// As a user I want to find all players that have the Nationality "Brazil"
        /// </summary>
        /// <returns>A list of brazilian players</returns>
        public List<Player> FindBrazilianPlayers()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E04_PlayerIndex.IndexEntry, E04_PlayerIndex>()
                        .Where(p => p.Nationality == "Brazil")
                        .OfType<Player>()
                        .ToList();
            }
        }

        [Test]
        public void FindChristiano_ShouldReturnChristiano()
        {
            var christiano = FindChristiano();

            christiano.PrintDump();

            Assert.That(christiano.LastName, Is.EqualTo("Ronaldo"));
        }

        [Test]
        public void FindBrazilianPlayers_ShouldReturnAListOfBrazilianPlayers()
        {
            var brazilisanPlayers = FindBrazilianPlayers();

            brazilisanPlayers.PrintDump();

            Assert.That(brazilisanPlayers.Count, Is.AtLeast(1));
            Assert.That(brazilisanPlayers.All(p => p.Nationality.Name == "Brazil"), Is.True);
        }

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();

            IndexCreation.CreateIndexes(typeof(Player).Assembly, _store);

            var players = DataGenerator.CreatePlayerList();

            using (var bulkInsert = _store.BulkInsert())
            {
                foreach (var player in players)
                {
                    bulkInsert.Store(player);
                }
            }

            WaitForIndexing(_store);
        }
    }
}
