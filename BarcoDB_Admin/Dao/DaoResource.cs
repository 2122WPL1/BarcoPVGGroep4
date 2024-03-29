﻿using BarcoDB_Admin.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace BarcoDB_Admin.Dao
{
    public class DaoResource : DAO
    {
        //Jarne

        //copies the data from DAO
        public DaoResource() : base()
        {

        }

        protected static readonly DaoResource _instanceResource = new();

        public static DaoResource InstanceResource()
        {
            return _instanceResource;
        }

        public PlResource GetResource(int id)
        {
            return _context.PlResources.FirstOrDefault(r => r.Id == id);
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

        //CRUD
        public void AddResource(PlResource resource)
        {
            _context.Add(resource);
            _context.SaveChanges();
        }

        public void UpdateResouce(PlResource resource)
        { 
            _context.Update(resource);
            _context.SaveChanges();
        }

        public void RemoveResource(PlResource req)
        {
            _context.Remove(req);
            _context.SaveChanges();
        }



    }
}
