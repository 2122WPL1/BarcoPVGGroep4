using System;
using System.Collections.Generic;

namespace BarcoPVG.Models.Db
{
    public partial class Eut
    {
        public int Id { get; set; }
        public int IdRqDetail { get; set; }
        public DateTime AvailableDate { get; set; }
        public string? OmschrijvingEut { get; set; }
        public virtual RqRequestDetail IdRqDetailNavigation { get; set; } = null!;
    }
}
