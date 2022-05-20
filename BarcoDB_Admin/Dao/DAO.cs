using BarcoDB_Admin.Models.Classes;

namespace BarcoDB_Admin.Dao
{
    public class DAO
    {
        protected BarcoContext _context;
        protected static readonly DAO _instance = new();

        public BarcoUser BarcoUser { get; set; }

        public static DAO Instance()
        {
            return _instance;
        }

        protected DAO()
        {
            this._context = new BarcoContext();
            this.BarcoUser = new BarcoUser()
            {
                Name = "Bart",
                Functie = "DEV",
            };
        }

        //public List<string> GetDiv(Person loginPerson)
        //{
        //    List<string> output = new List<String>();
        //    List<RqBarcoDivisionPerson> listDiv = _context.RqBarcoDivisionPeople.ToList();
        //    foreach (RqBarcoDivisionPerson div in listDiv)
        //    {
        //        if (loginPerson.Afkorting == div.AfkPerson)
        //        {
        //            output.Add(div.AfkDevision);
        //        }
        //    }
        //    return output;
        //}

        //// Removes unsaved changed by replacing the context by a new instance
        //// Kaat
        //public void RemoveChanges()
        //{
        //    _context = new BarcoContext();
        //}
        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}
    }
}
