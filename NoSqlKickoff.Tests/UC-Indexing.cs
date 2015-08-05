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
    public class UC_Indexing : RavenTestBase
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
                session.Store(new Player { FirstName = "Lionel", LastName = "Messi" });
                session.Store(new Player { FirstName = "Bastian", LastName = "Schweinsteiger" });
                session.Store(new Player { FirstName = "Christiano", LastName = "Ronaldo" });
                session.Store(new Player { FirstName = "Stephane", LastName = "Chapuisat" });

                session.SaveChanges();
            }

            WaitForIndexing(_store);
        }

        [Test]
        public void SimpleQuery_AllPlayers()
        {
            using (var session = _store.OpenSession())
            {
                var allPlayers = session.Query<Player, Player_Index>().ToList();

                Assert.That(allPlayers.Count(), Is.EqualTo(4));
            }
        }
    }
}
