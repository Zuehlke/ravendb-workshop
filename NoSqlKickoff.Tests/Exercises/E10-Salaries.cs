using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes;
using NoSqlKickoff.Indexes.Exercises;
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
            using (var session = _store.OpenSession())
            {
                var teamsWithAverageSalary = session.Query<E10_TeamWithAverageSalaryIndex.IndexEntry, E10_TeamWithAverageSalaryIndex>().ToList();

                return teamsWithAverageSalary;
            }
        }

        /// <summary>
        /// TODO: Exercise 16
        /// As a user I want to have a list of all nationalities with their average salary
        /// </summary>
        /// <returns>
        /// A list of all nationalities with their average salary
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-reduce-indexes
        /// </remarks>
        /// <see cref="R13_MapReduce"/>
        public List<E10_NationalityWithAverageSalaryIndex.IndexEntry> GetListOfNationalitiesWithAverageSalary()
        {
            using (var session = _store.OpenSession())
            {
                var nationalitiesWithSalary = session.Query<E10_NationalityWithAverageSalaryIndex.IndexEntry, E10_NationalityWithAverageSalaryIndex>().ToList();

                return nationalitiesWithSalary;
            }
        }

        /// <summary>
        /// TODO: Exercise 17
        /// As a user I want to have a list of all countries with their average salary
        /// </summary>
        /// <returns>
        /// A list of all countries with their average salary
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-reduce-indexes
        /// </remarks>
        /// <see cref="R13_MapReduce"/>
        public List<E10_CountryWithAverageSalaryIndex.IndexEntry> GetListOfCountriesWithAverageSalary()
        {
            using (var session = _store.OpenSession())
            {
                var countriesWithSalary = session.Query<E10_CountryWithAverageSalaryIndex.IndexEntry, E10_CountryWithAverageSalaryIndex>()
                        .ToList();

                return countriesWithSalary;
            }
        }

        [Test]
        public void GetListOfTeamsWithAverageSalary_ShouldReturnListOfTeamsWithAverageSalary()
        {
            var teamsWithAverageSalary = GetListOfTeamsWithAverageSalary();

            teamsWithAverageSalary.PrintDump();

            Assert.That(teamsWithAverageSalary.Count, Is.EqualTo(13));
            Assert.That(teamsWithAverageSalary.Single(t => t.TeamName == "Real Madrid").AverageSalary, Is.EqualTo(2000000));
        }

        [Test]
        public void GetListOfNationalitiesWithAverageSalary_ShouldReturnListOfNationalitiesWithAverageSalary()
        {
            var nationalitiesWithAverageSalary = GetListOfNationalitiesWithAverageSalary();

            nationalitiesWithAverageSalary.PrintDump();

            Assert.That(nationalitiesWithAverageSalary.Count, Is.EqualTo(25));
            Assert.That(nationalitiesWithAverageSalary.Single(t => t.Nationality == "Germany").AverageSalary, Is.GreaterThan(1000000));
        }

        [Test]
        public void GetListOfCountriesWithAverageSalary_ShouldReturnListOfCountriesWithAverageSalary()
        {
            var countriesWithAverageSalary = GetListOfCountriesWithAverageSalary();

            countriesWithAverageSalary.PrintDump();

            Assert.That(countriesWithAverageSalary.Count, Is.EqualTo(6));
            Assert.That(countriesWithAverageSalary.Single(t => t.Country == "Germany").AverageSalary, Is.GreaterThan(1000000));
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
