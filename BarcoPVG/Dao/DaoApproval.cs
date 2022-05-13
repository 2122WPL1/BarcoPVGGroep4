using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoApproval : DAO
    {
        //Jarne
        //here comes all the data from Person
        protected DaoPlanning _daoPlanning = new();
        protected DaoJR daoJR = new();
        //copies the data from DAO
        public DaoApproval() : base()
        {

        }

        protected static readonly DaoApproval _instanceApproval = new();

        public static DaoApproval InstanceApproval()
        {
            return _instanceApproval;
        }

        // Approved items will be displayed in the queue for the respective teams
        // Creates a record in the Pl_planning table.
        public void ApproveRequest(int jrId)
        {
            var DetailList = daoJR.RqDetail(jrId);
            var request = _context.RqRequests.SingleOrDefault(rq => rq.IdRequest == jrId);

            
            
            // On approval, set JR number and request date
            request.JrNumber = $"JRDEV{request.IdRequest:D5}";
            request.RequestDate = DateTime.Now;
            request.JrStatus = "In Plan";

            // List of unique test divisions checked in this JR
            var divisions = DetailList.Select(d => d.Testdivisie).Distinct().ToList(); // OVERBODIG
            // Create a new planning record for each unique division
            foreach (string division in divisions)
            {
                bool msg = false;

                var planning = _daoPlanning.CreatePlPlanning(request, division);
                int id = planning.IdPlanning;
            jump:
                try //foutafhandeling
                {
                    _context.PlPlannings.Add(planning);
                    _context.SaveChanges();
                    if (msg)
                    {
                        MessageBox.Show("the original ID is " + id + ", because there was an error the id has been changed to " + planning.IdPlanning);
                    }
                }
                //DbUpdateException can not be used because there is a conflict
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
            var DetailList = daoJR.RqDetail(jrId);
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

        //Mati//Kaat//Mohamed
        public void PrintPvg(int idrequest, JR jr)
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
