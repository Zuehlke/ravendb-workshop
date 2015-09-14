using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    /// <summary>
    /// Use Case: Get index entries directly
    /// Goal: Query an index and get its values as object, totally ignoring the document store
    /// </summary>
    public class R06_Select : RavenTestBase
    {
        private IDocumentStore _store;

        private List<Player> _players;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            // We first have to create the static indexes
            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

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

            // Let's wait for indexing to happen
            // this method is part of RavenTestBase and thus should only be used in tests
            WaitForIndexing(_store);
        }

        [Test]
        public void SimpleProjection_WithSelect()
        {
            using (var session = _store.OpenSession())
            {
                var filteredResults = session.Query<Player, Player_Index_R06>()
                     .Where(p => p.FirstName.StartsWith("C"))
                     .Select(p => p.LastName)
                     .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(1));
                Assert.That(filteredResults.Single(), Is.EqualTo("Ronaldo"));
            }
        }

        [Test]
        public void SimpleProjection_WithSelectIntoDifferentType()
        {
            using (var session = _store.OpenSession())
            {
                // Select works only on the Document and not on the index Entry
                // Use ProjectFromIndexFieldsInto instead.
                var filteredResults = session.Query<Player_Index_R06.IndexEntry, Player_Index_R06>()
                     .Where(p => p.FirstName.StartsWith("C"))
                     .OfType<Player>()
                     .Select(p => new
                                      {
                                        p.LastName,
                                        p.FirstName
                                      })
                     .ToList()
                     .Select(p => new PlayerWithFullName
                                      {
                                          FullName = p.FirstName + " " + p.LastName
                                      })
                    .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(1));
                Assert.That(filteredResults.Single().FullName, Is.EqualTo("Christiano Ronaldo"));
            }
        }
    }
}
