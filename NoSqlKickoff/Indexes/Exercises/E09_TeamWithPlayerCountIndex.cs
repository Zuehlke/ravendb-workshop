using System.Linq;

using NoSqlKickoff.Indexes.Reference;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    /// <summary>
    /// Team Map Reduce Index for E09_MapReduce
    /// </summary>
    /// <remarks>
    /// http://ravendb.net/docs/article-page/3.0/csharp/indexes/map-reduce-indexes
    /// </remarks>
    /// <see cref="Player_Index_R13"/>
    public class E09_TeamWithPlayerCountIndex : AbstractIndexCreationTask<Employment, E09_TeamWithPlayerCountIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public string TeamName { get; set; }

            public string[] PlayerIds { get; set; }

            public string TeamId { get; set; }

            public int PlayerCount { get; set; }
        }

        public E09_TeamWithPlayerCountIndex()
        {
            // TODO: implement map property
            Map = employments => from employment in employments
                                 let team = LoadDocument<Team>(employment.TeamId)
                                 select new IndexEntry
                                    {
                                        TeamId = employment.TeamId,
                                        PlayerIds = new[] { employment.PlayerId },
                                        TeamName = team.Name,
                                        PlayerCount = 0
                                    };


            Reduce = entries => from entry in entries
                                group entry by entry.TeamId into g
                                let playerIds = g.SelectMany(e => e.PlayerIds).Distinct().ToArray()
                                select new IndexEntry
                                        {
                                            TeamId = g.Key,
                                            TeamName = g.First().TeamName,
                                            PlayerCount = playerIds.Length,
                                            PlayerIds = playerIds
                                        };
        }
    }
}
