using BarcoDB_Admin.ViewModels;
using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy
     class ViewModelEditUser : AbstractViewModelContainer
    {

        ObservableCollection<Person> personDetails { get; set; }
        public ViewModelEditUser(string Afkorting) : base()
        {
            Person = _daoUser.GetAllUser().FirstOrDefault(x => x.Afkorting == Afkorting);
            
        }


    }
}
