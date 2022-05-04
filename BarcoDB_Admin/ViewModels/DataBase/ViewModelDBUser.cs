using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using BarcoPVG.Models.Db;

namespace BarcoDB_Admin.ViewModels.DataBase
{
     class ViewModelDBUser : AbstractViewModelBase
    {
        private List<Person> _AllUsers;

        public List<Person> AllUsers 
        { 
            get => _AllUsers; 
            set => _AllUsers = value; 
        }

        public ViewModelDBUser() : base()
        {
            Load();
        }

       public Person SelectedUser
        {
            _AllUsers = _dao.GetAllUser();
        }
    }
}
