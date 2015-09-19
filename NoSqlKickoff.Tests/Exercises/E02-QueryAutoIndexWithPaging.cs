using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model.Exercises;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E02_QueryAutoIndexWithPaging : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 3 and 4
        /// As a user I want to get a list of 5 players at once (paged list)
        /// As a user I want to get the second list of 5 players at once (paged list)
        /// </summary>
        /// <returns>A list of 5 players</returns>
        public List<Player> GetPagedListOfFivePlayers(int page)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Player>().Take(5).Skip(page - 1).ToList();
            }
        }

        [Test]
        public void GetPagedListOfFivePlayers_ShouldReturnTheFirstPageOfPlayers()
        {
            var players = GetPagedListOfFivePlayers(1);

            players.PrintDump();

            Assert.That(players.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetPagedListOfFivePlayers_ShouldReturnTheSecondPageOfPlayers()
        {
            var players = GetPagedListOfFivePlayers(2);

            players.PrintDump();

            Assert.That(players.Count, Is.EqualTo(5));
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

            // nasty: we have to initialize the dynamic index
            using (var session = _store.OpenSession())
            {
                session.Query<Player>().Any();
            }

            WaitForIndexing(_store);
        }
    }
}
