using BarcoDB_Admin.Models.Classes;

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
        /// Kaat
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
