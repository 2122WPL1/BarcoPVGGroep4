using System;
using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoInternalJR : DAO
    {
        //Jarne
        //here comes all the data from Internal JR

        //copies the data from DAO
        protected static readonly DaoInternalJR _instanceInternalJR = new();
        DaoPlanning _instancePlanning = DaoPlanning.InstancePlanning();
        DaoLogin _instanceLogin = DaoLogin.InstanceLogin();


        public static DaoInternalJR InstanceInternalJR()
        {
            return _instanceInternalJR;
        }

        public DaoInternalJR() : base()
        {

        }

        public void AddInternJobRequest(JR Jr)
        {
            // Copy data from JR to new RqRequest
            // Used ternary operator to use String.Empty when null
            RqRequest rqrequest = new()
            {
                JrStatus = Jr.JrStatus == null ? "In Plan" : Jr.JrStatus,
                RequestDate = DateTime.Now,
                //RequestDate = (DateTime)Jr.ExpEnddate, // Nullable
                Requester = Jr.Requester == null ? string.Empty : Jr.Requester,
                BarcoDivision = Jr.BarcoDivision == null ? string.Empty : Jr.BarcoDivision,
                JobNature = Jr.JobNature == null ? string.Empty : Jr.JobNature,
                EutProjectname = Jr.EutProjectname == null ? string.Empty : Jr.EutProjectname,
                // EutPartnumbers = Jr.EutPartnr == null ? string.Empty : Jr.EutPartnr,
                HydraProjectNr = Jr.HydraProjectnumber == null ? string.Empty : Jr.HydraProjectnumber,

                ExpectedEnddate = Jr.ExpEnddate == null ? DateTime.Now : (DateTime)Jr.ExpEnddate, // Not nullable, so needs to be casted
                InternRequest = Jr.InternRequest == null ? false : Jr.InternRequest, // Bool, default false
                Battery = Jr.Battery == null ? false : Jr.Battery, // Bool, default false

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


            _context.Add(rqrequest);

            SaveChanges();

            //ApproveRequest(rqrequest.IdRequest);
            ApproveInternalRequest(rqrequest);

            //return rqrequest;
        }

        public void ApproveInternalRequest(RqRequest jrId)
        {
            //var DetailList = RqDetail(jrId);
            var request = jrId;// _context.RqRequests.SingleOrDefault(rq => rq.IdRequest == jrId);

            // List of unique test divisions checked in this JR

            // On approval, set JR number and request date
            request.JrNumber = $"INTRN{request.IdRequest:D5}";
            request.RequestDate = DateTime.Now;
            request.JrStatus = "In Plan";

            // Create a new planning record for each unique division

            var planning = _instancePlanning.CreatePlPlanning(request, _instanceLogin.BarcoUser.Division);

            _context.Add(planning);

            _context.SaveChanges();

        }
    }
}
