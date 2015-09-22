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

        /// <summary>
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        /// <returns>
        /// A list of ReducedPlayer objects (First name, Last name) who have played in Dortmund during 13/14
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-use-transformers-in-queries
        /// http://ravendb.net/docs/article-page/3.0/csharp/transformers/passing-parameters
        /// </remarks>
        /// <see cref="R07_SimpleTransformer"/>
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

        /// <summary>
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        /// <returns>
        /// A list of ReducedPlayer objects (First name, Last name) who have played in Dortmund during 13/14
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/fanout-indexes
        /// </remarks>
        /// <see cref="R05_ProjectFromIndexFieldsInto"/>
        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingTeamFanOutIndex()
        {
            using (var session = _store.OpenSession())
            {
                var playersIn1314 = session.Query<E08_TeamFanOutIndex.IndexEntry, E08_TeamFanOutIndex>()
                    .Where(t => t.TeamName == "Borussia Dortmund" && t.Season == "2013-2014")
                    .ProjectFromIndexFieldsInto<ReducedPlayer>()
                    .ToList();

                return playersIn1314;
            }
        }

        /// <summary>
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        /// <returns>
        /// A list of ReducedPlayer objects (First name, Last name) who have played in Dortmund during 13/14
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/fanout-indexes
        /// </remarks>
        /// <see cref="R05_ProjectFromIndexFieldsInto"/>
        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingPlayerFanOutIndex()
        {
            using (var session = _store.OpenSession())
            {
                var playersIn1314 = session.Query<E08_PlayerFanOutIndex.IndexEntry, E08_PlayerFanOutIndex>()
                    .Where(t => t.TeamName == "Borussia Dortmund" && t.Season == "2013-2014")
                    .ProjectFromIndexFieldsInto<ReducedPlayer>()
                    .ToList();

                return playersIn1314;
            }
        }

        /// <summary>
        /// As a user I want to find all employments of “Gonzalo Higuaín”
        /// </summary>
        /// <returns>
        /// A list of EmploymentCopyInPlayer objects from Higuain
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-perform-projection#oftype-(as)---simple-projection
        /// </remarks>
        /// <see cref="R04_Querying_TypeCoercion"/>
        public List<EmploymentCopyInPlayer> FindEmploymentsOfHiguain()
        {
            using (var session = _store.OpenSession())
            {
                var higuain = session.Query<E08_PlayerIndex.IndexEntry, E08_PlayerIndex>()
                                .Where(x => x.FirstName == "Gonzalo" && x.LastName == "Higuaín")
                                .OfType<Player>()
                                .Single();

                return higuain.EmploymentCopies.ToList();
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

        [Test]
        public void FindPlayersOfDortmundIn1314_UsingTransformer_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayersOfDortmundIn1314_UsingTransformer();

            players.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(players.Count, Is.EqualTo(4));
        }

        [Test]
        public void FindPlayersOfDortmundIn1314_UsingTeamFanOutIndex_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayersOfDortmundIn1314_UsingTeamFanOutIndex();

            players.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(players.Count, Is.EqualTo(4));
        }

        [Test]
        public void FindPlayersOfDortmundIn1314_UsingPlayerFanOutIndex_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayersOfDortmundIn1314_UsingPlayerFanOutIndex();

            players.PrintDump();

            WaitForUserToContinueTheTest(_store);

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