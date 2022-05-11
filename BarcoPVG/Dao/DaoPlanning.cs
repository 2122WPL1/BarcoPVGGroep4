using System;
using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoPlanning : DAO
    {
        //Jarne
        //here comes all the data from Planning

        protected DaoJR daoJR = new();
        protected DaoResources _daoResources = new();

        //copies the data from DAO
        public DaoPlanning() : base()
        {
            
        }
        protected static readonly DaoPlanning _instancePlanning = new();

        public static DaoPlanning InstancePlanning()
        {
            return _instancePlanning;
        }

        // Returns list of all Plannings in database
        // Kaat
        public List<PlPlanning> GetPlPlannings()
        {
            return _context.PlPlannings.ToList();
        }

        // Returns list of all Plannings for a division
        // Kaat
        public List<PlPlanning> GetPlPlannings(string division)
        {
            return _context.PlPlannings.Where(pl => pl.TestDiv == division).ToList();
        }

        // Creates and saves Plplanningskalender based on Test
        // Kaat
        public void CreateNewTest(Test test)
        {
            var jr = daoJR.GetJR(test.RQId);

            var planningsKalender = new PlPlanningsKalender
            {
                IdRequest = jr.IdRequest,
                JrNr = jr.JrNumber,
                JrStatus = jr.JrStatus,
                Omschrijving = test.Description,
                Startdatum = test.StartDate,
                Einddatum = test.EndDate is null ? test.StartDate : test.EndDate,
                Testdiv = test.TestDivision,
                Resources = _daoResources.GetResource(test.Resource).Id,
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
                Resource = _daoResources.GetResource(dbTest.Resources).Naam,
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
            dbTest.Resources = _daoResources.GetResource(test.Resource).Id;

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

        /// Set JR status to Finished if all related plans are finished
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
            var newEndDate = test.EndDate == null ? newStartDate : test.EndDate;

            // get resource number
            int resourceID = _daoResources.GetResource(test.Resource).Id;

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
                bool one = item.Einddatum is null ? item.Startdatum >= newStartDate : item.Einddatum >= newStartDate;
                bool two = newEndDate >= item.Startdatum;

                if (one && two)
                {
                    return true;
                }
            }
            return false;
        }

        /// Returns a PlPlanning for the given job request and division
        /// <param name="request">Job Request</param>
        /// <param name="division">Test team division</param>
        /// <returns>PlPlanning with request and division data</returns>
        /// Kaat
        public PlPlanning CreatePlPlanning(RqRequest request, string division)
        {
            var planning = new PlPlanning //clustered?
            {
                IdRequest = request.IdRequest,
                JrNr = request.JrNumber,
                Requestdate = request.RequestDate,
                DueDate = request.RequestDate == null ? request.RequestDate : ((DateTime)request.RequestDate).AddDays(5),
                TestDiv = division,
                TestDivStatus = "In plan", // use enums?
            };
            return planning;
        }
    }
}
