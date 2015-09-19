using System;
using System.Collections.Generic;
using System.Linq;

using NoSqlKickoff.Model;
using NoSqlKickoff.Model.Exercises;

namespace NoSqlKickoff.Tests.Exercises
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

        public static List<Team> CreateTeamList()
        {
            return TeamGenerator.GetTeams();
        }

        public static List<Employment> CreateEmploymentList()
        {
            return EmploymentGenerator.GetEmployments();
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
            new Player { Id = "Players/113", FirstName = "Mesut", LastName = "Özil", Nationality = new Nationality { Name = "Germany" }},
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

    public class EmploymentGenerator
    {
        #region Employment Data

        //TODO: Add correct season & salary data

        private static readonly List<Employment> EmploymentsFromBayern = new List<Employment>
        {
            new Employment {Id = "Employments/100", PlayerId = "Players/100", TeamId = "Teams/15", Season = "2014-2015", Salary = 1000000 },
            new Employment {Id = "Employments/101", PlayerId = "Players/101", TeamId = "Teams/15", Season = "2014-2015", Salary = 1000000 },
            new Employment {Id = "Employments/102", PlayerId = "Players/102", TeamId = "Teams/15", Season = "2014-2015", Salary = 1000000 },
            new Employment {Id = "Employments/103", PlayerId = "Players/103", TeamId = "Teams/15", Season = "2014-2015", Salary = 1000000 },
            new Employment {Id = "Employments/104", PlayerId = "Players/104", TeamId = "Teams/15", Season = "2014-2015", Salary = 1000000 }                               
        };

        private static readonly List<Employment> EmploymentsFromReal = new List<Employment>
        {
            new Employment { Id = "Employments/110", PlayerId = "Players/110", TeamId = "Teams/10", Season = "2007-2008", Salary = 1000000 },
            new Employment { Id = "Employments/111", PlayerId = "Players/111", TeamId = "Teams/10", Season = "2007-2008", Salary = 1000000 },
            new Employment { Id = "Employments/112", PlayerId = "Players/112", TeamId = "Teams/10", Season = "2007-2008", Salary = 1000000 },
            new Employment { Id = "Employments/113", PlayerId = "Players/113", TeamId = "Teams/10", Season = "2007-2008", Salary = 1000000 },
            new Employment { Id = "Employments/114", PlayerId = "Players/114", TeamId = "Teams/10", Season = "2007-2008", Salary = 1000000 },
            new Employment { Id = "Employments/115", PlayerId = "Players/245", TeamId = "Teams/10", Season = "2007-2008", Salary = 1000000 }, // Higuain
            new Employment { Id = "Employments/116", PlayerId = "Players/245", TeamId = "Teams/10", Season = "2008-2009", Salary = 1000000 }, // Higuain
            new Employment { Id = "Employments/117", PlayerId = "Players/245", TeamId = "Teams/10", Season = "2009-2010", Salary = 1000000 }, // Higuain
            new Employment { Id = "Employments/118", PlayerId = "Players/245", TeamId = "Teams/10", Season = "2010-2011", Salary = 1000000 }, // Higuain
            new Employment { Id = "Employments/119", PlayerId = "Players/245", TeamId = "Teams/10", Season = "2011-2012", Salary = 1000000 }, // Higuain
            new Employment { Id = "Employments/120", PlayerId = "Players/245", TeamId = "Teams/10", Season = "2012-2013", Salary = 1000000 }, // Higuain
        };

        private static readonly List<Employment> EmploymentsFromBarcelona = new List<Employment>
        {
            new Employment { Id = "Employments/121", PlayerId = "Players/121", TeamId = "Teams/20", Season = "2008-2009", Salary = 1000000 },
            new Employment { Id = "Employments/122", PlayerId = "Players/122", TeamId = "Teams/20", Season = "2008-2009", Salary = 1000000 },
            new Employment { Id = "Employments/123", PlayerId = "Players/123", TeamId = "Teams/20", Season = "2008-2009", Salary = 1000000 },
            new Employment { Id = "Employments/124", PlayerId = "Players/124", TeamId = "Teams/20", Season = "2008-2009", Salary = 1000000 },
            new Employment { Id = "Employments/125", PlayerId = "Players/125", TeamId = "Teams/20", Season = "2008-2009", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromMilan = new List<Employment>
        {
            new Employment { Id = "Employments/131", PlayerId = "Players/131", TeamId = "Teams/50", Season = "2009-2010", Salary = 1000000 },
            new Employment { Id = "Employments/132", PlayerId = "Players/132", TeamId = "Teams/50", Season = "2009-2010", Salary = 1000000 },
            new Employment { Id = "Employments/133", PlayerId = "Players/133", TeamId = "Teams/50", Season = "2009-2010", Salary = 1000000 },
            new Employment { Id = "Employments/134", PlayerId = "Players/134", TeamId = "Teams/50", Season = "2009-2010", Salary = 1000000 },
            new Employment { Id = "Employments/135", PlayerId = "Players/135", TeamId = "Teams/50", Season = "2009-2010", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromJuve = new List<Employment>
        {
            new Employment { Id = "Employments/141", PlayerId = "Players/141", TeamId = "Teams/55", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/142", PlayerId = "Players/142", TeamId = "Teams/55", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/143", PlayerId = "Players/143", TeamId = "Teams/55", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/144", PlayerId = "Players/144", TeamId = "Teams/55", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/145", PlayerId = "Players/145", TeamId = "Teams/55", Season = "2010-2011", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromSantos = new List<Employment>
        {
            new Employment { Id = "Employments/151", PlayerId = "Players/151", TeamId = "Teams/40", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/152", PlayerId = "Players/152", TeamId = "Teams/40", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/153", PlayerId = "Players/153", TeamId = "Teams/40", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/154", PlayerId = "Players/154", TeamId = "Teams/40", Season = "2010-2011", Salary = 1000000 },
            new Employment { Id = "Employments/155", PlayerId = "Players/155", TeamId = "Teams/40", Season = "2010-2011", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromRoma = new List<Employment>
        {
            new Employment { Id = "Employments/161", PlayerId = "Players/161", TeamId = "Teams/60", Season = "2011-2012", Salary = 1000000 },
            new Employment { Id = "Employments/162", PlayerId = "Players/162", TeamId = "Teams/60", Season = "2011-2012", Salary = 1000000 },
            new Employment { Id = "Employments/163", PlayerId = "Players/163", TeamId = "Teams/60", Season = "2011-2012", Salary = 1000000 },
            new Employment { Id = "Employments/164", PlayerId = "Players/164", TeamId = "Teams/60", Season = "2011-2012", Salary = 1000000 },
            new Employment { Id = "Employments/165", PlayerId = "Players/165", TeamId = "Teams/60", Season = "2011-2012", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromDortmund = new List<Employment>
        {
            new Employment { Id = "Employments/171", PlayerId = "Players/171", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/172", PlayerId = "Players/172", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/173", PlayerId = "Players/173", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/174", PlayerId = "Players/174", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/175", PlayerId = "Players/175", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/176", PlayerId = "Players/176", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/177", PlayerId = "Players/177", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/178", PlayerId = "Players/178", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/179", PlayerId = "Players/179", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/180", PlayerId = "Players/180", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/181", PlayerId = "Players/181", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/182", PlayerId = "Players/182", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 },
            new Employment { Id = "Employments/183", PlayerId = "Players/183", TeamId = "Teams/25", Season = "2013-2014", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromAjax = new List<Employment> 
        { 
            new Employment { Id = "Employments/191", PlayerId = "Players/191", TeamId = "Teams/70", Season = "1995-1996", Salary = 1000000 },
            new Employment { Id = "Employments/192", PlayerId = "Players/192", TeamId = "Teams/70", Season = "1995-1996", Salary = 1000000 },
            new Employment { Id = "Employments/193", PlayerId = "Players/193", TeamId = "Teams/70", Season = "1995-1996", Salary = 1000000 },
            new Employment { Id = "Employments/194", PlayerId = "Players/194", TeamId = "Teams/70", Season = "1995-1996", Salary = 1000000 },
            new Employment { Id = "Employments/195", PlayerId = "Players/195", TeamId = "Teams/70", Season = "1995-1996", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromManchester = new List<Employment>
        {
            new Employment { Id = "Employments/201", PlayerId = "Players/201", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/202", PlayerId = "Players/202", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/203", PlayerId = "Players/203", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/204", PlayerId = "Players/204", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/205", PlayerId = "Players/205", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/206", PlayerId = "Players/206", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/207", PlayerId = "Players/207", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/208", PlayerId = "Players/208", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/209", PlayerId = "Players/209", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/210", PlayerId = "Players/210", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/211", PlayerId = "Players/211", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/212", PlayerId = "Players/212", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/213", PlayerId = "Players/213", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/214", PlayerId = "Players/214", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/215", PlayerId = "Players/215", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 },
            new Employment { Id = "Employments/216", PlayerId = "Players/216", TeamId = "Teams/30", Season = "2004-2005", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromLiverpool = new List<Employment>
        {
            new Employment { Id = "Employments/221", PlayerId = "Players/221", TeamId = "Teams/35", Season = "2003-2004", Salary = 1000000 },
            new Employment { Id = "Employments/222", PlayerId = "Players/222", TeamId = "Teams/35", Season = "2003-2004", Salary = 1000000 },
            new Employment { Id = "Employments/223", PlayerId = "Players/223", TeamId = "Teams/35", Season = "2003-2004", Salary = 1000000 },
            new Employment { Id = "Employments/224", PlayerId = "Players/224", TeamId = "Teams/35", Season = "2003-2004", Salary = 1000000 },
            new Employment { Id = "Employments/225", PlayerId = "Players/225", TeamId = "Teams/35", Season = "2003-2004", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromChelsea = new List<Employment>
        { 
            new Employment { Id = "Employments/231", PlayerId = "Players/231", TeamId = "Teams/65", Season = "2002-2003", Salary = 1000000 },
            new Employment { Id = "Employments/232", PlayerId = "Players/232", TeamId = "Teams/65", Season = "2002-2003", Salary = 1000000 },
            new Employment { Id = "Employments/233", PlayerId = "Players/233", TeamId = "Teams/65", Season = "2002-2003", Salary = 1000000 },
            new Employment { Id = "Employments/234", PlayerId = "Players/234", TeamId = "Teams/65", Season = "2002-2003", Salary = 1000000 },
            new Employment { Id = "Employments/235", PlayerId = "Players/235", TeamId = "Teams/65", Season = "2002-2003", Salary = 1000000 }
        };

        private static readonly List<Employment> EmploymentsFromNapoli = new List<Employment>
        {
            new Employment { Id = "Employments/241", PlayerId = "Players/241", TeamId = "Teams/45", Season = "2000-2001", Salary = 1000000 },
            new Employment { Id = "Employments/242", PlayerId = "Players/242", TeamId = "Teams/45", Season = "2000-2001", Salary = 1000000 },
            new Employment { Id = "Employments/243", PlayerId = "Players/243", TeamId = "Teams/45", Season = "2000-2001", Salary = 1000000 },
            new Employment { Id = "Employments/244", PlayerId = "Players/244", TeamId = "Teams/45", Season = "2000-2001", Salary = 1000000 },
            new Employment { Id = "Employments/245", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2013-2014", Salary = 1000000 }, //Higuain
            new Employment { Id = "Employments/246", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2014-2015", Salary = 1000000 }, //Higuain
            new Employment { Id = "Employments/247", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2015-2016", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/248", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2016-2017", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/249", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2017-2018", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/250", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2018-2019", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/251", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2019-2020", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/252", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2020-2021", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/253", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2021-2022", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/254", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2022-2023", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/255", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2023-2024", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/256", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2023-2024", Salary = 1000000 },  //Higuain
            new Employment { Id = "Employments/257", PlayerId = "Players/245", TeamId = "Teams/45", Season = "2023-2024", Salary = 1000000 }  //Higuain
        };

        #endregion

        public static List<Employment> GetEmployments()
        {
            return
                EmploymentsFromAjax
                    .Concat(EmploymentsFromBayern)
                    .Concat(EmploymentsFromBarcelona)
                    .Concat(EmploymentsFromChelsea)
                    .Concat(EmploymentsFromDortmund)
                    .Concat(EmploymentsFromJuve)
                    .Concat(EmploymentsFromLiverpool)
                    .Concat(EmploymentsFromManchester)
                    .Concat(EmploymentsFromMilan)
                    .Concat(EmploymentsFromNapoli)
                    .Concat(EmploymentsFromReal)
                    .Concat(EmploymentsFromRoma)
                    .Concat(EmploymentsFromSantos)
                    .ToList();

        }
    }
}
