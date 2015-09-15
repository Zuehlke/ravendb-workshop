using System.Linq;

using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    [TestFixture]
    public class E04_QueryStaticIndexWithTypeCoercion : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 7
        /// As a user I want to find the player "Christiano Ronaldo" by querying the full name
        /// </summary>
        public Player FindChristiano()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E04_PlayerIndex.IndexEntry, E04_PlayerIndex>()
                    .Where(p => p.FullName == "Christiano Ronaldo")
                    .OfType<Player>()
                    .SingleOrDefault();
            }
        }

        [Test]
        public void FindChristiano_ShouldReturnChristiano()
        {
            var christiano = FindChristiano();

            christiano.PrintDump();

            Assert.That(christiano.LastName, Is.EqualTo("Ronaldo"));
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
