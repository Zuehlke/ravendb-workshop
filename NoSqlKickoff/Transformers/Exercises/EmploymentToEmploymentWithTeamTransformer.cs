using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Exercises
{
    public class EmploymentToEmploymentWithTeamTransformer : AbstractTransformerCreationTask<Employment>
    {
        public EmploymentToEmploymentWithTeamTransformer()
        {
            TransformResults = employments => from employment in employments
                                              let team = LoadDocument<Team>(employment.TeamId)
                                              select new EmploymentWithTeam
                                                  {
                                                      TeamId = team.Id,
                                                      Season = employment.Season,
                                                      TeamName = team.Name,
                                                      Id = employment.Id
                                                  };
        }
    }
}
