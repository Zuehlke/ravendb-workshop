using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Team Map Reduce Index for E08_EmploymentCopyEmbeddedInPlayerAndTeam
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-reduce-indexes
    /// </remarks>
    /// <see cref="Player_Index_R13"/>
    public class E08_TeamMapReduceIndex : AbstractIndexCreationTask<Team, E08_TeamMapReduceIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public ReducedPlayer[] Players { get; set; }

            public string TeamName { get; set; }

            public string Season { get; set; }

            public string TeamId { get; set; }
        }

        public class ReducedPlayer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public E08_TeamMapReduceIndex()
        {
            // TODO: implement map property
            Map = teams => from team in teams
                           from employment in team.EmploymentCopies
                           select new IndexEntry
                                        {
                                            TeamId = team.Id,
                                            TeamName = team.Name,
                                            Season = employment.Season,
                                            Players = new[] { new ReducedPlayer { FirstName = employment.FirstName, LastName = employment.LastName } }
                                        };
            
            Reduce = entries => from entry in entries
                                group entry by new { entry.TeamId, entry.Season } into g
                                select
                                    new IndexEntry
                                        {
                                            TeamId = g.Key.TeamId,
                                            TeamName = g.First().TeamName,
                                            Season = g.Key.Season,
                                            Players = g.SelectMany(e => e.Players).ToArray()
                                        };
        }
    }
}
