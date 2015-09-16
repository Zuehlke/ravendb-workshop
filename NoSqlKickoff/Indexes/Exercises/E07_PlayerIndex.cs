using System.Linq;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E07_PlayerIndex : AbstractIndexCreationTask<Player>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public string Season { get; set; }
        }

        public E07_PlayerIndex()
        {
            // TODO: implement map property
            Map = players => from player in players
                             from employment in Recurse(player, p => p.Employments)
                             let team = LoadDocument<Team>(employment.TeamId)
                             select new IndexEntry
                                        {
                                            TeamName = team.Name, 
                                            Season = employment.Season
                                        };

            MaxIndexOutputsPerDocument = 30;
        }
    }
}
