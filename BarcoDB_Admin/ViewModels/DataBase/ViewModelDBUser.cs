using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcoDB_Admin.Viewmodels;
using BarcoPVG.Models.Db;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    //Amy
     class ViewModelDBUser : AbstractViewModelBase
    {
        public ObservableCollection<Person> allUsers { get; set; }
        protected Person _selectedUser;

        public ViewModelDBUser() : base()
        {
            allUsers = new ObservableCollection<Person>();
            List<Person> users = _dao.GetAllUser();

            foreach (Person person in users)
            {
                allUsers.Add(person);
            }

            _selectedUser = new Person();
        }

       public Person SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnpropertyChanged();
            }
        }
    }
}
