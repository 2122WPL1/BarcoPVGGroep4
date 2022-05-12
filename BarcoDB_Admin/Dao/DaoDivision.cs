using BarcoDB_Admin.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace BarcoDB_Admin.Dao
{
    public class DaoDivision : DAO
    {
        //Jarne 

        //copies the data from DAO
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

        public void RemoveDivision(RqBarcoDivision div)
        {
            _context.Remove(div);
            _context.SaveChanges();
        }

        public RqBarcoDivision GetDivision()
        {
            RqBarcoDivision div = new RqBarcoDivision();
            return div;
        }

        //public void GetDiv(Person loginPerson)
        //{
        //    List<RqBarcoDivision> listDiv = GetAllDivisions();
        //    foreach (RqBarcoDivision div in listDiv)
        //    {
        //        //if (div.Afkorting == loginPerson)
        //        //{

        //        //}
        //    }
        //}
    }
}
