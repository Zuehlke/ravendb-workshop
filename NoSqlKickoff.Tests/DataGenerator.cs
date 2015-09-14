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

        public static List<PlayerWithTeam> CreatePlayerListWithTeams(List<Team> teams)
        {
            return PlayerGenerator.GetPlayerListWithTeams(teams).ToList();
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
            new Player { FirstName = "Iker", LastName = "Casillas", Nationality = new Nationality { Name = "Spain" }},
            new Player { FirstName = "Özil", LastName = "Casillas", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Ferenc", LastName = "Puskas", Nationality = new Nationality { Name = "Hungary" }}
        };

        private static readonly List<Player> PlayersFromBarcelona = new List<Player>
        {
            new Player { FirstName = "Lionel", LastName = "Messi", Nationality = new Nationality { Name = "Argentina" }},
            new Player { FirstName = "Jordi", LastName = "Alba", Nationality = new Nationality { Name = "Spain" }},
            new Player { FirstName = "Iniesta", LastName = "Andrés", Nationality = new Nationality { Name = "Spain" }},
            new Player { FirstName = "Puyol", LastName = "Carles", Nationality = new Nationality { Name = "Spain" }},
            new Player { FirstName = "Ivan", LastName = "Rakitic", Nationality = new Nationality { Name = "Croatia" }}
        };

        private static readonly List<Player> PlayersFromMilan = new List<Player>
        {
            new Player { FirstName = "Paolo", LastName = "Maldini", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Mario", LastName = "Balotelli", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Franco", LastName = "Baresi", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Clarence", LastName = "Seedorf", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { FirstName = "Kevin-Prince", LastName = "Boateng", Nationality = new Nationality { Name = "Ghana" }}
        };

        private static readonly List<Player> PlayersFromJuve = new List<Player>
        {
            new Player { FirstName = "Zinedine", LastName = "Zidane", Nationality = new Nationality { Name = "France" }},
            new Player { FirstName = "Stephan", LastName = "Lichtsteiner", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { FirstName = "Gianluigi", LastName = "Buffon", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Pavel", LastName = "Nedvěd", Nationality = new Nationality { Name = "Czech Republic" }},
            new Player { FirstName = "Michel", LastName = "Platini", Nationality = new Nationality { Name = "France" }}
        };

        private static readonly List<Player> PlayersFromSantos = new List<Player>
        {
            new Player { FirstName = "Pele", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new Player { FirstName = "Alex", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new Player { FirstName = "Carlos", LastName = "Galván", Nationality = new Nationality { Name = "Brazil" }},
            new Player { FirstName = "Basílio", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new Player { FirstName = "Arouca", LastName = "", Nationality = new Nationality { Name = "Argentina" }}
        };

        private static readonly List<Player> PlayersFromRoma = new List<Player>
        {
            new Player { FirstName = "Rudi", LastName = "Völler", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Ashley", LastName = "Cole", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Federico", LastName = "Balzaretti", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Francesco", LastName = "Totti", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Daniele", LastName = "De Rossi", Nationality = new Nationality { Name = "Italy" }}
        };

        private static readonly List<Player> PlayersFromDortmund = new List<Player>
        {
            new Player { FirstName = "Stephane", LastName = "Chapuisat", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { FirstName = "Marco", LastName = "Reus", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Jürgen", LastName = "Kohler", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Stefan", LastName = "Reuter", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Mats", LastName = "Hummels", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Michael", LastName = "Zorc", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Lars", LastName = "Ricken", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Roman", LastName = "Weidenfeller", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Christian", LastName = "Wörns", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Andreas", LastName = "Möller", Nationality = new Nationality { Name = "Germany" }},
            new Player { FirstName = "Roman", LastName = "Bürki", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { FirstName = "Alex", LastName = "Frei", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { FirstName = "Sven", LastName = "Bender", Nationality = new Nationality { Name = "Germany" }}
        };

        private static readonly List<Player> PlayersFromAjax = new List<Player> 
        { 
            new Player { FirstName = "Johann", LastName = "Cruyff", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { FirstName = "Bas", LastName = "Kuipers", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { FirstName = "Ronald", LastName = "de Boer", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { FirstName = "Peter", LastName = "Larsson", Nationality = new Nationality { Name = "Sweden" }},
            new Player { FirstName = "André", LastName = "Onana", Nationality = new Nationality { Name = "Cameroun" }}
        };

        private static readonly List<Player> PlayersFromManchester = new List<Player>
        {
            new Player { FirstName = "George", LastName = "Best", Nationality = new Nationality { Name = "North Ireland" }},
            new Player { FirstName = "Wayne", LastName = "Rooney", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Ryan", LastName = "Giggs", Nationality = new Nationality { Name = "Wales" }},
            new Player { FirstName = "Bobby", LastName = "Charlton", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Eric", LastName = "Cantona", Nationality = new Nationality { Name = "France" }},
            new Player { FirstName = "Paul", LastName = "Scholes", Nationality = new Nationality { Name = "England" }}, 
            new Player { FirstName = "Gary", LastName = "Neville", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Roy", LastName = "Keane", Nationality = new Nationality { Name = "Ireland" }},
            new Player { FirstName = "Peter", LastName = "Schmeichel", Nationality = new Nationality { Name = "Denmark" }},
            new Player { FirstName = "Ole Gunnar", LastName = "Solskjær", Nationality = new Nationality { Name = "Norway" }},
            new Player { FirstName = "Wes", LastName = "Brown", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Darren", LastName = "Fletcher", Nationality = new Nationality { Name = "Scotland" }},
            new Player { FirstName = "Andy", LastName = "Cole", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Robin", LastName = "van Persie", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { FirstName = "Javier", LastName = "Hernández", Nationality = new Nationality { Name = "Mexico" }},
            new Player { FirstName = "Ashley", LastName = "Young", Nationality = new Nationality { Name = "England" }}
        };

        private static readonly List<Player> PlayersFromLiverpool = new List<Player>
        {
            new Player { FirstName = "Steven", LastName = "Gerrard", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Raheem", LastName = "Sterling", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Fernando", LastName = "Torres", Nationality = new Nationality { Name = "Spain" }},
            new Player { FirstName = "Robbie", LastName = "Fowler", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Ian", LastName = "Rush", Nationality = new Nationality { Name = "Wales" }}
        };

        private static readonly List<Player> PlayersFromChelsea = new List<Player>
        { 
            new Player { FirstName = "Frank", LastName = "Lampard", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Eden", LastName = "Hazard", Nationality = new Nationality { Name = "Belgium" }},
            new Player { FirstName = "Cesar", LastName = "Azpilicueta", Nationality = new Nationality { Name = "Spain" }},
            new Player { FirstName = "John", LastName = "Terry", Nationality = new Nationality { Name = "England" }},
            new Player { FirstName = "Petr", LastName = "Čech", Nationality = new Nationality { Name = "Czech Republic" }}
        };

        private static readonly List<Player> PlayersFromNapoli = new List<Player>
        {
            new Player { FirstName = "Diego", LastName = "Maradona", Nationality = new Nationality { Name = "Argentina" }},
            new Player { FirstName = "Paolo", LastName = "Cannavaro", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Marek", LastName = "Hamšík", Nationality = new Nationality { Name = "Slovakia" }},
            new Player { FirstName = "Giuseppe", LastName = "Bruscolotti", Nationality = new Nationality { Name = "Italy" }},
            new Player { FirstName = "Gonzalo", LastName = "Higuaín", Nationality = new Nationality { Name = "Argentina" }}
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

        public static IEnumerable<PlayerWithTeam> GetPlayerListWithTeams(List<Team> teams)
        {
            foreach (var team in teams)
            {
                var playersOfTeam = TeamDictionary[team.Name];
                foreach (var player in playersOfTeam)
                {
                    var playerWithTeam = new PlayerWithTeam
                    {
                        DateOfBirth = player.DateOfBirth,
                        FirstName = player.FirstName,
                        Id = player.Id,
                        LastName = player.LastName,
                        MiddleName = player.MiddleName,
                        Nationality = player.Nationality,
                        Team = team
                    };
                    yield return playerWithTeam;
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
