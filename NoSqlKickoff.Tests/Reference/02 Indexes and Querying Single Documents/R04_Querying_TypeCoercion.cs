using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model.Reference;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.Reference
{
    /// <summary>
    /// Use Case: Query result type coercion
    /// Goal: Query properties of the index and still get Player objects back
    ///
    /// Index Entry Property Names and Document Property Names have to match exactly, otherwise
    //  either the compiler complains, it will throw at runtime or you won't get any results
    //  Best Solution: use type coercion
    /// </summary>
    public class R04_Querying_TypeCoercion : RavenTestBase
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
        public void TypeCoercion_OnNewlyCreatedProperty()
        {
            using (var session = _store.OpenSession())
            {
                 //Not possible to query on Name, because Player does not have a Name property
                 //var filteredResults = session.Query<Player, Player_Index_UC04>()
                 //   .Where(p => p.Name.StartsWith("C"))
                 //   .ToList();

                // Instead we have to query on the Index Entry and then coerce the type to Player using special operators

                // Option 1: As<T>
                //var filteredResults = session.Query<Player_Index_UC04.IndexEntry, Player_Index_UC04>()
                //.Where(p => p.Name.StartsWith("C"))
                //.As<Player>()
                //.ToList();

                // Option 2: OfType<T>
                var filteredResults = session.Query<Player_Index_R04.IndexEntry, Player_Index_R04>()
                     .Where(p => p.Name.StartsWith("C"))
                     .OfType<Player>()
                     .ToList();
                
                Assert.That(filteredResults.Count(), Is.EqualTo(5));
            }
        }

        [Test]
        public void TypeCoerction_OnRenamedProperty()
        {
            using (var session = _store.OpenSession())
            {
                // Not possible to use Player as Result Type, since it does not have the property "FirstNameRenamed"
                // var filteredResults = session.Query<Player, Player_Index_UC04>()
                //    .Where(p => p.FirstNameRenamed.StartsWith("C"))
                //    .ToList();

                // Solution: Type coercion
                var filteredResults = session.Query<Player_Index_R04.IndexEntry, Player_Index_R04>()
                     .Where(p => p.FirstNameRenamed.StartsWith("C"))
                     .OfType<Player>()
                     .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(5));
            }
        }

        [Test]
        public void NestedProperties_WithoutTypeCoercion()
        {
            using (var session = _store.OpenSession())
            {
                var filteredResults = session.Query<Player, Player_Index_R04>()
                    .Where(p => p.Nationality.Name == "Germany")
                    .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(17));
            }
        }

        [Test]
        public void TypeCoerction_WithNestedProperties()
        {
            using (var session = _store.OpenSession())
            {
                var filteredResults = session.Query<Player_Index_R04.IndexEntry, Player_Index_R04>()
                    .Where(p => p.Nationality == "Germany")
                    .OfType<Player>()
                    .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(17));
            }
        }
    }
}
