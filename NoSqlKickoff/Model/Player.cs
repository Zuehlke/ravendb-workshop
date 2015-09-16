﻿using System;
using System.Collections.Generic;

using Raven.Imports.Newtonsoft.Json;

namespace NoSqlKickoff.Model
{
    public class Player
    {
        public Player()
        {
            Employments = new List<Employment>();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Nationality Nationality { get; set; }

        public string TeamId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Team TeamNavigationProperty { get; set; }

        public List<Employment> Employments { get; set; }
    }

    public class Nationality
    {
        public string Name { get; set; }
    }
}