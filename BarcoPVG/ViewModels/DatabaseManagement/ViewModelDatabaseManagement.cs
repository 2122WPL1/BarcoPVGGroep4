using BarcoPVG.Viewmodels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarcoPVG.ViewModels.DatabaseManagement
{
    public class ViewModelDatabaseManagement : AbstractViewModelCollectionRQ
    {
        private AbstractViewModelBase _DataBase; //amy

        public AbstractViewModelBase DataBase
        {
            get => _DataBase;
            set
            {
                _DataBase = value;
                OnpropertyChanged();
            }
        }
 
        public DelegateCommand DisplayDatabaseUserCommand { get; set; }
        public DelegateCommand DisplayDataResourceCommand { get; set; }
        public DelegateCommand DisplayDatabaseDivisionCommand { get; set; }
        public ViewModelDatabaseManagement() : base()
        {

            //DisplayDataResourceCommand = new DelegateCommand(DisplayDataBaseResourceStartup);

            //DisplayDatabaseManagementStartupCommand = new DelegateCommand(DisplayDatabaseManagementStartup);
            //DisplayDatabaseUserCommand = new DelegateCommand(DisplayDatabaseUserStartup);
            //DisplayDatabaseDivisionCommand = new DelegateCommand(DisplayDatabaseDivisionStartup);
            //functie of delegatecommand niet gevonden
        }
        //public void DisplayDatabaseManagement()
        //{
        //    this.DataBase = new ViewModelDBUser();
        //}
        //public void DisplayDataBaseResourceStartup()
        //{
        //    this.DataBase = new ViewModelDBResource();
        //}
        //public void DisplayDatabaseDivisionStartup()
        //{
        //    this.DataBase = new ViewModelDBDevision();
        //}

    }
}
