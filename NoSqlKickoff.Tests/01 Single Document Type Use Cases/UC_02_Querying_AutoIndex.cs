using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    /// <summary>
    /// Use Case: Simple querying against an automatically created index
    /// Goal: Query the Player collection with Filtering and Paging
    /// </summary>
    public class UC_02_Querying_AutoIndex : RavenTestBase
    {
        private IDocumentStore _store;

        private List<Player> _players;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            _players = DataGenerator.CreatePlayerList();

            // Store some players in the database
            using (var session = _store.OpenSession())
            {
                foreach (var player in _players)
                {
                    session.Store(player);
                }

                session.SaveChanges();
            }
        }

        [Test]
        public void SimpleQuery_AllPlayers()
        {
            using (var session = _store.OpenSession())
            {
                // Stale Index --> Query returns immediately with no results
                //var allPlayers = session.Query<Player>().ToList();

                // Wait for unstale index
                var allPlayers = session.Query<Player>().Customize(c => c.WaitForNonStaleResultsAsOfNow()).ToList();

                Assert.That(allPlayers.Count, Is.EqualTo(_players.Count));

                WaitForUserToContinueTheTest(_store);
            }
        }

        [Test]
        public void SimpleQuery_WithPaging()
        {
            using (var session = _store.OpenSession())
            {
                RavenQueryStatistics stats;
                var firstPage = session.Query<Player>()
                    .Customize(c => c.WaitForNonStaleResultsAsOfNow())
                    .Statistics(out stats)
                    .Skip(0)
                    .Take(2)
                    .ToList();

                Assert.That(firstPage.Count(), Is.EqualTo(2));
                Assert.That(stats.TotalResults, Is.EqualTo(_players.Count));
            }
        }

        [Test]
        public void SimpleQuery_WithFilter()
        {
            using (var session = _store.OpenSession())
            {
                var filteredResults = session.Query<Player>()
                    .Customize(c => c.WaitForNonStaleResultsAsOfNow())
                    .Where(p => p.FirstName.StartsWith("C"))
                    .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(1));
            }
        }
    }
}
