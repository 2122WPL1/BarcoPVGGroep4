using BarcoDB_Admin.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace BarcoDB_Admin.Dao
{
    public class DaoResource : DAO
    {
        //copies the data from DAO
        public DaoResource() : base()
        {

        }

        protected static readonly DaoUser _instanceResource = new();

        public static DaoUser InstanceResource()
        {
            return _instanceResource;
        }

        public PlResource GetResource(int id)
        {
            return _context.PlResources.SingleOrDefault(r => r.Id == id);
        }

        public PlResource GetResource(string name)
        {
            return _context.PlResources.SingleOrDefault(r => r.Naam == name);
        }

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

        public List<PlResource> GetResources()
        {
            return _context.PlResources.ToList();
        }

        public void RemoveResource(PlResource req)
        {
            _context.Remove(req);
            _context.SaveChanges();
        }

        public void AddResource(string resource)
        {

        }

    }
}
