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
    public class UC_08_Include : RavenTestBase
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
        public void GetPlayerWithTeam_NaiveApproach()
        {
            using (var session = _store.OpenSession())
            {
                var allPlayers = session.Query<Player>().ToList();

                foreach (var player in allPlayers)
                {
                    // This causes a network request for each team --> exception if too many operations per session
                    var team = session.Load<Team>(player.TeamId);
                    player.TeamNavigationProperty = team;
                }

                Assert.IsTrue(allPlayers.All(p => p.TeamNavigationProperty != null));
            }
        }
        
        [Test]
        public void GetPlayerWithTeam_UsingInclude()
        {
            // Include is loading all mentioned documents into the session using the same network request

            using (var session = _store.OpenSession())
            {
                var allPlayers = session.Query<Player>()
                    .Include(p => p.TeamId)
                    .ToList();

                // Let's get all teams of the requested players
                // The call to session load is only loading from the session, not from the server
                var allTeams = allPlayers
                    .Select(p => session.Load<Team>(p.TeamId))
                    .DistinctBy(t => t.Id)
                    .ToDictionary(t => t.Id);

                // Assign the team to the navigation property of the player
                foreach (var player in allPlayers)
                {
                    player.TeamNavigationProperty = allTeams[player.TeamId];
                }

                Assert.IsTrue(allPlayers.All(p => p.TeamNavigationProperty != null));
            }
        }
    }
}
