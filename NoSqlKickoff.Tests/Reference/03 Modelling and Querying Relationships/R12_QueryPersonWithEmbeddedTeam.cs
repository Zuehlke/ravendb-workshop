using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.Reference
{
    /// <summary>
    /// Use Case: Get index entries directly
    /// Goal: Query an index and get its values as object, totally ignoring the document store
    /// </summary>
    public class R12_QueryPersonWithEmbeddedTeam : RavenTestBase
    {
        private IDocumentStore _store;

        private List<Team> _teams;

        private List<PlayerWithTeam> _players;

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

                _players = DataGenerator.CreatePlayerListWithTeams();

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
        public void QueryAllPersonsThatPlayInTheirHomeCountry()
        {
            using (var session = _store.OpenSession())
            {
                // Not possible to compare on index fields !!
                // var playersThatPlayInTheirHomeCountry = session.Query<Player_Index_UC13.IndexEntry, Player_Index_UC13>()
                //    .Where(p => p.TeamCountryName == p.Nationality)
                //    .ToList();

                var playersThatPlayInTheirHomeCountry = session.Query<Player_Index_R12.IndexEntry, Player_Index_R12>()
                    .Where(p => p.PlaysInHisHomeCountry)
                    .OfType<PlayerWithTeam>()
                    .ToList();

                Assert.That(playersThatPlayInTheirHomeCountry.Count(), Is.EqualTo(47));
            }
        }
    }
}
