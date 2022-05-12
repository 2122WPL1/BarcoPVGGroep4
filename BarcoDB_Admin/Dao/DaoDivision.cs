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

        protected static readonly DaoDivision _instanceDivision = new();

        public static DaoDivision InstanceDivision()
        {
            return _instanceDivision;
        }

        public List<RqBarcoDivision> GetAllDivisions()
        {
            return _context.RqBarcoDivisions.ToList();
        }

        public void AddDivision(RqBarcoDivision div)
        {
            _context.Add(div);
            _context.SaveChanges();
        }

        public void UpdateDivision(RqBarcoDivision div)
        {
            _context.Update(div);
            _context.SaveChanges();
        }

        public void RemoveDivision(RqBarcoDivision div)
        {
            _context.Remove(div);
            _context.SaveChanges();
        }

    }
}
