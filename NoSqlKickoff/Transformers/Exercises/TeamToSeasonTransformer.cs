using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Transformers.Exercises
{
    public class TeamToSeasonTransformer : AbstractTransformerCreationTask<Team>
    {
        public TeamToSeasonTransformer()
        {
            TransformResults = teams => from team in teams
                                        select
                                            new Team
                                                {
                                                    Id = team.Id,
                                                    Name = team.Name,
                                                    Country = team.Country,
                                                    EmploymentCopies =
                                                        team.EmploymentCopies.Where(
                                                            e => e.Season == Parameter("season").Value<string>())
                                                        .ToList()
                                                };
        }
    }
}
