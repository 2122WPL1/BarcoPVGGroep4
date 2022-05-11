using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using BarcoDB_Admin.ViewModels.DataBase;
using BarcoDB_Admin.ViewModels.Edit;
using Prism.Commands;
using System;
using System.Windows;

namespace BarcoDB_Admin.ViewModels
{
    //Amy
    internal class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;
        #region Commands
        public DelegateCommand Exit { get; set; }
        public DelegateCommand DisplayDatabaseUserCommand { get; set; }
        public DelegateCommand DisplayDataResourceCommand { get; set; }
        public DelegateCommand DisplayDataBaseDivisionCommand { get; set; }
        public DelegateCommand DisplayAddUserCommand { get; set; }
        public DelegateCommand DisplayEditUserCommand { get; set; }
        public DelegateCommand DisplayAddResourcesCommand { get; set; }
        public DelegateCommand DisplayEditResourcesCommand { get; set; }
        public DelegateCommand DisplayAddDivisionCommand { get; set; }
        public DelegateCommand DisplayEditDivisionCommand { get; set; }
        public DelegateCommand SaveUserCommand { get; set; }
        public DelegateCommand SaveResourcesCommand { get; set; }



        public ViewModelMain()
        {
            DisplayDatabaseUserCommand = new DelegateCommand(DisplayDatabaseUserStartup);
            DisplayDataResourceCommand = new DelegateCommand(DisplayDataResourceStartup);
            DisplayDataBaseDivisionCommand = new DelegateCommand(DisplayDataBaseDivisionStartup);
            DisplayAddUserCommand = new DelegateCommand(DisplayAddUserStartup);
            DisplayEditUserCommand = new DelegateCommand(DisplayEditUserStartup);
            DisplayAddResourcesCommand = new DelegateCommand(DisplayAddResourcesStartup);
            DisplayEditResourcesCommand = new DelegateCommand(DisplayEditResourcesStartup);
            DisplayAddDivisionCommand = new DelegateCommand(DisplayAddDivisionStartup);
            DisplayEditDivisionCommand = new DelegateCommand(DisplayEditDivisionStartup);
 

            Exit = new DelegateCommand(exit);
        }
        public void exit() //sluiten project
        {
            Environment.Exit(0);
        }
        public AbstractViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnpropertyChanged();
            }
        }
        #region Schermen
        public void DisplayDatabaseUserStartup()
        {
            this.ViewModel = new ViewModelDBUser();
        }
        public void DisplayDataResourceStartup()
        {
            this.ViewModel = new ViewModelDBResources();
        }
        public void DisplayDataBaseDivisionStartup()
        {
            this.ViewModel = new ViewModelDBDevision();
        }
        public void DisplayAddUserStartup()
        {
            SaveUserCommand = new DelegateCommand(InsertUser);
            this.ViewModel = new ViewModelAddUser();
        }

        public void DisplayEditUserStartup()
        {
            var user = ((ViewModelDBUser)this.ViewModel).SelectedUser.Afkorting;
            SaveUserCommand = new DelegateCommand(UpdateUser);

            if (user != null)//EditUserUserControl can only be opened when there is a User selected
            {
                this.ViewModel = new ViewModelEditUser(user);
            }
            else
            {
                MessageBox.Show("No user selected!");
            }
        }

        public void DisplayAddResourcesStartup()
        {
            SaveResourcesCommand = new DelegateCommand(InsertResources);
            this.ViewModel = new ViewModelAddResources();
        }
        public void DisplayEditResourcesStartup()
        {

            var resource = ((ViewModelDBResources)this.ViewModel).SelectedResouce.Id;

            if (resource != null) //EditResourcesUserControl can only be opened when there is a resource selected
            {
                 this.ViewModel = new ViewModelEditResources(resource);
            }
            else
            {
                MessageBox.Show("No Resource selected!");
            }
        }
        public void DisplayAddDivisionStartup()
        {
            this.ViewModel = new ViewModelAddDevision();
        }
        public void DisplayEditDivisionStartup()
        {
            var devision = ((ViewModelDBDevision)this.ViewModel).SelectedDivision.Afkorting;
            if (devision != null)//EditDevisionUserControl can only be opened when there is a devision selected
            {
                this.ViewModel = new ViewModelEditDevision(devision);
            }
            else
            {
                MessageBox.Show("No Devision selected!");

            }
        }

        public void InsertUser()
        {
            Person person = ((AbstractViewModelContainer)this.ViewModel).Person;


            _dao.AddUser(person);
            //this.ViewModel = new ViewModelAddUser();
            DisplayDatabaseUserStartup();

        }

        public void UpdateUser()
        {
                //_dao.EditUser(((ViewModelEditUser)this.ViewModel).Person);
            Person person = ((AbstractViewModelContainer)this.ViewModel).Person;

            //var person = ((ViewModelEditUser)this.ViewModel).SelectedUser;
            

            _dao.EditUser(person);
        }

        public void InsertResources()
        {
            //var resource = ((ViewModelAddResources)this.ViewModel);

            //_dao.AddResource(resource);
        }

    }
}
