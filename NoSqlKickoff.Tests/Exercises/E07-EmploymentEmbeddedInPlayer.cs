using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model.Exercises;

using NUnit.Framework;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E07_EmploymentEmbeddedInPlayer : RavenTestBase
    {
        private IDocumentStore _store;
        
        /// <summary>
        /// TODO: Exercise 11b
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        public List<Player> FindPlayersOfDortmundIn1314()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E07_PlayerFanOutIndex.IndexEntry, E07_PlayerFanOutIndex>()
                    .Where(e => e.TeamName == "Borussia Dortmund" && e.Season == "2013-2014")
                    .OfType<Player>()
                    .ToList();
            }
        }

        /// <summary>
        /// TODO: Exercise 12a
        /// As a user I want to find all employments of “Gonzalo Higuaín”
        /// </summary>
        public List<Employment> FindEmploymentsOfHiguain()
        {
            using (var session = _store.OpenSession())
            {
                var higuain = session.Query<E07_PlayerIndex.IndexEntry, E07_PlayerIndex>()
                    .Where(x => x.FirstName == "Gonzalo" && x.LastName == "Higuaín")
                    .OfType<Player>()
                    .Single();

                return higuain.Employments;
            }
        }

        [Test]
        public void FindPlayersOfDortmundIn1314_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayersOfDortmundIn1314();

            WaitForUserToContinueTheTest(_store);

            players.PrintDump();

            Assert.That(players.Count, Is.EqualTo(4));
        }

        [Test]
        public void FindEmploymentsOfHiguain_ShouldReturnAllEmploymentsOfHiguain()
        {
            var employments = FindEmploymentsOfHiguain();

            employments.PrintDump();

            Assert.That(employments.Count, Is.EqualTo(19));
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _store = NewDocumentStore();

            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

            var playerDictionary = DataGenerator.CreatePlayerList().ToDictionary(p => p.Id);
            var teamDictionary = DataGenerator.CreateTeamList().ToDictionary(p => p.Id);
            var employmentList = DataGenerator.CreateEmploymentList();

            foreach (var employment in employmentList)
            {
                var team = teamDictionary[employment.TeamId];
                employment.TeamName = team.Name;
                playerDictionary[employment.PlayerId].Employments.Add(employment);
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
            }

            WaitForIndexing(_store);
        }
    }
}
