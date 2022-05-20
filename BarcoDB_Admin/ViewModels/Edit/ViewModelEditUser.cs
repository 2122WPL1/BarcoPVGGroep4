using BarcoDB_Admin.Models.Db;
using System.Collections.ObjectModel;
using System.Linq;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy,Eakarach
    class ViewModelEditUser : AbstractViewModelContainer
    {
        ObservableCollection<Person> personDetails { get; set; }
        public bool _IsEnable1;
        public bool IsEnable1 //ensure that primary can not be changed
        {
            get
            {
                return _IsEnable1;
            }
            set
            {
                _IsEnable1 = value;
                OnpropertyChanged();
            }
        }
        public ViewModelEditUser(string Afkorting) : base()
        {
            Person = _daoUser.GetAllUser().FirstOrDefault(x => x.Afkorting == Afkorting); //to be able to edit the selected person in the database
            IsEnable1 = false;
        }
    }
}
