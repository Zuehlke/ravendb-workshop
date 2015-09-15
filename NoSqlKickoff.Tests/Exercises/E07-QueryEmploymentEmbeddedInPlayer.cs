using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using NUnit.Framework;

using ServiceStack.Text;

namespace NoSqlKickoff.Tests.Exercises
{
    class E07_QueryEmploymentEmbeddedInPlayer
    {

        /// <summary>
        /// Exercise 8: As a user I want to know what players had been employed by Borussia Dortmund for the season "2013-2014".
        /// TODO: Embed employment soley in player
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingEmploymentSoleyInPlayer()
        {
            return new List<Player>();
        }

        /// <summary>
        /// Exercise 9: As a user I want to know what players had been employed by Borussia Dortmund for the season "2013-2014".
        /// TODO: Embed employment infos partially in player
        /// </summary>
        public List<Player> FindPlayerEmploymentsUsingEmploymentPartiallyInPlayer()
        {
            return new List<Player>();
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

    }
}
