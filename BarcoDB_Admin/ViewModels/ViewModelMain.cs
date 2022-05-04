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

namespace BarcoDB_Admin.ViewModels
{
    //Amy
    internal class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;


        public DelegateCommand DisplayDatabaseUserCommand { get; set; }
        public DelegateCommand DisplayDataResourceCommand { get; set; }
        public DelegateCommand DisplayDataBaseDivisionCommand { get; set; }
        public DelegateCommand DisplayAddUserCommand { get; set; }
        public DelegateCommand DisplayEditUserCommand { get; set; }



        public ViewModelMain()
        {
            DisplayDatabaseUserCommand = new DelegateCommand(DisplayDatabaseUserStartup);
            DisplayDataResourceCommand = new DelegateCommand(DisplayDataResourceStartup);
            DisplayDataBaseDivisionCommand = new DelegateCommand(DisplayDataBaseDivisionStartup);
            DisplayAddUserCommand = new DelegateCommand(DisplayAddUserStartup);
            DisplayEditUserCommand = new DelegateCommand(DisplayEditUserStartup);
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
            this.ViewModel = new ViewModelEditUser();
        }

        public void DisplayEditUserStartup()
        {
            this.ViewModel = new ViewModelEditUser();
        }
    }
}
