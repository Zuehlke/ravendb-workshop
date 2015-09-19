using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Exercises;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E03_QueryStaticIndexWithFilter : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 5
        /// As a user I want to find the player "Christiano Ronaldo"
        /// </summary>
        public Player FindChristiano()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Player, E03_PlayerIndex>().SingleOrDefault(p => p.FirstName == "Christiano" && p.LastName == "Ronaldo");
            }
        }

        /// <summary>
        /// TODO: Exercise 6
        /// As a user I want to query for all players that start with a "C" in the firstname
        /// </summary>
        public List<Player> FindPlayersStartingWithC()
        {
            using (var sesion = _store.OpenSession())
            {
                return sesion.Query<Player, E03_PlayerIndex>().Where(p => p.FirstName.StartsWith("C")).ToList();
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
        public void FindPlayersStartingWithC_ShouldReturnAListOfAllPlayersStartingWithC()
        {
            var players = FindPlayersStartingWithC();

            players.PrintDump();

            Assert.That(players.Count, Is.AtLeast(5));
            Assert.IsTrue(players.All(p => p.FirstName.StartsWith("C")));
        }

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();

            // We need to tell the server which indexes he should create
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
