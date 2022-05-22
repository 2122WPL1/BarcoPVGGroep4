using BarcoDB_Admin.Models.Db;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    public class ViewModelDBUser : AbstractViewModelContainer
    {

        #region properties
        public DelegateCommand DeleteUser { get; set; }
        public List<Person> AllUsers { get ; set ; }
        public Person SelectedUser { get; set; } = null;
        #endregion

        public ViewModelDBUser() : base()
        {
            DeleteUser = new DelegateCommand(DeleteUserFromDB);
            Load();
        }

        protected List<Person> _allUsers;
        //protected Person _SelectedUser;//Amy

        public void Load()
        {
            AllUsers = new List<Person>();
            AllUsers = _daoUser.GetAllUser();
            OnpropertyChanged("AllUsers");
        }

        public void DeleteUserFromDB()
        {
            if (SelectedUser != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _daoUser.RemoveUser(SelectedUser);
                    Load();
                }
                else
                {
                    MessageBox.Show("This user has not been deleted");
                }
            }
            else
            {
                MessageBox.Show("No user selected");
            }
        }
    }
}