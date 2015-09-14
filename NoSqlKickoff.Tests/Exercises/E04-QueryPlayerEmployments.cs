using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    public class E04_QueryPlayerEmployments : RavenTestBase
    {
        private IDocumentStore _store;

        /// <summary>
        /// Exercise 7: As a user I want to know what players had been employed by Dortmund for the season "2013-2014".
        /// TODO: Keep player, team and employment seperate!
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingJoin()
        {
            return new List<Player>();
        }

        /// <summary>
        /// Exercise 8: As a user I want to know what players had been employed by Dortmund for the season "2013-2014".
        /// TODO: Embed employment soley in player
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingEmploymentSoleyInPlayer()
        {
            return new List<Player>();
        }

        /// <summary>
        /// Exercise 9: As a user I want to know what players had been employed by Dortmund for the season "2013-2014".
        /// TODO: Embed employment infos partially in player
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingEmploymentPartiallyInPlayer()
        {
            return new List<Player>();
        }

        [Test]
        public void FindPlayerEmploymentsUsingJoin_ShouldReturnAllPlayersOfDortmundIn20132014()
        {
            var players = FindPlayerEmploymentsUsingJoin();

            players.PrintDump();

            Assert.That(players.Count, Is.AtLeast(1));
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
