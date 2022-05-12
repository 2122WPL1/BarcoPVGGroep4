using System;
using System.Collections.Generic;

namespace BarcoDB_Admin.Models.Db
{
    public partial class Person
    {
        public string Afkorting { get; set; } = null!;
        public string? Voornaam { get; set; }
        public string? Familienaam { get; set; }
        public string? wachtwoord { get; set; }
        public string? Function { get; set; }
        
    }
}
