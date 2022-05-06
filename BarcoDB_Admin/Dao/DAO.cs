using System;
using System.Collections.Generic;
using System.Linq;
using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.Dao
{
    public class DAO
    {
        protected BarcoContext _context;
        protected static readonly DAO _instance = new();
        public BarcoUser BarcoUser { get; private set; }
        public static DAO Instance()
        {
            return _instance;
        }
        protected DAO()
        {
            this._context = new BarcoContext();
            //this.BarcoUser = RegistryConnection.GetValueObject<BarcoUser>(@"SOFTWARE\VivesBarco\Test");
            this.BarcoUser = new BarcoUser()
            {
                Name = "Super-Admin",
                Division = "Super-Admin",
                Function = "DATA",
            };
        }

        public List<string> GetDiv(Person loginPerson)
        {
            List<string> output = new List<String>();
            List<RqBarcoDivisionPerson> listDiv = _context.RqBarcoDivisionPeople.ToList();
            foreach (RqBarcoDivisionPerson div in listDiv)
            {
                if (loginPerson.Afkorting == div.AfkPerson)
                {
                    output.Add(div.AfkDevision);
                    
                }
            }
            return output;
        }

        // <summary>
        // Removes unsaved changed by replacing the context by a new instance
        // </summary>
        // Kaat
        public void RemoveChanges()
        {
            _context = new BarcoContext();
        }
        public void SaveChanges()
        {
            _context.SaveChanges(); //Sander: fout bij het aaanmaken van een JR (database probleem)
        }

        /// <summary>
        /// This function creates a list of rqRequestDetails objects that are linked to the given idRequest via the parameter
        /// </summary>
        /// <param name="idrequest"></param>
        /// <returns></returns>
        public List<RqRequestDetail> RqDetail(int idrequest)
        {
            List<RqRequestDetail> DetailRQ = _context.RqRequestDetails.Where(rq => rq.IdRequest == idrequest).ToList();
            return DetailRQ;
        }

        /// <summary>
        /// This function checks which of the testdivision are checked via the user input
        /// If a test division is selected, we store this data in the test division list
        /// The user input is given via the eut object as a parameter
        /// </summary>
        private void TestDivisionEutIsChecked(EUT eut, List<string> testDivision)
        {
            // Kaat
            // Iterate over all properties of an EUT
            foreach (var property in typeof(EUT).GetProperties())
            {
                // Divisions are bools
                // Skip if the property is not a bool
                if (property.PropertyType == typeof(bool))
                {
                    // If the division is checked
                    if ((bool)property.GetValue(eut))
                    {
                        // Add the division to the list
                        testDivision.Add(property.Name);
                    }
                };
            }
        }

        /// <summary>
        /// Returns a PlPlanning for the given job request and division
        /// </summary>
        /// <param name="request">Job Request</param>
        /// <param name="division">Test team division</param>
        /// <returns>PlPlanning with request and division data</returns>
        /// Kaat
        private PlPlanning CreatePlPlanning(RqRequest request, string division)
        {
            var planning = new PlPlanning
            {
                IdRequest = request.IdRequest,
                JrNr = request.JrNumber,
                Requestdate = request.RequestDate,
                DueDate = request.RequestDate == null? request.RequestDate: ((DateTime)request.RequestDate).AddDays(5),
                TestDiv = division,
                TestDivStatus = "In plan", // use enums?
            };
            return planning;
        }

        /// <summary>
        /// Returns a string with the PVGResponsible(s)
        /// </summary>
        // Kaat
        private string GetPVGResp(string testDivision, string barcoDivision)
        {
            // Get the PVGResponsibles for this division combination
            // possibly more than one
            var responsiblesList = _context.RqBarcoDivisionPeople.
                Where(bpd => bpd.AfkDevision == barcoDivision && bpd.Pvggroup == testDivision).
                Select(bdp => bdp.AfkPerson);

            // Create a string from the list
            string responsiblesString = String.Join(", ", responsiblesList);

            return responsiblesString;
        }

        /*
        //Mohamed
        public void FindAllJrLast24h()
        {
            List<RqRequest> rq = _context.RqRequests.Where(r => 
                r.RequestDate <= DateTime.Now&& 
                (r.RequestDate >= DateTime.Now.AddHours(-24))
            ).ToList();
        }
        */

        //Mati//Kaat//Mohamed
        public void PrintPvg(int idrequest,JR jr)
        { 
            // Get the PVGResponsibles for this division combination
            // possibly more than one
            List<RqRequestDetail> listDetail =
                _context.RqRequestDetails.Where(rqdetail => rqdetail.IdRequest == idrequest).ToList();
            foreach (var item in listDetail)
            {
                var property = typeof(JR).GetProperty(item.Testdivisie + "pvg");

                if (property != null)
                {
                    property.SetValue(jr, item.Pvgresp);
                }
            }
        }
    }
}
