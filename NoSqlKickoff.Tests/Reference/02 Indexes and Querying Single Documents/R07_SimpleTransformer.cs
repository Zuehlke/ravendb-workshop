﻿using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Reference;
using NoSqlKickoff.Transformers.Reference;

using NUnit.Framework;

using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Tests.Helpers;

namespace NoSqlKickoff.Tests.Reference
{
    public class R07_SimpleTransformer : RavenTestBase
    {
        private IDocumentStore _store;

        private List<Player> _players;

        [SetUp]
        public void SetUp()
        {
            _store = NewDocumentStore();
            _store.Initialize();

            // We first have to create the static indexes
            IndexCreation.CreateIndexes(typeof(Player_Index_R03).Assembly, _store);

            _players = DataGenerator.CreatePlayerList();

            // Store some players in the database
            using (var session = _store.OpenSession())
            {
                foreach (var player in _players)
                {
                    session.Store(player);
                }

                session.SaveChanges();
            }

            // Let's wait for indexing to happen
            // this method is part of RavenTestBase and thus should only be used in tests
            WaitForIndexing(_store);
        }

        [Test]
        public void SimpleTransformer()
        {
            using (var session = _store.OpenSession())
            {
                var filteredResults = session.Query<Player, Player_Index_R07>()
                     .Where(p => p.FirstName.StartsWith("C"))
                     .TransformWith<PlayerFullNameTransformer, PlayerWithFullName>()
                     .ToList();

                Assert.That(filteredResults.Count(), Is.EqualTo(5));
            }  
        }
    }
}
