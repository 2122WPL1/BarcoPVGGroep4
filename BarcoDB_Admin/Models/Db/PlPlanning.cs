using System;

namespace BarcoDB_Admin.Models.Db
{
    public partial class PlPlanning
    {
        public int IdPlanning { get; set; }
        public int IdRequest { get; set; }
        public string? JrNr { get; set; }
        public DateTime? Requestdate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? TestDiv { get; set; }
        public string? TestDivStatus { get; set; }
        public DateTime? TestDivPlanDate { get; set; }

        public virtual RqRequest IdRequestNavigation { get; set; } = null!;
    }
}
