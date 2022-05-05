using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using Prism.Commands;

namespace BarcoDB_Admin.ViewModels.DataBase
{
     class ViewModelDBUser : AbstractViewModelBase
    {
        public DelegateCommand DeleteUser { get; set; }

        private List<Person> _AllUsers;
        private Person _SelectedUser;

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
            DeleteUser = new DelegateCommand(deleteUserFromDB);
            Load();
        }

        public void Load()
        {
            _AllUsers = _dao.GetAllUser();
        }
        public void deleteUserFromDB()
        {
            if (SelectedUser != null)
            {
                _dao.RemoveUser(SelectedUser);
            }
            else
            {
                MessageBox.Show("Geen gebruiker geselecteerd");
            }
        }
    }
}
