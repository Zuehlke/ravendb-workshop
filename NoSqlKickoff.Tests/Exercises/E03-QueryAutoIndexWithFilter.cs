using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E03_QueryAutoIndexWithFilter : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// Exercise 5: As a user I want to find the player "Christiano Ronaldo"
        /// </summary>
        public Player FindChristiano()
        {
            return new Player();
        }

        /// <summary>
        /// Exercise 6: As a user I want to query for all players that start with a "C" in the firstname
        /// </summary>
        public List<Player> FindPlayersStartingWithC()
        {
            return new List<Player>();
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

            Assert.That(players.Count, Is.AtLeast(1));
            Assert.IsTrue(players.All(p => p.FirstName.StartsWith("c")));
        }

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();

            var players = DataGenerator.CreatePlayerList();

            using (var bulkInsert = _store.BulkInsert())
            {
                foreach (var player in players)
                {
                    bulkInsert.Store(player);
                }
            }
        }
    }
}
