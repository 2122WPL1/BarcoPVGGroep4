using System;
using System.Collections.Generic;
using System.Text;

namespace BarcoPVG.Models.Classes
{
    // Bridge between UI and Datamodels
    // Use class so only one getter/setter/onPropertyChanged needs to be created
    public class JR
    {

        // Required variables for the job request
        public int IdRequest { get; set; }
        public string JrNumber { get; set; }
        public string JrStatus { get; set; }
        public string Requester { get; set; }
        public string EutProjectname { get; set; }
        public string EutPartnr { get; set; }
        public string HydraProjectnumber { get; set; }
        public bool? InternRequest { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public bool Battery { get; set; }
        public string Link { get; set; }
        public string Remarks { get; set; }
        public string BarcoDivision { get; set; }
        public string JobNature { get; set; }
        public DateTime? ExpEnddate { get; set; }
        public DateTime? RequestDate { get; set; }

        // PVGResponsobles for this test
        public string EMCpvg { get; set; }
        public string ENVpvg { get; set; }
        public string RELpvg { get; set; }
        public string SAVpvg { get; set; }
        public string PCKpvg { get; set; }
        public string ECOpvg { get; set; }
    }
}
