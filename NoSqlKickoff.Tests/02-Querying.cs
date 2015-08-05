using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    public class _02_Querying : RavenTestBase
    {
        private IDocumentStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            using (var session = _store.OpenSession())
            {
                session.Store(new Player { FirstName = "Lionel", LastName = "Messi" });
                session.Store(new Player { FirstName = "Bastian", LastName = "Schweinsteiger" });
                session.Store(new Player { FirstName = "Christiano", LastName = "Ronaldo" });
                session.Store(new Player { FirstName = "Stephane", LastName = "Chapuisat" });

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

                Assert.That(allPlayers.Count(), Is.EqualTo(4));
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
                Assert.That(stats.TotalResults, Is.EqualTo(4));
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

                

                WaitForUserToContinueTheTest(_store);

                Assert.That(filteredResults.Count(), Is.EqualTo(1));
            }
        }
    }
}
