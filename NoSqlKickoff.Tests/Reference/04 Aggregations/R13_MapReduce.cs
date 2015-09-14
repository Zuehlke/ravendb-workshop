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
    public class R13_MapReduce : RavenTestBase
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
            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

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
        public void QueryMapReduceIndex()
        {
            using (var session = _store.OpenSession())
            {
                var countriesWithMoreThan4Players = session.Query<Player_Index_R13.IndexEntry, Player_Index_R13>()
                    .Where(e => e.NumberOfPlayers > 4)
                    .ToList();

                Assert.That(countriesWithMoreThan4Players.Count(), Is.EqualTo(1));
            }
        }
    }
}
