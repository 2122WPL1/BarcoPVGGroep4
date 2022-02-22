using System;
using System.Collections.Generic;

namespace BarcoPVG.Model.db
{
    public partial class RqBarcoDivision
    {
        public string Afkorting { get; set; } = null!;
        public string? Alias { get; set; }
        public bool? Actief { get; set; }
    }
}
