using System.Linq;

using NoSqlKickoff.Model;

using NUnit.Framework;

using Raven.Client;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests
{
    [TestFixture]
    public class UC_01_StoreAndLoad : RavenTestBase
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
            Player player = DataGenerator.CreatePlayer();
            
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

        [Test]
        public void StoreAndLoadMultiple()
        {
            var players = DataGenerator.CreatePlayerList();

            using (var session = _store.OpenSession())
            {
                foreach (var player in players)
                {
                    session.Store(player);    
                }

                session.SaveChanges();
            }

            Player[] loadedPlayers;
            using (var session = _store.OpenSession())
            {
                loadedPlayers = session.Load<Player>(players.Select(p => p.Id).ToArray());
            }

            CollectionAssert.AreEquivalent(
                players.Select(player => player.LastName),
                loadedPlayers.Select(player => player.LastName));
        }
    }
}
