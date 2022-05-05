using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;

namespace BarcoDB_Admin.ViewModels.DataBase
{
     class ViewModelDBUser : AbstractViewModelBase
    {
        private List<Person> _AllUsers;
        private Person _SelectedUser;//Amy

        public List<Person> AllUsers
        { 
            get => _AllUsers; 
            set =>_AllUsers = value; 
        }
        public Person SelectedUser
        {
            get => _SelectedUser;
            set => _SelectedUser = value;
        }

        public ViewModelDBUser() : base()
        {
            Load();
        }

        public void Load()
        {
            _AllUsers = _dao.GetAllUser();
        }
    }
}
