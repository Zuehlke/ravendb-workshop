using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Exercises
{
    public class EmploymentToEmploymentWithTeamTransformer : AbstractTransformerCreationTask<Employment>
    {
        public EmploymentToEmploymentWithTeamTransformer()
        {
            // TODO: Implement the TransformResults property
            // HINT: employments => from employment in employments...
            // HINT: LoadDocument()
        }
    }
}
