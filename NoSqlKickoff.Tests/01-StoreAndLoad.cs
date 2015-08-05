using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    [TestFixture]
    public class _01_StoreAndLoad : RavenTestBase
    {
        private IDocumentStore _store;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();
        }

        [Test]
        public void StoreAndLoad()
        {
            var player = new Player { FirstName = "Christiano", LastName = "Ronaldo" };

            using (var session = _store.OpenSession())
            {
                session.Store(player);
                session.SaveChanges();

                //player is tracked in session --> no call to Raven
                var samePlayer = session.Load<Player>(player.Id);
            }

            Player loadedPlayer;
            using (var session = _store.OpenSession())
            {
                loadedPlayer = session.Load<Player>(player.Id);
            }

            //Automatic ID Generation on Client with HiLo Algorithm
            //Id = players/1

            Assert.AreEqual(loadedPlayer.FirstName, player.FirstName);
        }

    }
}
