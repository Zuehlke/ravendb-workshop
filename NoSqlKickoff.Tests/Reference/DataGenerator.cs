using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Reference;

namespace NoSqlKickoff.Tests.Reference
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

        public static List<Player> CreatePlayerListWithTeamIds()
        {
            var teams = TeamGenerator.GetTeams();
            return PlayerGenerator.GetPlayersWithTeamIds(teams).ToList();
        }

        public static List<PlayerWithTeam> CreatePlayerListWithTeams()
        {
            var teams = TeamGenerator.GetTeams();
            return PlayerGenerator.GetPlayerListWithTeams(teams).ToList();
        }

        public static List<Team> CreateTeamList()
        {
            return TeamGenerator.GetTeams();
        }
    }

    public class TeamGenerator
    {
        #region Team Data

        private static readonly List<Team> Teams = new List<Team>
        {
            new Team { Id = "Teams/10", Name = "Real Madrid", Country = "Spain" },
            new Team { Id = "Teams/15", Name = "Bayern München", Country = "Germany" },
            new Team { Id = "Teams/20", Name = "FC Barcelona", Country = "Spain" },
            new Team { Id = "Teams/25", Name = "Borussia Dortmund", Country = "Germany" },
            new Team { Id = "Teams/30", Name = "Manchester United", Country = "England" },
            new Team { Id = "Teams/35", Name = "FC Liverpool", Country = "England" },
            new Team { Id = "Teams/40", Name = "FC Santos", Country = "Brazil" },
            new Team { Id = "Teams/45", Name = "SSC Neapel", Country = "Italy" },
            new Team { Id = "Teams/50", Name = "AC Milan", Country = "Italy" },
            new Team { Id = "Teams/55", Name = "Juventus Turin", Country = "Italy" },
            new Team { Id = "Teams/60", Name = "AS Rom", Country = "Italy" },
            new Team { Id = "Teams/65", Name = "FC Chelsea", Country = "England" },
            new Team { Id = "Teams/70", Name = "Ajax Amsterdam", Country = "Netherlands" },
        };

        #endregion

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
            new Player { Id = "Players/100", FirstName = "Bastian", LastName = "Schweinsteiger", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/101", FirstName = "Manuel", LastName = "Neuer", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/102", FirstName = "Philipp", LastName = "Lahm", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/103", FirstName = "Lothar", LastName = "Matthäus", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/104", FirstName = "Franz", LastName = "Beckenbauer", Nationality = new Nationality { Name = "Germany" }}
        };

        private static readonly List<Player> PlayersFromReal = new List<Player>
        {
            new Player { Id = "Players/110", FirstName = "Christiano", LastName = "Ronaldo", Nationality = new Nationality { Name = "Portugal" }},
            new Player { Id = "Players/111", FirstName = "Alfredo", LastName = "di Stefano", Nationality = new Nationality { Name = "Argentina" }},
            new Player { Id = "Players/112", FirstName = "Iker", LastName = "Casillas", Nationality = new Nationality { Name = "Spain" }},
            new Player { Id = "Players/113", FirstName = "Özil", LastName = "Casillas", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/114", FirstName = "Ferenc", LastName = "Puskas", Nationality = new Nationality { Name = "Hungary" }}
        };

        private static readonly List<Player> PlayersFromBarcelona = new List<Player>
        {
            new Player { Id = "Players/121", FirstName = "Lionel", LastName = "Messi", Nationality = new Nationality { Name = "Argentina" }},
            new Player { Id = "Players/122", FirstName = "Jordi", LastName = "Alba", Nationality = new Nationality { Name = "Spain" }},
            new Player { Id = "Players/123", FirstName = "Iniesta", LastName = "Andrés", Nationality = new Nationality { Name = "Spain" }},
            new Player { Id = "Players/124", FirstName = "Puyol", LastName = "Carles", Nationality = new Nationality { Name = "Spain" }},
            new Player { Id = "Players/125", FirstName = "Ivan", LastName = "Rakitic", Nationality = new Nationality { Name = "Croatia" }}
        };

        private static readonly List<Player> PlayersFromMilan = new List<Player>
        {
            new Player { Id = "Players/131", FirstName = "Paolo", LastName = "Maldini", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/132", FirstName = "Mario", LastName = "Balotelli", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/133", FirstName = "Franco", LastName = "Baresi", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/134", FirstName = "Clarence", LastName = "Seedorf", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { Id = "Players/135", FirstName = "Kevin-Prince", LastName = "Boateng", Nationality = new Nationality { Name = "Ghana" }}
        };

        private static readonly List<Player> PlayersFromJuve = new List<Player>
        {
            new Player { Id = "Players/141", FirstName = "Zinedine", LastName = "Zidane", Nationality = new Nationality { Name = "France" }},
            new Player { Id = "Players/142", FirstName = "Stephan", LastName = "Lichtsteiner", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { Id = "Players/143", FirstName = "Gianluigi", LastName = "Buffon", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/144", FirstName = "Pavel", LastName = "Nedvěd", Nationality = new Nationality { Name = "Czech Republic" }},
            new Player { Id = "Players/145", FirstName = "Michel", LastName = "Platini", Nationality = new Nationality { Name = "France" }}
        };

        private static readonly List<Player> PlayersFromSantos = new List<Player>
        {
            new Player { Id = "Players/151", FirstName = "Pele", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new Player { Id = "Players/152", FirstName = "Alex", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new Player { Id = "Players/153", FirstName = "Carlos", LastName = "Galván", Nationality = new Nationality { Name = "Brazil" }},
            new Player { Id = "Players/154", FirstName = "Basílio", LastName = "", Nationality = new Nationality { Name = "Brazil" }},
            new Player { Id = "Players/155", FirstName = "Arouca", LastName = "", Nationality = new Nationality { Name = "Argentina" }}
        };

        private static readonly List<Player> PlayersFromRoma = new List<Player>
        {
            new Player { Id = "Players/161", FirstName = "Rudi", LastName = "Völler", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/162", FirstName = "Ashley", LastName = "Cole", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/163", FirstName = "Federico", LastName = "Balzaretti", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/164", FirstName = "Francesco", LastName = "Totti", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/165", FirstName = "Daniele", LastName = "De Rossi", Nationality = new Nationality { Name = "Italy" }}
        };

        private static readonly List<Player> PlayersFromDortmund = new List<Player>
        {
            new Player { Id = "Players/171", FirstName = "Stephane", LastName = "Chapuisat", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { Id = "Players/172", FirstName = "Marco", LastName = "Reus", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/173", FirstName = "Jürgen", LastName = "Kohler", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/174", FirstName = "Stefan", LastName = "Reuter", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/175", FirstName = "Mats", LastName = "Hummels", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/176", FirstName = "Michael", LastName = "Zorc", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/177", FirstName = "Lars", LastName = "Ricken", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/178", FirstName = "Roman", LastName = "Weidenfeller", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/179", FirstName = "Christian", LastName = "Wörns", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/180", FirstName = "Andreas", LastName = "Möller", Nationality = new Nationality { Name = "Germany" }},
            new Player { Id = "Players/181", FirstName = "Roman", LastName = "Bürki", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { Id = "Players/182", FirstName = "Alex", LastName = "Frei", Nationality = new Nationality { Name = "Switzerland" }},
            new Player { Id = "Players/183", FirstName = "Sven", LastName = "Bender", Nationality = new Nationality { Name = "Germany" }}
        };

        private static readonly List<Player> PlayersFromAjax = new List<Player> 
        { 
            new Player { Id = "Players/191", FirstName = "Johann", LastName = "Cruyff", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { Id = "Players/192", FirstName = "Bas", LastName = "Kuipers", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { Id = "Players/193", FirstName = "Ronald", LastName = "de Boer", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { Id = "Players/194", FirstName = "Peter", LastName = "Larsson", Nationality = new Nationality { Name = "Sweden" }},
            new Player { Id = "Players/195", FirstName = "André", LastName = "Onana", Nationality = new Nationality { Name = "Cameroun" }}
        };

        private static readonly List<Player> PlayersFromManchester = new List<Player>
        {
            new Player { Id = "Players/201", FirstName = "George", LastName = "Best", Nationality = new Nationality { Name = "North Ireland" }},
            new Player { Id = "Players/202", FirstName = "Wayne", LastName = "Rooney", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/203", FirstName = "Ryan", LastName = "Giggs", Nationality = new Nationality { Name = "Wales" }},
            new Player { Id = "Players/204", FirstName = "Bobby", LastName = "Charlton", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/205", FirstName = "Eric", LastName = "Cantona", Nationality = new Nationality { Name = "France" }},
            new Player { Id = "Players/206", FirstName = "Paul", LastName = "Scholes", Nationality = new Nationality { Name = "England" }}, 
            new Player { Id = "Players/207", FirstName = "Gary", LastName = "Neville", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/208", FirstName = "Roy", LastName = "Keane", Nationality = new Nationality { Name = "Ireland" }},
            new Player { Id = "Players/209", FirstName = "Peter", LastName = "Schmeichel", Nationality = new Nationality { Name = "Denmark" }},
            new Player { Id = "Players/210", FirstName = "Ole Gunnar", LastName = "Solskjær", Nationality = new Nationality { Name = "Norway" }},
            new Player { Id = "Players/211", FirstName = "Wes", LastName = "Brown", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/212", FirstName = "Darren", LastName = "Fletcher", Nationality = new Nationality { Name = "Scotland" }},
            new Player { Id = "Players/213", FirstName = "Andy", LastName = "Cole", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/214", FirstName = "Robin", LastName = "van Persie", Nationality = new Nationality { Name = "Netherlands" }},
            new Player { Id = "Players/215", FirstName = "Javier", LastName = "Hernández", Nationality = new Nationality { Name = "Mexico" }},
            new Player { Id = "Players/216", FirstName = "Ashley", LastName = "Young", Nationality = new Nationality { Name = "England" }}
        };

        private static readonly List<Player> PlayersFromLiverpool = new List<Player>
        {
            new Player { Id = "Players/221", FirstName = "Steven", LastName = "Gerrard", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/222", FirstName = "Raheem", LastName = "Sterling", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/223", FirstName = "Fernando", LastName = "Torres", Nationality = new Nationality { Name = "Spain" }},
            new Player { Id = "Players/224", FirstName = "Robbie", LastName = "Fowler", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/225", FirstName = "Ian", LastName = "Rush", Nationality = new Nationality { Name = "Wales" }}
        };

        private static readonly List<Player> PlayersFromChelsea = new List<Player>
        { 
            new Player { Id = "Players/231", FirstName = "Frank", LastName = "Lampard", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/232", FirstName = "Eden", LastName = "Hazard", Nationality = new Nationality { Name = "Belgium" }},
            new Player { Id = "Players/233", FirstName = "Cesar", LastName = "Azpilicueta", Nationality = new Nationality { Name = "Spain" }},
            new Player { Id = "Players/234", FirstName = "John", LastName = "Terry", Nationality = new Nationality { Name = "England" }},
            new Player { Id = "Players/235", FirstName = "Petr", LastName = "Čech", Nationality = new Nationality { Name = "Czech Republic" }}
        };

        private static readonly List<Player> PlayersFromNapoli = new List<Player>
        {
            new Player { Id = "Players/241", FirstName = "Diego", LastName = "Maradona", Nationality = new Nationality { Name = "Argentina" }},
            new Player { Id = "Players/242", FirstName = "Paolo", LastName = "Cannavaro", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/243", FirstName = "Marek", LastName = "Hamšík", Nationality = new Nationality { Name = "Slovakia" }},
            new Player { Id = "Players/244", FirstName = "Giuseppe", LastName = "Bruscolotti", Nationality = new Nationality { Name = "Italy" }},
            new Player { Id = "Players/245", FirstName = "Gonzalo", LastName = "Higuaín", Nationality = new Nationality { Name = "Argentina" }}
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
                        FirstName = player.FirstName,
                        Id = player.Id,
                        LastName = player.LastName,
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
