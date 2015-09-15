using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E01_StoreAndLoadPlayers : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 1
        /// As a user I want save a list of the following players: 
        /// "Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger".
        /// </summary>
        /// <returns>
        /// An array of the ids of the players
        /// </returns>
        public string[] StoreListOfPlayers()
        {
            var ids = new List<string>();

            using (var session = _store.OpenSession())
            {
                var christiano = new Player { FirstName = "Christiano", LastName = "Ronaldo" };
                var lionel = new Player { FirstName = "Lionel", LastName = "Messi" };
                var bastian = new Player { FirstName = "Bastian", LastName = "Schweinsteiger" };
                
                session.Store(christiano);
                session.Store(lionel);
                session.Store(bastian);
                
                ids.Add(christiano.Id);
                ids.Add(lionel.Id);
                ids.Add(bastian.Id);

                session.SaveChanges();
            }

            return ids.ToArray();
        }

        /// <summary>
        /// TODO: Exercise 2
        /// As a user I want to be able to receive back the list of players 
        /// I stored before ("Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger") 
        /// given a list of their ids
        /// </summary>
        public List<Player> GetListOfPlayersById(string[] ids)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<Player>(ids).ToList();
            }
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

                // oops, at this point the player list is empty, because the dynamic index was just created and is still stale
                // We have to wait for indexing here in order to get some results

                WaitForIndexing(_store);

                players = session.Query<Player>().ToList();

                players.PrintDump();

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

            WaitForIndexing(_store);

            var players = GetListOfPlayersById(ids);

            players.PrintDump();

            Assert.That(players.Count, Is.EqualTo(3));
            Assert.That(players.Any(p => p.LastName == "Ronaldo"));
            Assert.That(players.Any(p => p.LastName == "Messi"));
            Assert.That(players.Any(p => p.LastName == "Schweinsteiger"));
        }
    }
}
