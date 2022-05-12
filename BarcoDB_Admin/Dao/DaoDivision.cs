using BarcoDB_Admin.Models.Db;
using System.Collections.Generic;
using System.Linq;

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

    }
}
