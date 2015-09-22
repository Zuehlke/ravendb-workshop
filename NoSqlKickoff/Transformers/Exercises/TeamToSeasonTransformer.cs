using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Exercises
{
    public class TeamToSeasonTransformer : AbstractTransformerCreationTask<Team>
    {
        public TeamToSeasonTransformer()
        {
            // TODO: Implement the TransformResults property
            // HINT: teams => from team in teams...
            // HINT: Parameter()
        }
    }
}
