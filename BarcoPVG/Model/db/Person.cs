using System;
using System.Collections.Generic;

namespace BarcoPVG.Model.db
{
    public partial class Person
    {
        public string Afkorting { get; set; } = null!;
        public string? Voornaam { get; set; }
        public string? Familienaam { get; set; }
    }
}
