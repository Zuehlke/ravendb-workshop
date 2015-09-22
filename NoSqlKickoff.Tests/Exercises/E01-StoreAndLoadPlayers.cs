using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model.Exercises;
using NoSqlKickoff.Tests.Reference;

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
        /// As a user I want to store a list of the following players: 
        /// "Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger".
        /// </summary>
        /// <returns>
        /// An array of the ids of the players
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/what-is-a-session-and-how-does-it-work
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/opening-a-session
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/storing-entities
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/saving-changes
        /// </remarks>
        /// <see cref="R01_StoreAndLoad"/>
        public string[] StoreListOfPlayers()
        {
            // HINT: OpenSession()
            // HINT: Store()
            // HINT: SaveChanges()

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Exercise 2
        /// As a user I want to be able to receive back the list of players 
        /// I stored before ("Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger") 
        /// given a list of their ids
        /// </summary>
        /// <returns>
        /// A list of player objects
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/loading-entities
        /// </remarks>
        /// <see cref="R01_StoreAndLoad"/>
        public List<Player> GetListOfPlayersById(string[] ids)
        {
            // HINT: OpenSession()
            // HINT: Load()
            
            throw new NotImplementedException();
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

                // if you run this test in debug mode, you can access the management web UI
                WaitForUserToContinueTheTest(_store);

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

            var players = GetListOfPlayersById(ids);

            players.PrintDump();

            Assert.That(players.Count, Is.EqualTo(3));
            Assert.That(players.Any(p => p.LastName == "Ronaldo"));
            Assert.That(players.Any(p => p.LastName == "Messi"));
            Assert.That(players.Any(p => p.LastName == "Schweinsteiger"));
        }
    }
}
