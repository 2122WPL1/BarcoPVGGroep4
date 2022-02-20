using System;
using System.Collections.Generic;

namespace BarcoPVG.Models.Db
{
    public partial class RqRequestDetail
    {
        public RqRequestDetail()
        {
            Euts = new HashSet<Eut>();
        }

        public int IdRqDetail { get; set; }
        public string Testdivisie { get; set; } = null!;
        public string? Pvgresp { get; set; }
        public int IdRequest { get; set; }

        public virtual RqRequest IdRequestNavigation { get; set; } = null!;
        public virtual RqTestDevision TestdivisieNavigation { get; set; } = null!;
        public virtual ICollection<Eut> Euts { get; set; }
    }
}
