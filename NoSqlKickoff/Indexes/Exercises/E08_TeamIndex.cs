using System.Linq;

using NoSqlKickoff.Model.Exercises;

using Raven.Client.Indexes;

namespace NoSqlKickoff.Indexes.Exercises
{
    public class E08_TeamIndex : AbstractIndexCreationTask<Team>
    {
        public E08_TeamIndex()
        {
            // TODO: implement map property
            Map = teams => from team in teams
                             select new 
                                        {
                                            team.Name
                                        };
        }
    }
}
