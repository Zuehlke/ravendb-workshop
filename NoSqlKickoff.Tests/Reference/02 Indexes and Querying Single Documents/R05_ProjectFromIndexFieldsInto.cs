using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Reference;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.Reference
{
    /// <summary>
    /// Use Case: Get index entries directly
    /// Goal: Query an index and get its values as object, partially filling a document from the index or 
    /// totally ignoring the document store
    /// </summary>
    public class R05_ProjectFromIndexFieldsInto : RavenTestBase
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
        public void GetStoredIndexFields()
        {
            using (var session = _store.OpenSession())
            {
                // Not possible to just get the Index Entry, because the Index specifies that it works on objects of type Player
                // var filteredResults = session.Query<Player_Index_UC05.IndexEntry, Player_Index_UC05>()
                //        .Where(p => p.Name.StartsWith("C"))
                //        .ToList();

                // Solution: ProjectFromIndexFieldsInto
                var filteredResults = session.Query<Player_Index_R05.IndexEntry, Player_Index_R05>()
                     .Where(p => p.FullName.StartsWith("C"))
                     .ProjectFromIndexFieldsInto<PlayerWithFullName>()
                     .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(5));
            }
        }

        // TODO: Example where only index entry is queried
    }
}
