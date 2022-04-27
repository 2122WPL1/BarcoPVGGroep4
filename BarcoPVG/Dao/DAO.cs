using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using BarcoPVG.Models.Classes;
using BarcoPVG;
using BarcoPVG.Models;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    // SINGLETON PATTERN
    // Private constructor, static instance
    // Ensures only one DBconnection is opened at a time
    // Ensures connection is closed when not in use
    public class DAO
    {
        // Variables
        private BarcoContext _context;
        private static readonly DAO _instance = new(); 

        public BarcoUser BarcoUser { get; set; }

        // Calls an DAO instance
        public static DAO Instance()
        {
            return _instance;
        }

        // DAO Constructor - PRIVATE
        // Calls an instance from the Barco2021Context and stores this context in the current context
        private DAO()
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

        
        //Eakarach
        //Login
        public void LoginSucceedded(Person loginPerson)
        {
            string name = loginPerson.Voornaam;

            //Put division or to list if they have more than one division
            //string division = GetAllDivisions().Where(div => "TS" == loginPerson.Afkorting).ToString();

            //Put Function to give right the the user
            //string func = "";
            this.BarcoUser = new BarcoUser()
            {
                Name = name, 
                Division = "Super-Admin",
                Function = "DATA",
            };
        }
        

        /// <summary>
        /// Removes unsaved changed by replacing the context by a new instance
        /// </summary>
        /// Kaat
        public void RemoveChanges()
        {
            _context = new BarcoContext();
        }


        // LISTS

        // Returns list of all JRs
        public List<RqRequest> GetAllJobRequests()
        {
            return _context.RqRequests
                .Include(r => r.IdRequest)
                .ToList();

            //indien geen JR aanwezig in databanke moet je null sturen, an-ders bovenstaande query
            //return null;
        }

        // Returns list of all jobNatures
        public List<RqJobNature> GetAllJobNatures()
        {
            return _context.RqJobNatures.ToList();
        }

        // Returns list of all BarcoDivisions
        public List<RqBarcoDivision> GetAllDivisions()
        {
            return _context.RqBarcoDivisions.ToList();
        }

        // Returns list of all Equipment
        // Kaat
        public List<PlResource> GetResources()
        {
            return _context.PlResources.ToList();
        }

        // Returns list of Equipment for a given TestDivision
        // Kaat
        public List<PlResource> GetResources(string TestDivision)
        {
            var idList = _context.PlResourcesDivisions.Where(rd => rd.DivisionAfkorting == TestDivision).Select(rd => rd.ResourcesId).ToList();

            // try with mapping?
            var resourceList = new List<PlResource>();

            foreach (var id in idList)
            {
                resourceList.Add(GetResource(id));
            }

            return resourceList;
        }

        // Gets a resource by id
        // Kaat
        public PlResource GetResource(int id)
        {
            return _context.PlResources.SingleOrDefault(r => r.Id == id);
        }

        // Gets a resource by name
        public PlResource GetResource(string name)
        {
            return _context.PlResources.SingleOrDefault(r => r.Naam == name);
        }

        // JR CHANGES

        /// <summary>
        /// Gets a JR with user data autofilled
        /// Kaat
        /// </summary>
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

                NetWeight = Jr.NetWeight == null? string.Empty : Jr.NetWeight,
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


        //MOHAMED
        //Matti
        /// <summary>
        /// This function adds the input from the EUT part to the request object
        /// We create local variables to address the fields of the corresponding tables
        /// The combined object is eventually given to the context
        /// </summary>
        /// <param name="request"></param>
        /// <param name="eut"></param>
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
                    detail.Pvgresp = GetPVGResp(testeut, request.BarcoDivision);

                    request.RqRequestDetails.Add(detail);
                }

                detail.Euts.Add(new Eut
                {
                    // Static added for now
                    // TODO: Dynamic linking
                    OmschrijvingEut = "EUT" + EutNr,
                    AvailableDate = ((DateTime)eut.AvailabilityDate).Date

                });


            };
            _context.RqRequests.Add(request);
        }

        // INCOMPLETE
        // Finds RqRequest by ID, updates based on JR, and saves changes
        // Sends error message
        // TODO: update data stored in other tables
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

                if(rqo == null)
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
        // TODO: catch nullRefEx - Currently impossible due to selecting listitem on load
        
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
                    Battery = (bool)selectedRQ.Battery,
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
                    Battery = (bool)selectedRQ.Battery
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

        // Mohamed, Kaat
        public List <EUT> GetEut(RqRequest rq)
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

        // Approval

        /// <summary>
        /// Approved items will be displayed in the queue for the respective teams
        /// Creates a record in the Pl_planning table.
        /// </summary>
        public void ApproveRequest(int jrId)
        {
            var DetailList = RqDetail(jrId);
            var request = _context.RqRequests.SingleOrDefault(rq => rq.IdRequest == jrId);

            // List of unique test divisions checked in this JR
            var divisions = DetailList.Select(d => d.Testdivisie).Distinct().ToList(); // OVERBODIG

            // On approval, set JR number and request date
            request.JrNumber = $"JRDEV{request.IdRequest:D5}";
            request.RequestDate = DateTime.Now;
            request.JrStatus = "In Plan";

            // Create a new planning record for each unique division
            foreach (string division in divisions)
            {
                var planning = CreatePlPlanning(request, division);

                _context.Add(planning);
                    _context.SaveChanges(); //Sander: het approven van een job request zorgt voor een probleem met de databank primary key van Planning_PK en pl_Planning
                                        //een dubbele id
                                        // hij wilt een record aanmaken met hetzelfde id 0 ookal bestaad die al

            }
        }

        public void ApproveInternalRequest(int jrId)
        {
            var DetailList = RqDetail(jrId);
            var request = _context.RqRequests.SingleOrDefault(rq => rq.IdRequest == jrId);

            // List of unique test divisions checked in this JR
            var divisions = DetailList.Select(d => d.Testdivisie).Distinct().ToList(); // OVERBODIG

            // On approval, set JR number and request date
            request.JrNumber = $"INTRN{request.IdRequest:D5}";
            request.RequestDate = DateTime.Now;
            request.JrStatus = "In Plan";

            // Create a new planning record for each unique division
            foreach (string division in divisions)
            {
                var planning = new PlPlanning
                {
                    IdRequest = request.IdRequest,
                    JrNr = request.JrNumber,
                    Requestdate = request.RequestDate,
                    DueDate = null,
                    TestDiv = division,
                    TestDivStatus = "In plan",
                };
                _context.Add(planning);
                _context.SaveChanges();
            }
        }


        // Planning

        /// <summary>
        /// Returns list of all Plannings in database
        /// </summary>
        /// Kaat
        public List<PlPlanning> GetPlPlannings()
        {
            return _context.PlPlannings.ToList();
        }

        /// <summary>
        /// Returns list of all Plannings for a division
        /// </summary>
        /// Kaat
        public List<PlPlanning> GetPlPlannings(string division)
        {
            return _context.PlPlannings.Where(pl => pl.TestDiv == division).ToList();
        }


        // Creates and saves Plplanningskalender based on Test
        // Kaat
        public void CreateNewTest(Test test)
        {
            var jr = GetJR(test.RQId);

            var planningsKalender = new PlPlanningsKalender
            {
                IdRequest = jr.IdRequest,
                JrNr = jr.JrNumber,
                JrStatus = jr.JrStatus,
                Omschrijving = test.Description,
                Startdatum = test.StartDate,
                Einddatum = test.EndDate is null? test.StartDate : test.EndDate,
                Testdiv = test.TestDivision,
                Resources = GetResource(test.Resource).Id,
                TestStatus = test.Status
            };

            _context.Add(planningsKalender);
            _context.SaveChanges();
        }

        public Test GetTest(int testId)
        {
            // Find selected PlPlanningsKalender
            var dbTest = _context.PlPlanningsKalenders.SingleOrDefault(pl => pl.Id == testId);

            return GetTest(dbTest);
        }

        public Test GetTest(PlPlanningsKalender dbTest)
        {
            // Create new test based on PlPlanningsKalender
            var test = new Test
            {
                DbTestId = dbTest.Id,
                Description = dbTest.Omschrijving,
                RQId = dbTest.IdRequest,
                TestDivision = dbTest.Testdiv,
                StartDate = dbTest.Startdatum,
                EndDate = dbTest.Einddatum,
                Resource = GetResource(dbTest.Resources).Naam,
                Status = dbTest.TestStatus
            };
            return test;
        }

        // Finds PlPlanningsKalender by PlanningsKalenders ID, updates based on Test, and saves changes
        // Kaat
        public void UpdateTest(int id, Test test)
        {
            // Get existing PlPK
            var dbTest = _context.PlPlanningsKalenders.SingleOrDefault(pk => pk.Id == id);

            // Leave if test not found
            if (dbTest is null)
            {
                return;
            }

            dbTest.Omschrijving = test.Description;
            dbTest.Startdatum = test.StartDate;
            dbTest.Einddatum = test.EndDate;
            dbTest.Testdiv = test.TestDivision;
            dbTest.Resources = GetResource(test.Resource).Id;

            SaveChanges();
        }

        // Updates status of test
        // Kaat
        public void UpdateTestStatus(Test test)
        {
            // Get existing PlPK
            var dbTest = _context.PlPlanningsKalenders.SingleOrDefault(pk => pk.Id == test.DbTestId);

            // Leave if test not found
            if (dbTest is null)
            {
                return;
            }

            dbTest.TestStatus = test.Status;

            SaveChanges();
        }

        public void DeleteTest(int id)
        {
            // Get existing PlPK
            var dbTest = _context.PlPlanningsKalenders.SingleOrDefault(pk => pk.Id == id);

            // Leave if test not found
            if (dbTest is null)
            {
                return;
            }

            _context.Remove(dbTest);

            // Savechanges on saving of Tests
            // User can still cancel -> New DBContext, removing saved changes
            // SaveChanges();
        }

        // Finds all test linked to this JR Id
        // Kaat
        public List<Test> GetTestsForJR(int jrId)
        {
            var tests = new List<Test>();

            var planningsKalenders = _context.PlPlanningsKalenders.
                Where(pk => pk.IdRequest == jrId).
                ToList();


            foreach (var item in planningsKalenders)
            {
                var newTest = GetTest(item);
                tests.Add(newTest);
            }
            return tests;
        }

        // Finds all test linked to this JR Id and division
        // Not linked to plan so can't use plPlanning
        // Kaat
        public List<Test> GetTestsForJRAndDivision(int jrId, string testDivision)
        {
            var tests = new List<Test>();

            var planningsKalenders = _context.PlPlanningsKalenders.
                Where(pk => pk.IdRequest == jrId && pk.Testdiv == testDivision).
                ToList();


            foreach (var item in planningsKalenders)
            {
                var newTest = GetTest(item);
                tests.Add(newTest);
            }
            return tests;
        }

        // Finds all tests for a division linked to this JR id
        // Kaat
        public List<Test> GetTestsForJR(int jrId, string division)
        {
            var tests = new List<Test>();

            var planningsKalenders = _context.PlPlanningsKalenders.
                Where(pk => pk.IdRequest == jrId && pk.Testdiv == division).
                ToList();

            foreach (var item in planningsKalenders)
            {
                var newTest = GetTest(item);
                tests.Add(newTest);
            }
            return tests;
        }

        public List<Test> GetAllTests()
        {
            // Find selected PlPlanningsKalender
            var dbTests = _context.PlPlanningsKalenders.ToList();

            var uiTests = new List<Test>();

            foreach (var item in dbTests)
            {
                uiTests.Add(GetTest(item));
            }
            return uiTests;
        }

        /// <summary>
        /// Set JR status to Finished if all related plans are finished
        /// </summary>
        /// <param name="rqId"></param>
        public void SetRqStatusIfComplete(int rqId)
        {
            // Get all planning
            var planningList = _context.PlPlannings.Where(pl => pl.IdRequest == rqId).ToList();

            // Leave function if there is an unfinished planning
            if (planningList.Exists(pl => pl.TestDivStatus != "Finished"))
            {
                return;
            }
            // Get request
            var dbRq = _context.RqRequests.SingleOrDefault(rq => rq.IdRequest == rqId);

            dbRq.JrStatus = "Finished";

            SaveChanges();
        }


        public List<Test> GetAllTestsForDivision(string testDivision)
        {
            // Find selected PlPlanningsKalender
            var dbTests = _context.PlPlanningsKalenders.Where(plk => plk.Testdiv == testDivision).ToList();

            var uiTests = new List<Test>();

            foreach (var item in dbTests)
            {
                uiTests.Add(GetTest(item));
            }
            return uiTests;
        }

        // Kaat
        public bool IsResourceDoubleBooked(Test test)
        {
            if (test.Resource is null || test.StartDate is null)
            {
                return false;
            }
            var newStartDate = test.StartDate;

            // If there is no endDate, set startDate as EndDate
            var newEndDate = test.EndDate == null? newStartDate: test.EndDate;

            // get resource number
            int resourceID = GetResource(test.Resource).Id;

            // Get all uses of this resource
            var resourceUses = _context.PlPlanningsKalenders.Where(plk => plk.Resources == resourceID).ToList();

            // Get and remove plPlanningskalender related to test
            var thisDbTest = _context.PlPlanningsKalenders.SingleOrDefault(plk => plk.Id == test.DbTestId);
            resourceUses.Remove(thisDbTest);

            // If new pk enddate is before existing pk startdate
            // but new pk startdate is not before existing pk enddate

            //if (resourceUses.Exists(u => u.Einddatum >= startDate && u.Startdatum <= endDate))
            //{
            //    return true;
            //}

            foreach (var item in resourceUses)
            {
                bool one = item.Einddatum is null? item.Startdatum >= newStartDate: item.Einddatum >= newStartDate;
                bool two = newEndDate >= item.Startdatum;

                if (one && two)
                {
                    return true;
                }
            }
            return false;
        }


        // SAVING
        // Stores all data from GUI in DB
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
        //Mohamed
        public void FindAllJrLast24h()
        {
            List<RqRequest> rq = _context.RqRequests.Where(r => 
                r.RequestDate <= DateTime.Now&& 
                (r.RequestDate >= DateTime.Now.AddHours(-24))
            ).ToList();
        }
         
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