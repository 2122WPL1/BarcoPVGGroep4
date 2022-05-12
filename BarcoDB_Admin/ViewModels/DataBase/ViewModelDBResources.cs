using BarcoDB_Admin.Dao;
using BarcoDB_Admin.Models.Db;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    class ViewModelDBResources : AbstractViewModelContainer
    {
        #region properties
        public DelegateCommand DeleteResource { get; set; }
        public List<PlResource> AllResources { get; set; }
        public PlResource SelectedResouce { get; set; } = null;
        #endregion
        public ViewModelDBResources() : base()
        {
            DeleteResource = new DelegateCommand(DeleteResourceFromDB);
            
            Load();
        }
        private void Load()
        {
            AllResources = _daoResource.GetResources();
        }
        private void DeleteResourceFromDB()
        {
            if (SelectedResouce != null)
            {
                if (MessageBox.Show( "Are you sure you want to delete this Resource?", "Delete Resource", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _daoResource.RemoveResource(SelectedResouce);
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
