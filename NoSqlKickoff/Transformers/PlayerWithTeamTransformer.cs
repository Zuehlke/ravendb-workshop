using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers
{
    public class PlayerWithTeamTransformer : AbstractTransformerCreationTask<Player>
    {
        public PlayerWithTeamTransformer()
        {
            TransformResults = players => from player in players
                                          let team = LoadDocument<Team>(player.TeamId)
                                          select new PlayerWithTeam
                                                     {
                                                         DateOfBirth = player.DateOfBirth,
                                                         FirstName = player.FirstName,
                                                         Id = player.Id,
                                                         LastName = player.LastName,
                                                         Team = team,
                                                         Nationality = player.Nationality,
                                                         MiddleName = player.MiddleName
                                                     };
        }
    }
}
