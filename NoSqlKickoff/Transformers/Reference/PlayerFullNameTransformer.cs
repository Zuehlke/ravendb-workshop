using System.Linq;

using NoSqlKickoff.Model.Reference;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Reference
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
                                                         FullName = string.Format("{0} {1}", player.FirstName, player.LastName)
                                                     };
        }
    }
}
