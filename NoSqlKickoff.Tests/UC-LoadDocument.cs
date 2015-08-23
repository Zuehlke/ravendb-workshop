using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    public class UC_LoadDocument : RavenTestBase
    {
        private IDocumentStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            IndexCreation.CreateIndexes(typeof(Player_Index_UC03).Assembly, _store);

            using (var session = _store.OpenSession())
            {
                var team1 = new Team { Name = "Zühlke Days All Stars", Country = "Germany" };
                var team2 = new Team { Name = "FIFA 15 All Stars", Country = "Switzerland" };
                session.Store(team1);
                session.Store(team2);
                session.Store(new Player { FirstName = "Lionel", LastName = "Messi", TeamId = team1.Id });
                session.Store(new Player { FirstName = "Bastian", LastName = "Schweinsteiger", TeamId = team1.Id });
                session.Store(new Player { FirstName = "Christiano", LastName = "Ronaldo", TeamId = team2.Id });
                session.Store(new Player { FirstName = "Stephane", LastName = "Chapuisat", TeamId = team2.Id });

                session.SaveChanges();
            }

            WaitForIndexing(_store);
        }

        [Test]
        public void QueryWithIndexOnTeamName()
        {
            using (var session = _store.OpenSession())
            {
                var playersFromSwitzerland = session.Query<Player_Index_UC03.IndexEntry, Player_Index_UC03>()
                    //.Where(p => p.Country == "Switzerland")
                    .ProjectFromIndexFieldsInto<Player_Index_UC03.IndexEntry>()
                    .ToList();

                Assert.That(playersFromSwitzerland.Count(), Is.EqualTo(2));
            }
        }
    }
}
