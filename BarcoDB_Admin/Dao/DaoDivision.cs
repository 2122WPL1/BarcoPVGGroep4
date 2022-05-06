using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.Dao
{
    public class DaoDivision : DAO
    {
        public DaoDivision() : base()
        {

        }
        public List<RqBarcoDivision> GetAllDivisions()
        {
            return _context.RqBarcoDivisions.ToList();
        }
        public void RemoveDivision(RqBarcoDivision div)
        {
            _context.Remove(div);
            _context.SaveChanges();
        }
        public void GetDiv(Person loginPerson)
        {
            List<RqBarcoDivision> listDiv = GetAllDivisions();
            foreach (RqBarcoDivision div in listDiv)
            {
                //if (div.Afkorting == loginPerson)
                //{

                //}
            }
        }
    }
}
