using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    public class UC_03_Include : RavenTestBase
    {
        private IDocumentStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            IndexCreation.CreateIndexes(typeof(Player_Index).Assembly, _store);

            using (var session = _store.OpenSession())
            {
                var team = new Team { Name = "Zühlke Days All Stars" };
                session.Store(team);
                session.Store(new Player { FirstName = "Lionel", LastName = "Messi", TeamId = team.Id });
                session.Store(new Player { FirstName = "Bastian", LastName = "Schweinsteiger", TeamId = team.Id });
                session.Store(new Player { FirstName = "Christiano", LastName = "Ronaldo", TeamId = team.Id });
                session.Store(new Player { FirstName = "Stephane", LastName = "Chapuisat", TeamId = team.Id });

                session.SaveChanges();
            }

            WaitForIndexing(_store);
        }

        [Test]
        public void Include()
        {
            //Similar to Join in relational database

            using (var session = _store.OpenSession())
            {
                var allPlayers = session.Query<Player>()
                    .Include(p => p.TeamId)
                    .ToList()
                    .Select(
                        p =>
                            {
                                p.Team = session.Load<Team>(p.TeamId);
                                return p;
                            })
                    .ToList();

                Assert.IsTrue(allPlayers.All(p => p.Team.Name == "Zühlke Days All Stars"));
            }
        }
    }
}
