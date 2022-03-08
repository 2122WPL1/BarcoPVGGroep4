using System;
using System.Collections.Generic;

namespace BarcoPVG.Model.db
{
    public partial class RqBarcoDivisionPerson
    {
        public int Id { get; set; }
        public string AfkDevision { get; set; } = null!;
        public string AfkPerson { get; set; } = null!;
        public string Pvggroup { get; set; } = null!;
    }
}
