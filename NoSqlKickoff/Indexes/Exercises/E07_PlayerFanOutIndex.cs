﻿using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E07_PlayerFanOutIndex : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public string Season { get; set; }
        }

        public E07_PlayerFanOutIndex()
        {
            // TODO: implement map property
            Map = players => from player in players
                             from employment in Recurse(player, p => p.Employments)
                             select new IndexEntry
                                        {
                                            TeamName = employment.TeamName, 
                                            Season = employment.Season
                                        };

            // we need to configure the fan-out maximum
            MaxIndexOutputsPerDocument = 30;
        }
    }
}
