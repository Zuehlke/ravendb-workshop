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
            return PlayerGenerator.GetPlayers().Select(pi => new Player
                                                                 {
                                                                     DateOfBirth = pi.DateOfBirth,
                                                                     FirstName = pi.FirstName,
                                                                     LastName = pi.LastName,
                                                                     MiddleName = pi.MiddleName,
                                                                     Nationality = pi.Nationality,
                                                                     TeamId = pi.TeamId
                                                                 }).ToList();
        }

        public static List<Player> CreatePlayerListWithTeamIds(List<Team> teams)
        {
            return PlayerGenerator.GetPlayersWithTeamIds(teams).Select(pi => 
                new Player
                    {
                        DateOfBirth = pi.DateOfBirth,
                        FirstName = pi.FirstName,
                        LastName = pi.LastName,
                        MiddleName = pi.MiddleName,
                        Nationality = pi.Nationality,
                        TeamId = pi.TeamId
                    }).ToList();
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

        private static readonly List<PlayerInfo> PlayersFromBayern = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Bastian", LastName = "Schweinsteiger", Nationality = new Nationality { Name = "Germany" }, Team = "Bayern München", Season = "2013-2014" },
            new PlayerInfo { FirstName = "Manuel", LastName = "Neuer", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Philipp", LastName = "Lahm", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Lothar", LastName = "Matthäus", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Franz", LastName = "Beckenbauer", Nationality = new Nationality { Name = "Germany" }}
        };

        private static readonly List<PlayerInfo> PlayersFromReal = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Christiano", LastName = "Ronaldo", Nationality = new Nationality { Name = "Portugal" }},
            new PlayerInfo { FirstName = "Alfredo", LastName = "di Stefano", Nationality = new Nationality { Name = "Argentina" }},
            new PlayerInfo { FirstName = "Iker", LastName = "Casillas", Nationality = new Nationality { Name = "Spain" }},
            new PlayerInfo { FirstName = "Özil", LastName = "Casillas", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Ferenc", LastName = "Puskas", Nationality = new Nationality { Name = "Hungary" }}
        };

        private static readonly List<PlayerInfo> PlayersFromBarcelona = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Lionel", LastName = "Messi", Nationality = new Nationality { Name = "Argentina" }},
            new PlayerInfo { FirstName = "Jordi", LastName = "Alba", Nationality = new Nationality { Name = "Spain" }},
            new PlayerInfo { FirstName = "Iniesta", LastName = "Andrés", Nationality = new Nationality { Name = "Spain" }},
            new PlayerInfo { FirstName = "Puyol", LastName = "Carles", Nationality = new Nationality { Name = "Spain" }},
            new PlayerInfo { FirstName = "Ivan", LastName = "Rakitic", Nationality = new Nationality { Name = "Croatia" }}
        };

        private static readonly List<PlayerInfo> PlayersFromMilan = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Paolo", LastName = "Maldini", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Mario", LastName = "Balotelli", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Franco", LastName = "Baresi", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Clarence", LastName = "Seedorf", Nationality = new Nationality { Name = "Netherlands" }},
            new PlayerInfo { FirstName = "Kevin-Prince", LastName = "Boateng", Nationality = new Nationality { Name = "Ghana" }}
        };

        private static readonly List<PlayerInfo> PlayersFromJuve = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Zinedine", LastName = "Zidane", Nationality = new Nationality { Name = "France" }},
            new PlayerInfo { FirstName = "Stephan", LastName = "Lichtsteiner", Nationality = new Nationality { Name = "Switzerland" }},
            new PlayerInfo { FirstName = "Gianluigi", LastName = "Buffon", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Pavel", LastName = "Nedvěd", Nationality = new Nationality { Name = "Czech Republic" }},
            new PlayerInfo { FirstName = "Michel", LastName = "Platini", Nationality = new Nationality { Name = "France" }}
        };

        private static readonly List<PlayerInfo> PlayersFromSantos = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Pele", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new PlayerInfo { FirstName = "Alex", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new PlayerInfo { FirstName = "Carlos", LastName = "Galván", Nationality = new Nationality { Name = "Brazil" }},
            new PlayerInfo { FirstName = "Basílio", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new PlayerInfo { FirstName = "Arouca", LastName = "", Nationality = new Nationality { Name = "Argentina" }}
        };

        private static readonly List<PlayerInfo> PlayersFromRoma = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Rudi", LastName = "Völler", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Ashley", LastName = "Cole", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Federico", LastName = "Balzaretti", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Francesco", LastName = "Totti", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Daniele", LastName = "De Rossi", Nationality = new Nationality { Name = "Italy" }}
        };

        private static readonly List<PlayerInfo> PlayersFromDortmund = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Stephane", LastName = "Chapuisat", Nationality = new Nationality { Name = "Switzerland" }},
            new PlayerInfo { FirstName = "Marco", LastName = "Reus", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Jürgen", LastName = "Kohler", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Stefan", LastName = "Reuter", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Mats", LastName = "Hummels", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Michael", LastName = "Zorc", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Lars", LastName = "Ricken", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Roman", LastName = "Weidenfeller", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Christian", LastName = "Wörns", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Andreas", LastName = "Möller", Nationality = new Nationality { Name = "Germany" }},
            new PlayerInfo { FirstName = "Roman", LastName = "Bürki", Nationality = new Nationality { Name = "Switzerland" }},
            new PlayerInfo { FirstName = "Alex", LastName = "Frei", Nationality = new Nationality { Name = "Switzerland" }},
            new PlayerInfo { FirstName = "Sven", LastName = "Bender", Nationality = new Nationality { Name = "Germany" }}
        };

        private static readonly List<PlayerInfo> PlayersFromAjax = new List<PlayerInfo> 
        { 
            new PlayerInfo { FirstName = "Johann", LastName = "Cruyff", Nationality = new Nationality { Name = "Netherlands" }},
            new PlayerInfo { FirstName = "Bas", LastName = "Kuipers", Nationality = new Nationality { Name = "Netherlands" }},
            new PlayerInfo { FirstName = "Ronald", LastName = "de Boer", Nationality = new Nationality { Name = "Netherlands" }},
            new PlayerInfo { FirstName = "Peter", LastName = "Larsson", Nationality = new Nationality { Name = "Sweden" }},
            new PlayerInfo { FirstName = "André", LastName = "Onana", Nationality = new Nationality { Name = "Cameroun" }}
        };

        private static readonly List<PlayerInfo> PlayersFromManchester = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "George", LastName = "Best", Nationality = new Nationality { Name = "North Ireland" }},
            new PlayerInfo { FirstName = "Wayne", LastName = "Rooney", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Ryan", LastName = "Giggs", Nationality = new Nationality { Name = "Wales" }},
            new PlayerInfo { FirstName = "Bobby", LastName = "Charlton", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Eric", LastName = "Cantona", Nationality = new Nationality { Name = "France" }},
            new PlayerInfo { FirstName = "Paul", LastName = "Scholes", Nationality = new Nationality { Name = "England" }}, 
            new PlayerInfo { FirstName = "Gary", LastName = "Neville", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Roy", LastName = "Keane", Nationality = new Nationality { Name = "Ireland" }},
            new PlayerInfo { FirstName = "Peter", LastName = "Schmeichel", Nationality = new Nationality { Name = "Denmark" }},
            new PlayerInfo { FirstName = "Ole Gunnar", LastName = "Solskjær", Nationality = new Nationality { Name = "Norway" }},
            new PlayerInfo { FirstName = "Wes", LastName = "Brown", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Darren", LastName = "Fletcher", Nationality = new Nationality { Name = "Scotland" }},
            new PlayerInfo { FirstName = "Andy", LastName = "Cole", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Robin", LastName = "van Persie", Nationality = new Nationality { Name = "Netherlands" }},
            new PlayerInfo { FirstName = "Javier", LastName = "Hernández", Nationality = new Nationality { Name = "Mexico" }},
            new PlayerInfo { FirstName = "Ashley", LastName = "Young", Nationality = new Nationality { Name = "England" }}
        };

        private static readonly List<PlayerInfo> PlayersFromLiverpool = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Steven", LastName = "Gerrard", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Raheem", LastName = "Sterling", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Fernando", LastName = "Torres", Nationality = new Nationality { Name = "Spain" }},
            new PlayerInfo { FirstName = "Robbie", LastName = "Fowler", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Ian", LastName = "Rush", Nationality = new Nationality { Name = "Wales" }}
        };

        private static readonly List<PlayerInfo> PlayersFromChelsea = new List<PlayerInfo>
        { 
            new PlayerInfo { FirstName = "Frank", LastName = "Lampard", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Eden", LastName = "Hazard", Nationality = new Nationality { Name = "Belgium" }},
            new PlayerInfo { FirstName = "Cesar", LastName = "Azpilicueta", Nationality = new Nationality { Name = "Spain" }},
            new PlayerInfo { FirstName = "John", LastName = "Terry", Nationality = new Nationality { Name = "England" }},
            new PlayerInfo { FirstName = "Petr", LastName = "Čech", Nationality = new Nationality { Name = "Czech Republic" }}
        };

        private static readonly List<PlayerInfo> PlayersFromNapoli = new List<PlayerInfo>
        {
            new PlayerInfo { FirstName = "Diego", LastName = "Maradona", Nationality = new Nationality { Name = "Argentina" }},
            new PlayerInfo { FirstName = "Paolo", LastName = "Cannavaro", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Marek", LastName = "Hamšík", Nationality = new Nationality { Name = "Slovakia" }},
            new PlayerInfo { FirstName = "Giuseppe", LastName = "Bruscolotti", Nationality = new Nationality { Name = "Italy" }},
            new PlayerInfo { FirstName = "Gonzalo", LastName = "Higuaín", Nationality = new Nationality { Name = "Argentina" }}
        };

        private static readonly Dictionary<string, List<PlayerInfo>> TeamDictionary = new Dictionary<string, List<PlayerInfo>>
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

        public static List<PlayerInfo> GetPlayers()
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

        public static IEnumerable<PlayerInfo> GetPlayersWithTeamIds(List<Team> teams)
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

    public class PlayerInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Season { get; set; }

        public string Team { get; set; }

        public string TeamId { get; set; }

        public string Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Nationality Nationality { get; set; }
    }
}
