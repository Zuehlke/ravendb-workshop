using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlKickoff.Transformers
{
    using NoSqlKickoff.Model;

    using Raven.Client.Indexes;

    public class PlayerEmploymentTransformer : AbstractTransformerCreationTask<Employment>
    {
        public PlayerEmploymentTransformer()
        {
            TransformResults = employments => from employment in employments
                                              let player = LoadDocument<Player>(employment.PlayerId)
                                              select
                                                  new PlayerEmployment()
                                                      {
                                                          FirstName = player.FirstName,
                                                          LastName = player.LastName
                                                      };
        }
    }
}
