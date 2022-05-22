using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace BarcoDB_Admin.Dao
{
    public class DaoUser : DAO
    {
        //Jarne
        //here comes all the data from Person
        public List<Person> users = new List<Person>();

        //copies the data from DAO
        public DaoUser() : base()
        {

        }

        protected static readonly DaoUser _instanceUser = new();

        public static DaoUser InstanceUser()
        {
            return _instanceUser;
        }

        // LISTS
        // Eakarach
        // Returns list of all user
        public void RemoveUser(Person person)
        {
            _context.Remove(person);
            _context.SaveChanges();
        }
        
        public void AddUser(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
        }

        public void AddBarcoDivisionPerson(Person person, BarcoDivision div, RqTestDevision team)
        {
            var division = div.AllDivisions();
            for (int i = 0; i < division.Count; i++)
            {
                if (division[i] == true)
                {
                    var alldivision = _context.RqBarcoDivisions.ToList();
                    string divisionAfkorting = alldivision.ElementAt(i+1).Afkorting;

                    var barcoDivPerson = new RqBarcoDivisionPerson()
                    {
                        AfkDevision = divisionAfkorting,
                        AfkPerson = person.Afkorting,
                        Pvggroup = team.Afkorting
                    };

                    _context.Add(barcoDivPerson);
                    _context.SaveChanges();
                }
            }          
        }
        public void EditUser(Person person, BarcoDivision div, RqTestDevision team)
        {
            if (person.Functie == "TEST")
            {
                var barcoPerson = GetAllBarcoDivisionPersons().Where(t => t.AfkPerson == person.Afkorting);

                //this function still have to work
                //var division = div.AllDivisions();

                //for (int i = 0; i < division.Count; i++)
                //{
                //    if (division[i] == true)
                //    {
                //        var alldivision = _context.RqBarcoDivisions.ToList();
                //        string divisionAfkorting = alldivision.ElementAt(i + 1).Afkorting;

                //        var barcoDivPerson = new RqBarcoDivisionPerson()
                //        {
                //            AfkDevision = divisionAfkorting,
                //            AfkPerson = person.Afkorting,
                //            Pvggroup = team.Afkorting
                //        };

                //        _context.Add(barcoDivPerson);
                //        _context.SaveChanges();
                //    }
                //}

                foreach (var onePerson in barcoPerson)
                {
                    onePerson.Pvggroup = team.Afkorting;

                    _context.Update(onePerson);
                }


            }
            
            _context.Update(person);
            _context.SaveChanges();
        }

        public List<Person> GetAllUser()
        {
            return _context.People.ToList();
        }

        public List<RqBarcoDivisionPerson> GetAllBarcoDivisionPersons()
        {
            return _context.RqBarcoDivisionPeople.ToList();
        }

        public List<RqTestDevision> GetAllTestTeam()
        {
            return _context.RqTestDevisions.ToList();
        }


    }
}