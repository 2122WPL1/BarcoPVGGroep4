using System;
using System.Collections.Generic;

namespace BarcoPVG.Models.Db
{
    public partial class RqBarcoDivisionPerson
    {
        public int Id { get; set; }
        public string AfkDevision { get; set; } = null!;
        public string AfkPerson { get; set; } = null!;
        public string Pvggroup { get; set; } = null!;
    }
}
