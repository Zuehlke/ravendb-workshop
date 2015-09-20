using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.Exercises;

using NUnit.Framework;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E09_MapReduce : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 13
        /// As a user I want to have a list of all teams with the total number of players that ever played in each team
        /// </summary>
        public List<E09_TeamWithPlayerCountIndex.IndexEntry> GetListOfTeamsWithPlayerCount()
        {
            using (var session = _store.OpenSession())
            {
                var teamsWithCount = session.Query<E09_TeamWithPlayerCountIndex.IndexEntry, E09_TeamWithPlayerCountIndex>().ToList();

                return teamsWithCount;
            }
        }

        /// <summary>
        /// TODO: Exercise 14
        /// As a user I want to have the total number of players that ever played in "Real Madrid"
        /// </summary>
        public E09_TeamWithPlayerCountIndex.IndexEntry GetRealMadridWithPlayerCount()
        {
            using (var session = _store.OpenSession())
            {
                var real = session.Query<E09_TeamWithPlayerCountIndex.IndexEntry, E09_TeamWithPlayerCountIndex>()
                    .Single(e => e.TeamName == "Real Madrid");

                return real;
            }
        }

        [Test]
        public void GetListOfTeamsWithPlayerCount_ShouldReturnListOfTeamsWithPlayerCount()
        {
            var teamsWithCount = GetListOfTeamsWithPlayerCount();

            teamsWithCount.PrintDump();

            Assert.That(teamsWithCount.Count, Is.EqualTo(13));
            Assert.That(teamsWithCount.Single(t => t.TeamName == "Real Madrid").PlayerCount, Is.EqualTo(6));
        }

        [Test]
        public void GetRealMadridWithPlayerCount_ShouldReturnRealMadridWithPlayerCount()
        {
            var realMadridWithPlayerCount = GetRealMadridWithPlayerCount();

            realMadridWithPlayerCount.PrintDump();

            Assert.That(realMadridWithPlayerCount.PlayerCount, Is.EqualTo(6));
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _store = NewDocumentStore();

            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

            var playerDictionary = DataGenerator.CreatePlayerList().ToDictionary(p => p.Id);
            var teamDictionary = DataGenerator.CreateTeamList().ToDictionary(p => p.Id);
            var employmentList = DataGenerator.CreateEmploymentList();

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
