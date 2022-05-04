using System;
using System.Collections.Generic;

namespace BarcoDB_Admin.Models.Db
{
    public partial class RqBarcoDivision
    {
        public string Afkorting { get; set; } = null!;
        public string? Alias { get; set; }
        public bool? Actief { get; set; }
    }
}
