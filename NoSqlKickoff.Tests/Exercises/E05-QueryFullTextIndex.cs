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
    public class E05_QueryFullTextIndex : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 9
        /// As a user I want to find players that contain the name fragment "van", "di" or "de"
        /// </summary>
        /// <returns>
        /// A list of players which contain any of the name fragments "van", "di" or "de"
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-use-search
        /// </remarks>
        public List<Player> FindPlayersWithNameFragments()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Player, E05_PlayerFullTextIndex>()
                    .Search(p => p.LastName, "van")
                    .Search(p => p.LastName, "di")
                    .Search(p => p.LastName, "de")
                    .ToList();
            }
        }

        /// <summary>
        /// TODO: Exercise 10
        /// As a user I want to find players whose first name ends with "an"
        /// </summary>
        /// <returns>
        /// A list of players whose first name ends with "an"
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-use-search
        /// </remarks>
        public List<Player> FindPlayerWithFirstNameEndingWithAn()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Player, E05_PlayerFullTextIndex>()
                    .Search(p => p.FirstName, "*an", escapeQueryOptions: EscapeQueryOptions.AllowAllWildcards)
                    .ToList();
            }
        }

        [Test]
        public void FindPlayersWithNameFragments_ShouldReturnAListOfPlayersWithNameFragments()
        {
            var playersWithNameFragments = FindPlayersWithNameFragments();

            playersWithNameFragments.PrintDump();

            Assert.That(playersWithNameFragments.Count, Is.AtLeast(1));
            Assert.IsTrue(playersWithNameFragments.Any(p => p.LastName.Contains("van")));
            Assert.IsTrue(playersWithNameFragments.Any(p => p.LastName.Contains("di")));
            Assert.IsTrue(playersWithNameFragments.Any(p => p.LastName.Contains("de")));
        }

        [Test]
        public void FindPlayerWithFirstNameEndingWithAn_ShouldReturnAListOfPlayersEndingWithAn()
        {
            var playersEndingWithAn = FindPlayerWithFirstNameEndingWithAn();

            playersEndingWithAn.PrintDump();

            Assert.That(playersEndingWithAn.Count, Is.AtLeast(1));
            Assert.IsTrue(playersEndingWithAn.All(p => p.FirstName.EndsWith("an")));
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
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
