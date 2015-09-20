using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model.Exercises;
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
        /// TODO: Exercise 11c
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingInMemoryFilter()
        {
            using (var session = _store.OpenSession())
            {
                var dortmund = session.Query<Team, E08_TeamIndex>().Single(t => t.Name == "Borussia Dortmund");

                var playersIn1314 = dortmund.EmploymentCopies.Where(e => e.Season == "2013-2014").Select(e => new ReducedPlayer { FirstName = e.FirstName, LastName = e.LastName}).ToList();

                return playersIn1314;
            }
        }

        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingTransformer()
        {
            using (var session = _store.OpenSession())
            {
                var dortmund = session.Query<Team, E08_TeamIndex>()
                    .Where(t => t.Name == "Borussia Dortmund")
                    .TransformWith<TeamToSeasonTransformer, Team>()
                    .AddTransformerParameter("season", RavenJToken.FromObject("2013-2014"))
                    .Single();

                var playersIn1314 = dortmund.EmploymentCopies.Select(e => new ReducedPlayer { FirstName = e.FirstName, LastName = e.LastName }).ToList();

                return playersIn1314;
            }
        }

        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingFanOutIndex()
        {
            using (var session = _store.OpenSession())
            {
                var playersIn1314 = session.Query<E08_TeamIndex2.IndexEntry, E08_TeamIndex2>()
                    .Where(t => t.TeamName == "Borussia Dortmund" && t.Season == "2013-2014")
                    .ProjectFromIndexFieldsInto<ReducedPlayer>()
                    .ToList();

                return playersIn1314;
            }
        }

        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingMapReduceIndex()
        {
            using (var session = _store.OpenSession())
            {
                var dortmundIn1314 = session.Query<E08_TeamIndex3.IndexEntry, E08_TeamIndex3>()
                        .Single(t => t.TeamName == "Borussia Dortmund" && t.Season == "2013-2014");

                return dortmundIn1314.Players.Select(p => new ReducedPlayer { FirstName = p.FirstName, LastName = p.LastName}).ToList();
            }
        }

        // 4 Possibilities: In-memory cut out, Transformer cut out, Fan-out index, Map-Reduce Index

        [Test]
        public void FindPlayersOfDortmundIn1314_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayersOfDortmundIn1314_UsingMapReduceIndex();

            players.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(players.Count, Is.EqualTo(4));
        }

        [SetUp]
        public void SetUp()
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
