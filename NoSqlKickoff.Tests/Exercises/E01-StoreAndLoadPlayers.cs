using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E01_StoreAndLoadPlayers : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// Exercise 1: As a user I want save a list of the following players: 
        /// "Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger".
        /// </summary>
        /// <returns>
        /// An array of the ids of the players
        /// </returns>
        public string[] StoreListOfPlayers()
        {
            return new string[]{ };
        }

        /// <summary>
        /// Exercise 2: As a user I want to be able to receive back the whole 
        /// list of players ("Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger") 
        /// I stored before and load them by their id for verification
        /// </summary>
        public List<Player> GetListOfPlayersById(string[] ids)
        {
            return new List<Player>();
        }

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
        }

        [Test]
        public void StoreListOfPlayers_ShouldHaveStoredAListOfPlayers()
        {
            StoreListOfPlayers();

            using (var session = _store.OpenSession())
            {
                var players = session.Query<Player>().ToList();

                Assert.That(players.Count, Is.EqualTo(3));
                Assert.That(players.Any(p => p.LastName == "Ronaldo"));
                Assert.That(players.Any(p => p.LastName == "Messi"));
                Assert.That(players.Any(p => p.LastName == "Schweinsteiger"));
            }
        }

        [Test]
        public void GetListOfPlayersById_ShouldReturnAListOfPlayers()
        {
            var ids = StoreListOfPlayers();

            var players = GetListOfPlayersById(ids);

            Assert.That(players.Count, Is.EqualTo(3));
            Assert.That(players.Any(p => p.LastName == "Ronaldo"));
            Assert.That(players.Any(p => p.LastName == "Messi"));
            Assert.That(players.Any(p => p.LastName == "Schweinsteiger"));
        }
    }
}
