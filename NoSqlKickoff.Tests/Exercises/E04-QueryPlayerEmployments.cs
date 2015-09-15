using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model;
using NoSqlKickoff.Transformers;

using NUnit.Framework;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E04_QueryPlayerEmployments : RavenTestBase
    {
        private IDocumentStore _store;
        
        /// <summary>
        /// Exercise 7: As a user I want to know what players had been employed by Borussia Dortmund for the season "2013-2014".
        /// TODO: Keep player, team and employment seperate!
        /// </summary>
        public List<PlayerEmployment> FindPlayerEmploymentsUsingJoin()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E04_EmploymentIndex.IndexEntry, E04_EmploymentIndex>()
                    .Where(x => x.Season == "2013-2014" && x.TeamName == "Borussia Dortmund")
                    .TransformWith<PlayerEmploymentTransformer, PlayerEmployment>().ToList();
            }
        }

        /// <summary>
        /// Exercise 8: As a user I want to know what players had been employed by Borussia Dortmund for the season "2013-2014".
        /// TODO: Embed employment soley in player
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingEmploymentSoleyInPlayer()
        {
            return new List<Player>();
        }

        /// <summary>
        /// Exercise 9: As a user I want to know what players had been employed by Borussia Dortmund for the season "2013-2014".
        /// TODO: Embed employment infos partially in player
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingEmploymentPartiallyInPlayer()
        {
            return new List<Player>();
        }

        [Test]
        public void FindPlayerEmploymentsUsingJoin_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var playerList = DataGenerator.CreatePlayerList();
            var teamList = DataGenerator.CreateTeamList();
            var employmentList = DataGenerator.CreateEmploymentList();

            using (var bulkInsert = _store.BulkInsert(null, new BulkInsertOptions { OverwriteExisting = true }))
            {
                foreach (var player in playerList)
                {
                    bulkInsert.Store(player);
                }

                foreach (var team in teamList)
                {
                    bulkInsert.Store(team);
                }

                foreach (var employment in employmentList)
                {
                    bulkInsert.Store(employment);
                }
            }

            WaitForIndexing(_store);

            var playerEmployments = FindPlayerEmploymentsUsingJoin();

            playerEmployments.PrintDump();

            Assert.That(playerEmployments.Count, Is.AtLeast(1));
        }

        [Test]
        public void FindPlayerEmploymentsUsingEmploymentSoleyInPlayer_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayerEmploymentsUsingEmploymentSoleyInPlayer();

            players.PrintDump();

            Assert.That(players.Count, Is.AtLeast(1));
        }

        [Test]
        public void FindPlayerEmploymentsUsingEmploymentPartiallyInPlayer_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayerEmploymentsUsingEmploymentSoleyInPlayer();

            players.PrintDump();

            Assert.That(players.Count, Is.AtLeast(1));
        }

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();

            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

            var players = DataGenerator.CreatePlayerList();

            using (var bulkInsert = _store.BulkInsert())
            {
                foreach (var player in players)
                {
                    bulkInsert.Store(player);
                }
            }
        }
    }
}
