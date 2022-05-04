using System;
using System.Collections.Generic;
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

        public List<Person> AllUsers 
        { 
            get => _AllUsers; 
            set => _AllUsers = value; 
        }

        public ViewModelDBUser() : base()
        {
            Load();

        }

        private void Load()
        {
            _AllUsers = _dao.GetAllUser();
        }
    }
}
