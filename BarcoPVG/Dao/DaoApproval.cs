using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoApproval : DAO
    {
        //Jarne
        //here comes all the data from Person
        protected DaoPlanning _daoPlanning = new();
        //copies the data from DAO
        public DaoApproval() : base()
        {

        }

        // Approved items will be displayed in the queue for the respective teams
        // Creates a record in the Pl_planning table.
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
                bool msg = false;

                var planning = _daoPlanning.CreatePlPlanning(request, division);
                int id = planning.IdPlanning;
            jump:
                try //foutafhandeling
                {
                    _context.Add(planning);
                    _context.SaveChanges();
                    //TODO //Sander: het approven van een job request zorgt voor een probleem met de databank primary key van Planning_PK en pl_Planning
                    //TODO een dubbele id. hij wil een record aanmaken met hetzelfde id ook al bestaat die al tijdelijk opgelost met een try catch
                    if (msg)
                    {
                        MessageBox.Show("Het originele ID was " + id + ", maar omdat deze een dubbel is van iets anders is deze verandered naar " + planning.IdPlanning);
                    }
                }
                //DbUpdateException kan niet worden gebruikt omdat er dan een conflict is
                catch (Exception)
                {
                    msg = true;
                    planning.IdPlanning = planning.IdPlanning + 1;

                    goto jump;
                }
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
    }
}
