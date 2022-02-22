using System;
using System.Collections.Generic;

namespace BarcoPVG.Model.db
{
    public partial class RqTestDevision
    {
        public RqTestDevision()
        {
            PlResourcesDivisions = new HashSet<PlResourcesDivision>();
            RqRequestDetails = new HashSet<RqRequestDetail>();
        }

        public string Afkorting { get; set; } = null!;
        public string? Naam { get; set; }

        public virtual ICollection<PlResourcesDivision> PlResourcesDivisions { get; set; }
        public virtual ICollection<RqRequestDetail> RqRequestDetails { get; set; }
    }

    public partial class CopyOfRqTestDevision
    {
        public CopyOfRqTestDevision()
        {
            PlResourcesDivisions = new HashSet<PlResourcesDivision>();
            RqRequestDetails = new HashSet<RqRequestDetail>();
        }

        public string Afkorting { get; set; } = null!;
        public string? Naam { get; set; }

        public virtual ICollection<PlResourcesDivision> PlResourcesDivisions { get; set; }
        public virtual ICollection<RqRequestDetail> RqRequestDetails { get; set; }
    }

    public partial class CopyOfCopyOfRqTestDevision
    {
        public CopyOfCopyOfRqTestDevision()
        {
            PlResourcesDivisions = new HashSet<PlResourcesDivision>();
            RqRequestDetails = new HashSet<RqRequestDetail>();
        }

        public string Afkorting { get; set; } = null!;
        public string? Naam { get; set; }

        public virtual ICollection<PlResourcesDivision> PlResourcesDivisions { get; set; }
        public virtual ICollection<RqRequestDetail> RqRequestDetails { get; set; }
    }
}
