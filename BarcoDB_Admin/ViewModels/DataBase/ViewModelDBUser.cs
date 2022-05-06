using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BarcoDB_Admin.Dao;
using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using Prism.Commands;


namespace BarcoDB_Admin.ViewModels.DataBase
{
     class ViewModelDBUser : AbstractViewModelBase
    {
        DaoUser _dao = new DaoUser();
        public DelegateCommand DeleteUser { get; set; }
        public List<Person> AllUsers
        { 
            get;
            set;
        }
        public Person SelectedUser
        {
            get; set;
        }

        public ViewModelDBUser() : base()
        {
            DeleteUser = new DelegateCommand(deleteUserFromDB);
            Load();
        }

        public void Load()
        {
            AllUsers = _dao.GetAllUser();
        }
        public void deleteUserFromDB()
        {

            if (SelectedUser != null)
            {
                
                    if (MessageBox.Show( "Are you sure you want to delete this user?", "Delete User", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _dao.RemoveUser(SelectedUser);
                    Load();
                    OnpropertyChanged("AllUsers");
                    


                }
                else
                {
                    MessageBox.Show("This user has not been deleted");
                }
            }
            else
            {
                MessageBox.Show("No user selecteerd");
            }
        }
    }
}
