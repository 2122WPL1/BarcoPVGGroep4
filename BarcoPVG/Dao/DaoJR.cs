using System;
using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace BarcoPVG.Dao
{
    public class DaoJR : DAO
    {
        //Jarne
        //here comes all the data from JR

        //copies the data from DAO
        protected static readonly DaoJR _instanceJR = new();
        
        public static DaoJR InstanceJR()
        {
            return _instanceJR;
        }
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

        public DateTime Add5Datum()
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

        // INCOMPLETE
        // Finds RqRequest by ID, updates based on JR, and saves changes
        // Sends error message
        public string UpdateJobRequest(JR Jr)
        {
            string message = null; // message is null on success
            // Error control
            // JR Number not empty?
            if (Jr.BarcoDivision != null)
            {
                RqRequest rqrequest = _context.RqRequests.FirstOrDefault(r => r.IdRequest == Jr.IdRequest);

                rqrequest.JrNumber = Jr.JrNumber;
                rqrequest.JrStatus = Jr.JrStatus;
                rqrequest.RequestDate = (DateTime)Jr.RequestDate;
                rqrequest.Requester = Jr.Requester;
                rqrequest.BarcoDivision = Jr.BarcoDivision;
                rqrequest.JobNature = Jr.JobNature;
                rqrequest.EutProjectname = Jr.EutProjectname;
                rqrequest.EutPartnumbers = Jr.EutPartnr;
                rqrequest.HydraProjectNr = Jr.HydraProjectnumber;
                rqrequest.ExpectedEnddate = (DateTime)Jr.ExpEnddate; // Not nullable, so needs to be casted
                rqrequest.InternRequest = Jr.InternRequest;
                rqrequest.GrossWeight = Jr.GrossWeight;
                rqrequest.NetWeight = Jr.NetWeight;
                rqrequest.Battery = Jr.Battery;
                // Matti voorlopig
                // We create the rqo RqOptionel object to link the user data to the db data and saves the changes in the Barco database

                RqOptionel rqo = _context.RqOptionels.FirstOrDefault(o => o.IdRequest == Jr.IdRequest);

                if (rqo == null)
                {
                    rqo = new RqOptionel
                    {
                        Link = Jr.Link == null ? string.Empty : Jr.Link,
                        Remarks = Jr.Remarks == null ? string.Empty : Jr.Remarks,
                    };
                }

                //Sander: wss wanneer er een JR aangepast wordt zodat het wel optionele velden heeft komt er hier een crash
                //Sander: context heeft geen rqoptional
                rqo.Link = Jr.Link;
                rqo.Remarks = Jr.Remarks;
                // We combine the rqo and rqrequest objects
                rqrequest.RqOptionels.Add(rqo);
                _context.RqRequests.Update(rqrequest);
                SaveChanges();
            }
            else
            {
                message = "Error - empty job request\n" +
                    "Please fill in all necessary fields";
            }
            return message;
        }

        // INCOMPLETE
        // Gets existing JR by ID
        public JR GetJR(int idrequest) //Sander: Optionele JR velden geven geen error als ze inveguld zijn
        {
            // Find selected RqRequest
            RqRequest selectedRQ = _context.RqRequests.FirstOrDefault(rq => rq.IdRequest == idrequest);
            RqOptionel selectedRQO = _context.RqOptionels.FirstOrDefault(rqo => rqo.IdRequest == idrequest);
            JR selectedJR = null;

            if (selectedRQO != null)
            {
                // Create new JR with necessary data
                selectedJR = new()
                {
                    IdRequest = selectedRQ.IdRequest,
                    JrNumber = selectedRQ.JrNumber,
                    JrStatus = selectedRQ.JrStatus,
                    RequestDate = selectedRQ.RequestDate,
                    Requester = selectedRQ.Requester,
                    BarcoDivision = selectedRQ.BarcoDivision,
                    JobNature = selectedRQ.JobNature,
                    EutProjectname = selectedRQ.EutProjectname,
                    EutPartnr = selectedRQ.EutPartnumbers,
                    HydraProjectnumber = selectedRQ.HydraProjectNr,
                    ExpEnddate = selectedRQ.ExpectedEnddate,
                    InternRequest = selectedRQ.InternRequest,
                    GrossWeight = selectedRQ.GrossWeight,
                    NetWeight = selectedRQ.NetWeight,
                    Battery = selectedRQ.Battery,
                    //EutPartnr = selectedRQ.EutPartnumbers,

                    // Testing
                    Link = selectedRQO.Link,
                    Remarks = selectedRQO.Remarks,
                };
            }
            else
            {
                selectedJR = new JR
                {
                    IdRequest = selectedRQ.IdRequest,
                    JrNumber = selectedRQ.JrNumber,
                    JrStatus = selectedRQ.JrStatus,
                    RequestDate = selectedRQ.RequestDate,
                    Requester = selectedRQ.Requester,
                    BarcoDivision = selectedRQ.BarcoDivision,
                    JobNature = selectedRQ.JobNature,
                    EutProjectname = selectedRQ.EutProjectname,
                    EutPartnr = selectedRQ.EutPartnumbers,
                    HydraProjectnumber = selectedRQ.HydraProjectNr,
                    ExpEnddate = selectedRQ.ExpectedEnddate,
                    InternRequest = selectedRQ.InternRequest,
                    GrossWeight = selectedRQ.GrossWeight,
                    NetWeight = selectedRQ.NetWeight,
                    Battery = (bool)selectedRQ.Battery,
                };
            }
            return selectedJR;
        }

        public JR GetJR(RqRequest selectedRQ)
        {
            // Find related RqOptionel
            RqOptionel selectedRQO = _context.RqOptionels.FirstOrDefault(rqo => rqo.IdRequest == selectedRQ.IdRequest);

            // Create new JR with necessary data
            JR selectedJR = new()
            {
                IdRequest = selectedRQ.IdRequest,
                JrNumber = selectedRQ.JrNumber,
                JrStatus = selectedRQ.JrStatus,
                RequestDate = selectedRQ.RequestDate,
                Requester = selectedRQ.Requester,
                BarcoDivision = selectedRQ.BarcoDivision,
                JobNature = selectedRQ.JobNature,
                EutProjectname = selectedRQ.EutProjectname,
                EutPartnr = selectedRQ.EutPartnumbers,
                HydraProjectnumber = selectedRQ.HydraProjectNr,
                ExpEnddate = selectedRQ.ExpectedEnddate,
                InternRequest = selectedRQ.InternRequest,
                GrossWeight = selectedRQ.GrossWeight,
                NetWeight = selectedRQ.NetWeight,
                Battery = (bool)selectedRQ.Battery,
                //EutPartnr = selectedRQ.EutPartnumbers,

                // Testing
                Link = selectedRQO.Link,
                Remarks = selectedRQO.Remarks,
            };
            return selectedJR;
        }

        // Returns list of all JRs
        public List<RqRequest> GetAllJobRequests()
        {
            return _context.RqRequests
                //.Include(r => r.IdRequest)
                .ToList();

            //indien geen JR aanwezig in databanke moet je null sturen, an-ders bovenstaande query
            //return null;
        }

        // This function creates a list of rqRequestDetails objects that are linked to the given idRequest via the parameter
        // <param name="idrequest"></param>
        // <returns></returns>
        public List<RqRequestDetail> RqDetail(int idrequest)
        {
            List<RqRequestDetail> DetailRQ = _context.RqRequestDetails.Where(rq => rq.IdRequest == idrequest).ToList();
            return DetailRQ;
        }
    }
}
