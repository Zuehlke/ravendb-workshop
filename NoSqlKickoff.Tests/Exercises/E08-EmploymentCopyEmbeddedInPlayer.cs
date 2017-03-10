using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;
using NoSqlKickoff.Tests.Reference;
using NoSqlKickoff.Transformers.Exercises;

using NUnit.Framework;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Json.Linq;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E08_EmploymentCopyEmbeddedInPlayerAndTeam : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        /// <returns>
        /// A list of ReducedPlayer objects (First name, Last name) who have played in Dortmund during 13/14
        /// </returns>
        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingInMemoryFilter()
        {
            using (var session = _store.OpenSession())
            {
                var dortmund = session.Query<Team, E08_TeamIndex>().Single(t => t.Name == "Borussia Dortmund");

                var playersIn1314 = dortmund.EmploymentCopies.Where(e => e.Season == "2013-2014").Select(e => new ReducedPlayer { FirstName = e.FirstName, LastName = e.LastName }).ToList();

                return playersIn1314;
            }
        }
        
        [Test]
        public void FindPlayersOfDortmundIn1314_UsingInMemoryFilter_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayersOfDortmundIn1314_UsingInMemoryFilter();

            players.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(players.Count, Is.EqualTo(4));
        }
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _store = NewDocumentStore();

            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

            var playerDictionary = DataGenerator.CreatePlayerList().ToDictionary(p => p.Id);
            var teamDictionary = DataGenerator.CreateTeamList().ToDictionary(p => p.Id);
            var employmentList = DataGenerator.CreateEmploymentList();

            foreach (var employment in employmentList)
            {
                var player = playerDictionary[employment.PlayerId];
                var team = teamDictionary[employment.TeamId];
                player.EmploymentCopies.Add(new EmploymentCopyInPlayer { Season = employment.Season, TeamName = team.Name, EmploymentId = employment.Id });
                team.EmploymentCopies.Add(new EmploymentCopyInTeam { Season = employment.Season, FirstName = player.FirstName, LastName = player.LastName, EmploymentId = employment.Id });
            }

            using (var bulkInsert = _store.BulkInsert(null, new BulkInsertOptions { OverwriteExisting = true }))
            {
                foreach (var player in playerDictionary.Values)
                {
                    bulkInsert.Store(player);
                }

                foreach (var team in teamDictionary.Values)
                {
                    bulkInsert.Store(team);
                }

                foreach (var employment in employmentList)
                {
                    bulkInsert.Store(employment);
                }
            }

            WaitForIndexing(_store);
        }
    }
}