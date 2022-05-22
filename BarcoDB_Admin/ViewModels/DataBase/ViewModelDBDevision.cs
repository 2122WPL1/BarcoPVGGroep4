using System.Collections.Generic;
using BarcoDB_Admin.Models.Db;
using System.Windows;
using Prism.Commands;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    class ViewModelDBDevision : AbstractViewModelContainer
    {

        #region properties
        public List<RqBarcoDivision> AllDivisions { get; set; }
        public RqBarcoDivision SelectedDivision { get; set; } = null;
        public DelegateCommand DeleteDivision { get; set; }
        #endregion

        public ViewModelDBDevision() : base()
        {
            DeleteDivision = new DelegateCommand(DeleteDivisionFromDB);
            Load();
        }


        private void Load()
        {
            AllDivisions = _daoDivision.GetAllDivisions();
        }
        private void DeleteDivisionFromDB()
        {
            if (SelectedDivision != null)
            {
                if (MessageBox.Show( "Are you sure you want to delete this division?", "Delete division", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _daoDivision.RemoveDivision(SelectedDivision);
                    Load();
                    OnpropertyChanged("AllDivisions");
                }
                else
                {
                    MessageBox.Show("This division has not been deleted");
                }
            }
            else
            {
                MessageBox.Show("No division selected");
            }

        }
    }
}
