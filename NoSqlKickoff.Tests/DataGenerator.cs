using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model;

namespace NoSqlKickoff.Tests
{
    public class DataGenerator
    {
        public static PlayerGenerator CreatePlayer()
        {
            return new PlayerGenerator();
        }

        public static List<Player> CreatePlayerList()
        {
            return PlayerGenerator.GetPlayers();
        }

        public static List<Player> CreatePlayerListWithTeamIds(List<Team> teams)
        {
            return PlayerGenerator.GetPlayersWithTeamIds(teams).ToList();
        }

        public static List<Team> CreateTeamList()
        {
            return TeamGenerator.GetTeams();
        }
    }

    public class TeamGenerator
    {
        private static readonly List<Team> Teams = new List<Team>
        {
            new Team { Name = "Real Madrid", Country = "Spain" },
            new Team { Name = "Bayern München", Country = "Germany" },
            new Team { Name = "FC Barcelona", Country = "Spain" },
            new Team { Name = "Borussia Dortmund", Country = "Germany" },
            new Team { Name = "Manchester United", Country = "England" },
            new Team { Name = "FC Liverpool", Country = "England" },
            new Team { Name = "FC Santos", Country = "Brazil" },
            new Team { Name = "SSC Neapel", Country = "Italy" },
            new Team { Name = "AC Milan", Country = "Italy" },
            new Team { Name = "Juventus Turin", Country = "Italy" },
            new Team { Name = "AS Rom", Country = "Italy" },
            new Team { Name = "FC Chelsea", Country = "England" },
            new Team { Name = "Ajax Amsterdam", Country = "Netherlands" },
        };

        public static List<Team> GetTeams()
        {
            return Teams;
        }
    }

    public class PlayerGenerator
    {
        #region Player Data

        private static readonly List<Player> PlayersFromBayern = new List<Player>
        {
            new Player { FirstName = "Bastian", LastName = "Schweinsteiger", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Manuel", LastName = "Neuer", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Philipp", LastName = "Lahm", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Lothar", LastName = "Matthäus", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Franz", LastName = "Beckenbauer", Nationality = new Nationality { Name = "Germany" }}
        };

        private static readonly List<Player> PlayersFromReal = new List<Player>
        {
            new Player { FirstName = "Christiano", LastName = "Ronaldo", Nationality = new Nationality { Name = "Portugal" }},
            new Player { FirstName = "Alfredo", LastName = "di Stefano", Nationality = new Nationality { Name = "Argentina" }},
            new Player { FirstName = "Ferenc", LastName = "Puskas", Nationality = new Nationality { Name = "Hungary" }}
        };

        private static readonly List<Player> PlayersFromBarcelona = new List<Player>
        {
            new Player { FirstName = "Lionel", LastName = "Messi", Nationality = new Nationality { Name = "Argentina" }}
        };

        private static readonly List<Player> PlayersFromMilan = new List<Player>
        {
            new Player { FirstName = "Paolo", LastName = "Maldini", Nationality = new Nationality { Name = "Italy" }},
        };

        private static readonly List<Player> PlayersFromJuve = new List<Player>
        {
            new Player { FirstName = "Zinedine", LastName = "Zidane", Nationality = new Nationality { Name = "France" }},
        };

        private static readonly List<Player> PlayersFromSantos = new List<Player>
        {
            new Player { FirstName = "Pele", LastName = "", Nationality = new Nationality { Name = "Brazil" }}
        };

        private static readonly List<Player> PlayersFromRoma = new List<Player>
        {
            new Player { FirstName = "Rudi", LastName = "Völler", Nationality = new Nationality { Name = "Germany" }},
        };

        private static readonly List<Player> PlayersFromDortmund = new List<Player>
        {
            new Player { FirstName = "Stephane", LastName = "Chapuisat", Nationality = new Nationality { Name = "Switzerland" }},
        };

        private static readonly List<Player> PlayersFromAjax = new List<Player> 
        { 
            new Player { FirstName = "Johann", LastName = "Cruyff", Nationality = new Nationality { Name = "Netherlands" }},
        };

        private static readonly List<Player> PlayersFromManchester = new List<Player>
        {
            new Player { FirstName = "George", LastName = "Best", Nationality = new Nationality { Name = "North Ireland" }},
        };

        private static readonly List<Player> PlayersFromLiverpool = new List<Player>
        {
            new Player { FirstName = "Steven", LastName = "Gerrard", Nationality = new Nationality { Name = "English" }},
        };

        private static readonly List<Player> PlayersFromChelsea = new List<Player>
        { 
            new Player { FirstName = "Frank", LastName = "Lampard", Nationality = new Nationality { Name = "English" }},
        };

        private static readonly List<Player> PlayersFromNapoli = new List<Player>
        {
            new Player { FirstName = "Diego", LastName = "Maradona", Nationality = new Nationality { Name = "Argentina" }},
        };

        private static readonly Dictionary<string, List<Player>> TeamDictionary = new Dictionary<string, List<Player>>
        {
            {"Real Madrid", PlayersFromReal},
            {"Bayern München", PlayersFromBayern},
            {"FC Barcelona", PlayersFromBarcelona},
            {"Borussia Dortmund", PlayersFromDortmund},
            {"Manchester United", PlayersFromManchester},
            {"FC Liverpool", PlayersFromLiverpool},
            {"FC Santos", PlayersFromSantos},
            {"SSC Neapel", PlayersFromNapoli},
            {"AC Milan", PlayersFromMilan},
            {"Juventus Turin", PlayersFromJuve},
            {"AS Rom", PlayersFromRoma},
            {"FC Chelsea", PlayersFromChelsea},
            {"Ajax Amsterdam", PlayersFromAjax}
        };

        #endregion

        public static List<Player> GetPlayers()
        {
            return
                PlayersFromAjax
                    .Concat(PlayersFromBayern)
                    .Concat(PlayersFromBarcelona)
                    .Concat(PlayersFromChelsea)
                    .Concat(PlayersFromDortmund)
                    .Concat(PlayersFromJuve)
                    .Concat(PlayersFromLiverpool)
                    .Concat(PlayersFromManchester)
                    .Concat(PlayersFromMilan)
                    .Concat(PlayersFromNapoli)
                    .Concat(PlayersFromReal)
                    .Concat(PlayersFromRoma)
                    .Concat(PlayersFromSantos)
                    .ToList();

        }

        public static IEnumerable<Player> GetPlayersWithTeamIds(List<Team> teams)
        {
            foreach (var team in teams)
            {
                var playersOfTeam = TeamDictionary[team.Name];
                foreach (var player in playersOfTeam)
                {
                    player.TeamId = team.Id;
                    yield return player;
                }
            }
        }

        private string _firstName;
        private string _lastName;

        public PlayerGenerator()
        {
            var players = GetPlayers();
            var random = new Random();
            var nextRandom = random.Next(players.Count - 1);

            var randomPlayer = players[nextRandom];

            _firstName = randomPlayer.FirstName;
            _lastName = randomPlayer.LastName;
        }

        public static implicit operator Player(PlayerGenerator playerGenerator)
        {
            return new Player
                       {
                           FirstName = playerGenerator._firstName,
                           LastName = playerGenerator._lastName,
                       };
        }

        public PlayerGenerator WithName(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            return this;
        }
    }
}
