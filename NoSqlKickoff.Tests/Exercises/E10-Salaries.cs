﻿using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Exercises;
using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Tests.Reference;

using NUnit.Framework;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E10_Salaries : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// TODO: Exercise 15
        /// As a user I want to have a list of all teams with their average salary
        /// </summary>
        /// <returns>
        /// A list of all teams with their average salary
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-reduce-indexes
        /// </remarks>
        /// <see cref="R13_MapReduce"/>
        public List<E10_TeamWithAverageSalaryIndex.IndexEntry> GetListOfTeamsWithAverageSalary()
        {
            // HINT: Query()

            throw new NotImplementedException();
        }

        [Test]
        public void GetListOfTeamsWithAverageSalary_ShouldReturnListOfTeamsWithAverageSalary()
        {
            var teamsWithAverageSalary = GetListOfTeamsWithAverageSalary();

            teamsWithAverageSalary.PrintDump();

            Assert.That(teamsWithAverageSalary.Count, Is.EqualTo(13));
            Assert.That(teamsWithAverageSalary.Single(t => t.TeamName == "Real Madrid").AverageSalary, Is.EqualTo(2000000));
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