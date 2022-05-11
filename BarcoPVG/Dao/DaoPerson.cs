using System;
using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoPerson : DAO
    {
        //Jarne
        //here comes all the data from Person

        //copies the data from DAO
        public DaoPerson() : DAO
        {

        }

        // LISTS
        // Eakarach
        // Returns list of all user
        public List<Person> GetAllUser()
        {
            return _context.People.ToList();
        }

        /// <summary>
        /// Returns a string with the PVGResponsible(s)
        /// </summary>
        // Kaat
        public string GetPVGResp(string testDivision, string barcoDivision)
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

        // Returns list of all BarcoDivisions
        public List<RqBarcoDivision> GetAllDivisions()
        {
            return _context.RqBarcoDivisions.ToList();
        }
    }
}
