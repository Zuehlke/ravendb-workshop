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
    public class UC_11_LoadDocumentWithStoreFields : RavenTestBase
    {
        private IDocumentStore _store;

        private List<Player> _players;

        private List<Team> _teams;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            // We first have to create the static indexes
            IndexCreation.CreateIndexes(typeof(Player_Index_UC03).Assembly, _store);

            _teams = DataGenerator.CreateTeamList();

            // Store some players and teams in the database
            using (var session = _store.OpenSession())
            {
                foreach (var team in _teams)
                {
                    session.Store(team);
                }

                _players = DataGenerator.CreatePlayerListWithTeamIds(_teams);

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
        public void LoadDocumentWithStoreFields()
        {
            using (var session = _store.OpenSession())
            {
                var playersOfItalianTeams = session.Query<Player_Index_UC11.IndexEntry, Player_Index_UC11>()
                    .Where(p => p.CountryOfTeam == "Italy")
                    .ProjectFromIndexFieldsInto<PlayerWithTeam>()
                    .ToList();

                Assert.That(playersOfItalianTeams.Count(), Is.EqualTo(4));
                Assert.IsTrue(playersOfItalianTeams.All(p => p.Team != null));
            }
        }
    }
}
