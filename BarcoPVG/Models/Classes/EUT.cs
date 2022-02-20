using System;
using System.Collections.Generic;
using System.Text;

namespace BarcoPVG.Models.Classes
{
    // Bridge between UI and Datamodels
    // Use class so only one getter/setter/onPropertyChanged needs to be created
    public class EUT
    {
        // Variables
        public int IdRqDetail { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public string OmschrijvingEut { get; set; }
        public string PartNr { get; set; }

        // Tests to execute
        public bool EMC { get; set; }
        public bool ENV { get; set; }
        public bool REL { get; set; }
        public bool SAV { get; set; }
        public bool PCK { get; set; }
        public bool ECO { get; set; }

        // Constructor
        public EUT()
        {
            //anders komt datum van vandaag in datetimepicker te staan
            //AvailabilityDate = DateTime.Now;

            // Tests are not active on start
            EMC = false;
            ENV = false;
            REL = false;
            SAV = false;
            PCK = false;
            ECO = false;
        }
    }
}
