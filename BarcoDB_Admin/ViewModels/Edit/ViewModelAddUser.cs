using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.Edit
{
    public class ViewModelAddUser : AbstractViewModelBase
    {
        protected Person _Person;

        public ViewModelAddUser() : base()
        {
        }

        public Person Person
        {
            get { return _Person; }
            set { _Person = value; OnpropertyChanged(); }
        }
    }
}
