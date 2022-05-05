using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    class ViewModelDBResources : AbstractViewModelBase
    {

        List<PlResource> _Resouces;
        PlResource _SelectedResouce;

        public List<PlResource> AllResources
        {
            get { return _Resouces; }
            set { _Resouces = value; }
        }

        public PlResource SelectedResouce
        { 
            get => _SelectedResouce; 
            set => _SelectedResouce = value; 
        }

        public ViewModelDBResources() : base()
        {
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
                if (MessageBox.Show( "Are you sure you want to delete this Resource?", "Delete Resource", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    _dao.RemoveResource(SelectedResouce);
                    Load();
                    OnpropertyChanged("AllResources");
                }
                else
                {
                    MessageBox.Show("This resource has not been deleted");
                }
                

            }
            else
            {
                MessageBox.Show("No user selected");
            }
            
        }
    }
}
