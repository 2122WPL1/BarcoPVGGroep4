using System;
using System.Collections.Generic;

namespace BarcoDB_Admin.Models.Db
{
    public partial class Person
    {
        public string Afkorting { get; set; } = null!;
        public string? Voornaam { get; set; }
        public string? Familienaam { get; set; }
        public string? Wachtwoord { get; set; }
        public string? Email { get; set; }
        public string? Functie { get; set; }
        
    }
}
