using System;
using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoEUT : DAO
    {
        //Jarne
        //here comes all the data from EUT

        //copies the data from DAO
        protected DaoPerson _daoPerson = new();
        public DaoEUT() : base()
        {

        }
        protected static readonly DaoEUT _instanceEUT = new();

        public static DaoEUT InstanceEUT()
        {
            return _instanceEUT;
        }
        //MOHAMED
        //Matti
        // This function adds the input from the EUT part to the request object
        // We create local variables to address the fields of the corresponding tables
        // The combined object is eventually given to the context
        // <param name="request"></param>
        // <param name="eut"></param>
        public void AddEutToRqRequest(RqRequest request, EUT eut, string EutNr)
        {
            List<string> testDivision = new();

            //request.GrossWeight = request.GrossWeight == null ? string.Empty : request.GrossWeight;
            //request.NetWeight = request.NetWeight == null ? string.Empty : request.NetWeight;
            //request.EutPartnumbers = eut.PartNr;

            //We call the TestDivisionEutIsChecked function to check which testdivisions are checked
            TestDivisionEutIsChecked(eut, testDivision);

            // We link each testdivision to the corresponding id_request
            foreach (string testeut in testDivision)
            {
                // Find an existing detail - Kaat
                // THIS WILL GIVE ERRORS IF YOU HAVE MULTIPLE DETAILS FOR A GIVEN COMBO IN YOUR DB
                var detail = _context.RqRequestDetails.
                    Where(d => d.IdRequest == request.IdRequest && d.Testdivisie == testeut).
                    SingleOrDefault();

                // If no detail exists for this JR/TestDiv combination in the db
                // Possible if JR not yet in DB
                // Check rq list
                if (detail is null)
                {
                    detail = request.RqRequestDetails.
                    Where(d => d.IdRequest == request.IdRequest && d.Testdivisie == testeut).
                    SingleOrDefault();
                }

                // If still no detail exists for this JR/TestDiv combination
                // Create a new one - Kaat
                if (detail is null)
                {
                    detail = new RqRequestDetail();

                    detail.Testdivisie = testeut;
                    detail.Pvgresp = _daoPerson.GetPVGResp(testeut, request.BarcoDivision);

                    request.RqRequestDetails.Add(detail);
                }

                detail.Euts.Add(new Eut
                {
                    // Static added for now
                    OmschrijvingEut = "EUT" + EutNr,
                    AvailableDate = ((DateTime)eut.AvailabilityDate).Date
                });
            };
            _context.RqRequests.Add(request);
        }

        // Mohamed, Kaat
        public List<EUT> GetEut(RqRequest rq)
        {
            List<RqRequestDetail> rqDetailsForJR = _context.RqRequestDetails.Where(r => r.IdRequest == rq.IdRequest).ToList();
            List<EUT> EUTObjects = new();

            foreach (var detail in rqDetailsForJR)
            {
                List<Eut> eutsForDetail = _context.Euts.Where(e => e.IdRqDetail == detail.IdRqDetail).ToList(); ;

                var divisionBool = typeof(EUT).GetProperty(detail.Testdivisie);

                foreach (var eut in eutsForDetail)
                {
                    EUT selectedEUTObject = EUTObjects.SingleOrDefault(obj => obj.OmschrijvingEut == eut.OmschrijvingEut);

                    if (selectedEUTObject is null)
                    {
                        selectedEUTObject = new EUT
                        {
                            IdRqDetail = eut.IdRqDetail,
                            AvailabilityDate = eut.AvailableDate,
                            OmschrijvingEut = eut.OmschrijvingEut,
                        };
                        EUTObjects.Add(selectedEUTObject);
                    }
                    // Set division to true
                    divisionBool.SetValue(selectedEUTObject, true);
                }
            }
            return EUTObjects;
        }

        // This function checks which of the testdivision are checked via the user input
        // If a test division is selected, we store this data in the test division list
        // The user input is given via the eut object as a parameter
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
    }
}