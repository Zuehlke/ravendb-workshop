﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Model.Exercises;
using NoSqlKickoff.Transformers.Exercises;

using NUnit.Framework;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E06_EmploymentInSeparateCollection : RavenTestBase
    {
        private IDocumentStore _store;
        
        /// <summary>
        /// TODO: Exercise 11a
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        public List<ReducedPlayer> FindPlayerEmploymentsUsingJoin()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E06_EmploymentIndex.IndexEntry, E06_EmploymentIndex>()
                    .Where(x => x.Season == "2013-2014" && x.TeamName == "Borussia Dortmund")
                    .TransformWith<EmploymentToReducedPlayerTransformer, ReducedPlayer>().ToList();
            }
        }

        public List<ReducedPlayer> FindPlayerEmploymentsUsingJoin2()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E06_EmploymentIndex2.IndexEntry, E06_EmploymentIndex2>()
                        .Where(x => x.Season == "2013-2014" && x.TeamName == "Borussia Dortmund")
                        .ProjectFromIndexFieldsInto<ReducedPlayer>()
                        .ToList();
            }
        }

        [Test]
        public void FindPlayerEmploymentsUsingJoin_ShouldReturnAllPlayersOfDortmundIn1314()
        {    
            var playerEmployments = FindPlayerEmploymentsUsingJoin2();

            playerEmployments.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(playerEmployments.Count, Is.EqualTo(4));
        }

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();

            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

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
        }
    }
}
