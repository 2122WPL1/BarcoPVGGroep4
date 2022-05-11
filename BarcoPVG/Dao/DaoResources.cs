using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoResources : DAO
    {
        //Jarne
        //here comes all the data from Resources

        //copies the data from DAO
        public DaoResources() : base()
        {

        }
        protected static readonly DaoResources _instanceResources = new();

        public static DaoResources InstanceResources()
        {
            return _instanceResources;
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
    }
}
