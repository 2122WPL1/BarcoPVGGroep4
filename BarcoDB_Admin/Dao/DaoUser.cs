using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.Dao
{
    public class DaoUser : DAO
    {
        // LISTS
        // Eakarach
        // Returns list of all user
        public void RemoveUser(Person person)
        {
            _context.Remove(person);
            _context.SaveChanges();
        }
        public List<Person> GetAllUser()
        {
            return _context.People.ToList();
        }
    }
}
