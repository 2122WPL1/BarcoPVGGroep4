using System;
using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoJR : DAO
    {
        //Jarne
        //here comes all the data from JR

        //copies the data from DAO
        public DaoJR() : base()
        {

        }

        // Returns list of all jobNatures
        public List<RqJobNature> GetAllJobNatures()
        {
            return _context.RqJobNatures.ToList();
        }

        // JR CHANGES
        // Gets a JR with user data autofilled
        // Kaat
        public JR GetNewJR()
        {
            JR autofilledJR = new()
            {
                Requester = BarcoUser.Name,
                BarcoDivision = BarcoUser.Division
            };

            return autofilledJR;
        }

        // INCOMPLETE
        // Creates and saves RqRequest based on JR
        // TODO: save data stored in other tables
        public RqRequest AddJobRequest(JR Jr)
        {
            // Copy data from JR to new RqRequest
            // Used ternary operator to use String.Empty when null
            RqRequest rqrequest = new()
            {
                JrStatus = Jr.JrStatus == null ? "To approve" : Jr.JrStatus,
                RequestDate = Add5Datum(), // the JR has to be accepted within 5 non-holiday days.
                //RequestDate = (DateTime)Jr.ExpEnddate, // Nullable
                Requester = Jr.Requester == null ? string.Empty : Jr.Requester,
                BarcoDivision = Jr.BarcoDivision == null ? string.Empty : Jr.BarcoDivision,
                JobNature = Jr.JobNature == null ? string.Empty : Jr.JobNature,
                EutProjectname = Jr.EutProjectname == null ? string.Empty : Jr.EutProjectname,
                // EutPartnumbers = Jr.EutPartnr == null ? string.Empty : Jr.EutPartnr,
                HydraProjectNr = Jr.HydraProjectnumber == null ? string.Empty : Jr.HydraProjectnumber,

                ExpectedEnddate = Jr.ExpEnddate == null ? DateTime.Now : (DateTime)Jr.ExpEnddate, // Not nullable, so needs to be casted
                InternRequest = Jr.InternRequest, // Bool, default false
                Battery = Jr.Battery, // Bool, default false

                NetWeight = Jr.NetWeight == null ? string.Empty : Jr.NetWeight,
                GrossWeight = Jr.GrossWeight == null ? string.Empty : Jr.GrossWeight,
                EutPartnumbers = Jr.EutPartnr == null ? string.Empty : Jr.EutPartnr
            };


            //Matti voorlopig
            // We create a rqo object of the RqOptionel class to save the following fields in the database with the user input
            RqOptionel rqo = new()
            {
                Link = Jr.Link == null ? string.Empty : Jr.Link,
                Remarks = Jr.Remarks == null ? string.Empty : Jr.Remarks,

            };
            // We combine the rqo object with the rqrequest object and return the combined object
            rqrequest.RqOptionels.Add(rqo);

            return rqrequest;
        }

        private DateTime Add5Datum()
        {
            // Still have to look at holidays in Belgium****
            DateTime newDate = DateTime.Now;
            int fiveDays = 5;

            while (fiveDays > 0)
            {
                newDate = newDate.AddDays(1);

                if (newDate.DayOfWeek != DayOfWeek.Saturday && newDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    fiveDays -= 1;
                }
            }

            return newDate;
        }
    }
}
