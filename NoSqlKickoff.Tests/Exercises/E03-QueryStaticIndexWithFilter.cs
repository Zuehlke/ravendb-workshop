using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model.Exercises;
using NoSqlKickoff.Tests.Reference;

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
        /// <returns>
        /// A player object of Christiano
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-query
        /// </remarks>
        /// <see cref="R03_Querying_StaticIndex"/>
        public Player FindChristiano()
        {
            // HINT: Query()
            // HINT: Where()
            // HINT: SingleOrDefault()

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Exercise 6
        /// As a user I want to query for all players that start with a "C" in the firstname
        /// </summary>
        /// <returns>
        /// A list of players whose first name starts with "C"
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-query
        /// </remarks>
        /// <see cref="R03_Querying_StaticIndex"/>
        public List<Player> FindPlayersStartingWithC()
        {
            // HINT: Query()
            // HINT: Where()
            
            throw new NotImplementedException();
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

        [OneTimeSetUp]
        public void TestFixtureSetUp()
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
