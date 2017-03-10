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
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E06_EmploymentInSeparateCollection : RavenTestBase
    {
        private IDocumentStore _store;
        
        /// <summary>
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        /// <returns>
        /// A list of ReducedPlayer objects (First name, Last name) who have played in Dortmund during 13/14
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-use-transformers-in-queries
        /// </remarks>
        /// <see cref="R07_SimpleTransformer"/>
        /// <see cref="R09_LoadDocumentInIndex"/>
        /// <see cref="R10_LoadDocumentInTransformer"/>
        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingJoinAndTransformer()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E06_EmploymentIndex.IndexEntry, E06_EmploymentIndex>()
                    .Where(x => x.Season == "2013-2014" && x.TeamName == "Borussia Dortmund")
                    .TransformWith<EmploymentToReducedPlayerTransformer, ReducedPlayer>().ToList();
            }
        }

        /// <summary>
        /// As a user I want to know what players have been employed by "Borussia Dortmund" in season "2013-2014".
        /// </summary>
        /// <returns>
        /// A list of ReducedPlayer objects (First name, Last name) who have played in Dortmund during 13/14
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-perform-projection#projectfromindexfieldsinto
        /// </remarks>
        /// <see cref="R05_ProjectFromIndexFieldsInto"/>
        /// <see cref="R09_LoadDocumentInIndex"/>
        /// <see cref="R11_LoadDocumentWithStoreFields"/>
        public List<ReducedPlayer> FindPlayersOfDortmundIn1314_UsingJoinAndIndexStore()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<E06_EmploymentIndexWithStore.IndexEntry, E06_EmploymentIndexWithStore>()
                        .Where(x => x.Season == "2013-2014" && x.TeamName == "Borussia Dortmund")
                        .ProjectFromIndexFieldsInto<ReducedPlayer>()
                        .ToList();
            }
        }

        /// <summary>
        /// TODO: Exercise 12a (I)
        /// As a user I want to find all employments of “Gonzalo Higuaín”
        /// </summary>
        /// <returns>
        /// A list of EmploymentWithTeam objects from Higuain
        /// </returns>
        /// <remarks>
        /// http://ravendb.net/docs/article-page/3.0/csharp/client-api/session/querying/how-to-use-transformers-in-queries
        /// </remarks>
        /// <see cref="R07_SimpleTransformer"/>
        /// <see cref="R09_LoadDocumentInIndex"/>
        /// <see cref="R10_LoadDocumentInTransformer"/>
        public List<EmploymentWithTeam> FindEmploymentsOfHiguain_UsingJoinAndTransformer()
        {
            // HINT: Query()
            // HINT: TransformWith()

            throw new NotImplementedException();
        }
        
        [Test]
        public void FindPlayersOfDortmundIn1314_UsingJoinAndTransformer_ShouldReturnAllPlayersOfDortmundIn1314()
        {
            var playerEmployments = FindPlayersOfDortmundIn1314_UsingJoinAndTransformer();

            playerEmployments.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(playerEmployments.Count, Is.EqualTo(4));
        }

        [Test]
        public void FindPlayersOfDortmundIn1314_UsingJoinAndIndexStore_ShouldReturnAllPlayersOfDortmundIn1314()
        {
            var playerEmployments = FindPlayersOfDortmundIn1314_UsingJoinAndIndexStore();

            playerEmployments.PrintDump();

            WaitForUserToContinueTheTest(_store);

            Assert.That(playerEmployments.Count, Is.EqualTo(4));
        }

        [Test]
        public void FindEmploymentsOfHiguain_UsingJoinAndTransformer_ShouldReturnAllEmploymentsOfHiguain()
        {
            var employments = FindEmploymentsOfHiguain_UsingJoinAndTransformer();

            employments.PrintDump();

            Assert.That(employments.Count, Is.EqualTo(19));
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
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
