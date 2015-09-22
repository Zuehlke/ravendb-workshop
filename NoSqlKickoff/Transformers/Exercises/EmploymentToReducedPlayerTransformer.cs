using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Exercises
{
    public class EmploymentToReducedPlayerTransformer : AbstractTransformerCreationTask<Employment>
    {
        public EmploymentToReducedPlayerTransformer()
        {
            TransformResults = employments => from employment in employments
                                              let player = LoadDocument<Player>(employment.PlayerId)
                                              select new ReducedPlayer
                                                      {
                                                          FirstName = player.FirstName,
                                                          LastName = player.LastName
                                                      };
        }
    }
}
