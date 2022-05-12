using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.ViewModels.DataBase;
using BarcoDB_Admin.ViewModels.Edit;
using Prism.Commands;
using System;
using System.Windows;
using BarcoDB_Admin.Dao;

namespace BarcoDB_Admin.ViewModels
{
    //Amy
    internal class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;

        #region DelegateCommand
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
        public DelegateCommand SaveDivisionCommand { get; set; }

        #endregion

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

        #region views
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

        //Amy , Eakarach
        //User Management
        public void DisplayAddUserStartup()
        {
            this.ViewModel = new ViewModelAddUser();
            SaveUserCommand = new DelegateCommand(InsertUser);
        }
        #endregion

        public void DisplayEditUserStartup()
        {
            //EditUserUserControl can only be opened when there is a User selected
            if (((ViewModelDBUser)this.ViewModel).SelectedUser != null)
            {
                var user = ((ViewModelDBUser)this.ViewModel).SelectedUser.Afkorting; // make a new variable to keep the selected user
                SaveUserCommand = new DelegateCommand(UpdateUser);

                this.ViewModel = new ViewModelEditUser(user);
            }
            else
            {
                MessageBox.Show("No user selected!");
            }
        }


        public void DisplayAddResourcesStartup()
        {
            this.ViewModel = new ViewModelAddResources();
            SaveResourcesCommand = new DelegateCommand(InsertResource);
        }

        public void DisplayEditResourcesStartup()
        {
            //Amy
            //EditResourcesUserControl can only be opened when there is a resource selected
            if (((ViewModelDBResources)this.ViewModel).SelectedResouce != null) 
            {
                var resource = ((ViewModelDBResources)this.ViewModel).SelectedResouce.Id;
                SaveResourcesCommand = new DelegateCommand(UpdateResource);

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
            SaveDivisionCommand = new DelegateCommand(InsertDivision);
        }

        public void DisplayEditDivisionStartup()
        {
            if (((ViewModelDBDevision)this.ViewModel).SelectedDivision != null)
            {
                var devision = ((ViewModelDBDevision)this.ViewModel).SelectedDivision.Afkorting;
                SaveDivisionCommand = new DelegateCommand(UpdateDivision);

                this.ViewModel = new ViewModelEditDevision(devision);
            }
            else
            {
                MessageBox.Show("No Devision selected!");
            }
        }

        //CRU USER
        public void InsertUser()
        {

            Person person = ((ViewModelAddUser)this.ViewModel).Person;

            _daoUser.AddUser(person);
            DisplayDatabaseUserStartup();

        }

        public void UpdateUser() // update
        {
            Person user = ((AbstractViewModelContainer)this.ViewModel).Person;

            _daoUser.EditUser(user);
            DisplayDatabaseUserStartup();
        }

        //CRU Division
        public void InsertDivision()
        {

            RqBarcoDivision div = ((ViewModelAddDevision)this.ViewModel).Division;

            _daoDivision.AddDivision(div);
            DisplayDataBaseDivisionStartup();
        }

        public void UpdateDivision()
        {
            RqBarcoDivision div = ((AbstractViewModelContainer)this.ViewModel).Division;

            _daoDivision.UpdateDivision(div);
            DisplayDataBaseDivisionStartup();
        }


        //CRU Resource
        public void InsertResource()
        {

            PlResource res = ((ViewModelAddResources)this.ViewModel).Resource;

            _daoResource.AddResource(res);
            DisplayDataResourceStartup();
        }

        public void UpdateResource() // update
        {
            PlResource res = ((AbstractViewModelContainer)this.ViewModel).Resource;

            _daoResource.UpdateResouce(res);
            DisplayDataResourceStartup();
        }
    }
}