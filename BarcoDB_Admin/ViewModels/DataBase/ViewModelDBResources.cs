using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    class ViewModelDBResources : AbstractViewModelBase
    {

       
        
        public DelegateCommand DeleteResource { get; set; }
        public List<PlResource> AllResources
        {
            get;
            set;
        }

        public PlResource SelectedResouce
        {
            get; set;
        }

        public ViewModelDBResources() : base()
        {
            DeleteResource = new DelegateCommand(DeleteResourceFromDB);
            Load();
        }

        private void Load()
        {
            AllResources = _dao.GetResources();
        }
        private void DeleteResourceFromDB()
        {
            if (SelectedResouce != null)
            {
                _dao.RemoveResource(SelectedResouce);
                Load();
                OnpropertyChanged("AllResources");

            }
            else
            {
                MessageBox.Show("Geen gebruiker geselecteerd");
            }
            
        }
    }
}
