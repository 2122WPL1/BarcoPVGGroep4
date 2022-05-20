using System;
using System.Collections.Generic;

namespace BarcoDB_Admin.Models.Db
{
    public partial class RqRequest
    {
        public RqRequest()
        {
            PlPlannings = new HashSet<PlPlanning>();
            PlPlanningsKalenders = new HashSet<PlPlanningsKalender>();
            RqOptionels = new HashSet<RqOptionel>();
            RqRequestDetails = new HashSet<RqRequestDetail>();
        }

        public int IdRequest { get; set; }
        public string? JrNumber { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? JrStatus { get; set; }
        /// <summary>
        /// initialen
        /// </summary>
        public string Requester { get; set; } = null!;
        /// <summary>
        /// uit keuzelijst
        /// </summary>
        public string BarcoDivision { get; set; } = null!;
        public string JobNature { get; set; } = null!;
        public string EutProjectname { get; set; } = null!;
        public string EutPartnumbers { get; set; } = null!;
        public string HydraProjectNr { get; set; } = null!;
        public DateTime ExpectedEnddate { get; set; }
        public bool? InternRequest { get; set; }
        public string? GrossWeight { get; set; } = null!;
        public string? NetWeight { get; set; } = null!;
        public bool Battery { get; set; }

        public virtual ICollection<PlPlanning> PlPlannings { get; set; }
        public virtual ICollection<PlPlanningsKalender> PlPlanningsKalenders { get; set; }
        public virtual ICollection<RqOptionel> RqOptionels { get; set; }
        public virtual ICollection<RqRequestDetail> RqRequestDetails { get; set; }
    }
}
