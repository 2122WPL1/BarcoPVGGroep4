using BarcoDB_Admin.Dao;
using BarcoDB_Admin.Models.Db;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    public class ViewModelDBUser : AbstractViewModelBase
    {
        DaoUser _dao = new DaoUser();

        #region properties
        public DelegateCommand DeleteUser { get; set; }
        public List<Person> AllUsers { get => _allUsers; set => _allUsers = value; }
        public Person SelectedUser { get; set; }
        #endregion

        public ViewModelDBUser() : base()
        {
            DeleteUser = new DelegateCommand(deleteUserFromDB);
            Load();
        }

        protected List<Person> _allUsers;
        //protected Person _SelectedUser;//Amy

        public void Load()
        {
            AllUsers = _dao.GetAllUser();
        }

        public void deleteUserFromDB()
        {
            if (SelectedUser != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
