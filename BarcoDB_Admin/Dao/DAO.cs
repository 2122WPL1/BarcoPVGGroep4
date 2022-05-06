using System;
using System.Collections.Generic;
using System.Linq;
using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.Dao
{
    public class DAO
    {
        protected BarcoContext _context;
        protected static readonly DAO _instance = new();
        public BarcoUser BarcoUser { get; private set; }
        public static DAO Instance()
        {
            return _instance;
        }
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

        public List<string> GetDiv(Person loginPerson)
        {
            List<string> output = new List<String>();
            List<RqBarcoDivisionPerson> listDiv = _context.RqBarcoDivisionPeople.ToList();
            foreach (RqBarcoDivisionPerson div in listDiv)
            {
                if (loginPerson.Afkorting == div.AfkPerson)
                {
                    output.Add(div.AfkDevision);
                    
                }
            }
            return output;
        }

        // <summary>
        // Removes unsaved changed by replacing the context by a new instance
        // </summary>
        // Kaat
        public void RemoveChanges()
        {
            _context = new BarcoContext();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
