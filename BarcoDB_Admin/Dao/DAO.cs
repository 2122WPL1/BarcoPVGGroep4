using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin;
using BarcoDB_Admin.Models;
using BarcoDB_Admin.Models.Db;
using System.Data.SqlClient;

namespace BarcoDB_Admin.Dao
{
    public class DAO
    {
        // Variables
        protected BarcoContext _context;
        protected static readonly DAO _instance = new();


        
        public BarcoUser BarcoUser { get; private set; }
        // Calls an DAO instance
        public static DAO Instance()
        {
            return _instance;
           
        }

        // DAO Constructor - PRIVATE
        // Calls an instance from the Barco2021Context and stores this context in the current context
        protected DAO()
        {
            this._context = new BarcoContext();
            //this.BarcoUser = RegistryConnection.GetValueObject<BarcoUser>(@"SOFTWARE\VivesBarco\Test");
            this.BarcoUser = new BarcoUser()
            {
                Name = "Super-Admin",
                Division = "Super-Admin",
                Function = "DATA",
            };
            
        }

       public void RemoveUser(Person person)
        {
            
            _context.Remove(person);
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

        /// <summary>
        /// Removes unsaved changed by replacing the context by a new instance
        /// </summary>
        /// Kaat
        public void RemoveChanges()
        {
            _context = new BarcoContext();
        }


        // LISTS
        // Eakarach
        // Returns list of all user
        public List<Person> GetAllUser()
        {
            return _context.People.ToList();
        }

        // Returns list of all JRs
        //public List<RqRequest> GetAllJobRequests()
        //{
        //    return _context.RqRequests
        //        .Include(r => r.IdRequest)
        //        .ToList();

        //    //indien geen JR aanwezig in databanke moet je null sturen, an-ders bovenstaande query
        //    //return null;
        //}

        // Returns list of all jobNatures
        //public List<RqJobNature> GetAllJobNatures()
        //{
        //    return _context.RqJobNatures.ToList();
        //}

        // Returns list of all BarcoDivisions
        public List<RqBarcoDivision> GetAllDivisions()
        {
            return _context.RqBarcoDivisions.ToList();
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



        
        // Kaat

        // SAVING
        // Stores all data from GUI in DB
        public void SaveChanges()
        {
            _context.SaveChanges(); //Sander: fout bij het aaanmaken van een JR (database probleem)
        }

        /// <summary>
        /// This function creates a list of rqRequestDetails objects that are linked to the given idRequest via the parameter
        /// </summary>
        /// <param name="idrequest"></param>
        /// <returns></returns>
        
    }
}
