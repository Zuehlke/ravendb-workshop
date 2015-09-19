using System.Linq;

using NoSqlKickoff.Model.Reference;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Reference
{
    public class PlayerWithTeamTransformer : AbstractTransformerCreationTask<Player>
    {
        public PlayerWithTeamTransformer()
        {
            TransformResults = players => from player in players
                                          let team = LoadDocument<Team>(player.TeamId)
                                          select new PlayerWithTeam
                                                     {
                                                         FirstName = player.FirstName,
                                                         Id = player.Id,
                                                         LastName = player.LastName,
                                                         Team = team,
                                                         Nationality = player.Nationality
                                                     };
        }
    }
}
