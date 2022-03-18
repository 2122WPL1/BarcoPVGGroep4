using System;
using System.Collections.Generic;

namespace BarcoPVG.Models.Db
{
    public partial class Person
    {
        public string Afkorting { get; set; } = null!;
        public string? Voornaam { get; set; }
        public string? Familienaam { get; set; }
        public string? Password { get; set; }
    }
}
