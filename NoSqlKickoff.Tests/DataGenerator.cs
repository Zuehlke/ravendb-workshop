using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using NoSqlKickoff.Model;

using NUnit.Framework;

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
    }

    public class PlayerGenerator
    {
        private static readonly List<Player> Players = new List<Player>
                                                   {
                                                       new Player { FirstName = "Christiano", LastName = "Ronaldo"},
                                                       new Player { FirstName = "Lionel", LastName = "Messi"},
                                                       new Player { FirstName = "Bastian", LastName = "Schweinsteiger"},
                                                       new Player { FirstName = "Manuel", LastName = "Neuer"},
                                                       new Player { FirstName = "Philipp", LastName = "Lahm"},
                                                       new Player { FirstName = "Stephane", LastName = "Chapuisat"},
                                                       new Player { FirstName = "Diego", LastName = "Maradona"},
                                                       new Player { FirstName = "Lothar", LastName = "Matthäus"},
                                                       new Player { FirstName = "Rudi", LastName = "Völler"},
                                                       new Player { FirstName = "Franz", LastName = "Beckenbauer"},
                                                       new Player { FirstName = "Frank", LastName = "Lampard"},
                                                       new Player { FirstName = "Steven", LastName = "Gerrard"},
                                                       new Player { FirstName = "Pele", LastName = ""},
                                                       new Player { FirstName = "Johann", LastName = "Cruyff"},
                                                       new Player { FirstName = "Zinedine", LastName = "Zidane"},
                                                       new Player { FirstName = "Alfredo", LastName = "di Stefano"},
                                                       new Player { FirstName = "Ferenc", LastName = "Puskas"},
                                                       new Player { FirstName = "George", LastName = "Best"},
                                                       new Player { FirstName = "Paolo", LastName = "Maldini"},
                                                   };

        public static List<Player> GetPlayers()
        {
            return Players;
        }

        private string _firstName;
        private string _lastName;

        public PlayerGenerator()
        {
            var random = new Random();
            var nextRandom = random.Next(Players.Count - 1);

            var randomPlayer = Players[nextRandom];

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
