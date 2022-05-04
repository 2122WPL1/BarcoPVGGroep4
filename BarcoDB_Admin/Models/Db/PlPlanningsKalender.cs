using System;
using System.Collections.Generic;

namespace BarcoDB_Admin.Models.Db
{
    public partial class PlPlanningsKalender
    {
        public int Id { get; set; }
        public int IdRequest { get; set; }
        public string? JrNr { get; set; }
        public string? JrStatus { get; set; }
        public string? Omschrijving { get; set; }
        public DateTime? Startdatum { get; set; }
        public DateTime? Einddatum { get; set; }
        public string? Testdiv { get; set; }
        public int Resources { get; set; }
        public string? TestStatus { get; set; }
        public string? Reserve { get; set; }

        public virtual RqRequest IdRequestNavigation { get; set; } = null!;
        public virtual PlResource ResourcesNavigation { get; set; } = null!;
    }
}
