using System.Collections.Generic;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E02_QueryAutoIndexWithPaging : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// Exercise 3: As a user I need to get a list of 5 players at once (paged list)
        /// </summary>
        /// <returns></returns>
        public List<Player> GetPagedListOfFivePlayers(int page)
        {
            return new List<Player>();
        }

        [Test]
        public void GetPagedListOfFivePlayers_ShouldReturnTheFirstPageOfPlayers()
        {
            var players = GetPagedListOfFivePlayers(1);

            Assert.That(players.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetPagedListOfFivePlayers_ShouldReturnTheSecondPageOfPlayers()
        {
            var players = GetPagedListOfFivePlayers(2);

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
        }
    }
}
