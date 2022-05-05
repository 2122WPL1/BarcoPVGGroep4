//using BarcoPVG.Viewmodels;
//using BarcoPVG.ViewModels.DatabaseManagement;
using BarcoDB_Admin.Viewmodels;
using BarcoDB_Admin.ViewModels.DataBase;
using BarcoDB_Admin.ViewModels.Edit;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarcoDB_Admin.ViewModels
{
    //Amy
    internal class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;

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

        public void exit()
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
            this.ViewModel = new ViewModelAddUser();
        }

        public void DisplayEditUserStartup()
        {
            var user = ((ViewModelDBUser)this.ViewModel).SelectedUser;
            if (user != null)
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
            this.ViewModel = new ViewModelEditResources();
        }

        public void DisplayEditResourcesStartup()
        {
            this.ViewModel = new ViewModelEditResources();
        }

        public void DisplayAddDivisionStartup()
        {
            this.ViewModel = new ViewModelEditDevision();
        }

        public void DisplayEditDivisionStartup()
        {
            this.ViewModel = new ViewModelEditDevision();
        }

    }
}
