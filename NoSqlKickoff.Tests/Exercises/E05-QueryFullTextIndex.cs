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
    [TestFixture]
    public class E05_QueryFullTextIndex : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 8
        /// As a user I want to find players that contain the name fragment "van"
        /// </summary>
        public List<Player> FindPlayersWithVan()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Player, E05_PlayerFullTextIndex>()
                    .Search(p => p.FirstName, "van*", escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                    .Search(p => p.LastName, "van*", escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                    .ToList();
            }
        }

        [Test]
        public void FindPlayersWithVan_ShouldReturnAListOfPlayersWithVan()
        {
            var playersWithVan = FindPlayersWithVan();

            playersWithVan.PrintDump();

            Assert.That(playersWithVan.Count, Is.AtLeast(1));
            Assert.IsTrue(playersWithVan.All(p => p.LastName.Contains("van")));
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
