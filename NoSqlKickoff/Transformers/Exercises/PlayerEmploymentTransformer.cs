using System.Linq;

using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Exercises
{
    public class PlayerEmploymentTransformer : AbstractTransformerCreationTask<Employment>
    {
        public PlayerEmploymentTransformer()
        {
            TransformResults = employments => from employment in employments
                                              let player = LoadDocument<Player>(employment.PlayerId)
                                              select
                                                  new PlayerEmployment
                                                      {
                                                          FirstName = player.FirstName,
                                                          LastName = player.LastName
                                                      };
        }
    }
}
