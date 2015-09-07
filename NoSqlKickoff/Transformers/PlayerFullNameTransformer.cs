using System.Linq;

using NoSqlKickoff.Model;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers
{
    public class PlayerFullNameTransformer : AbstractTransformerCreationTask<Player>
    {
        public PlayerFullNameTransformer()
        {
            TransformResults = players => from player in players 
                                          select new PlayerWithFullName
                                                     {
                                                         TeamId = player.TeamId,
                                                         FirstName = player.FirstName,
                                                         LastName = player.LastName,
                                                         Nationality = player.Nationality,
                                                         Id = player.Id,
                                                         MiddleName = player.MiddleName,
                                                         DateOfBirth = player.DateOfBirth,
                                                         FullName = string.Format("{0} {1}", player.FirstName, player.LastName)
                                                     };
        }
    }
}
