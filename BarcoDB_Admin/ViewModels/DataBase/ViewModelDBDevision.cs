using BarcoDB_Admin.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    class ViewModelDBDevision : AbstractViewModelBase
    {
        private List<RqBarcoDivision> _AllDivisions;
        private RqBarcoDivision _SelectedDivision;

        public List<RqBarcoDivision> AllDivisions
        {
            get { return _AllDivisions; }
            set { _AllDivisions = value; }
        }

        public RqBarcoDivision SelectedDivision
        { 
            get => _SelectedDivision; 
            set => _SelectedDivision = value; 
        }

        public ViewModelDBDevision() : base()
        {
            Load();
        }

        private void Load()
        {
            AllDivisions = _dao.GetAllDivisions();
        }
        private void DeleteResourceFromDB()
        {
            if (SelectedDivision != null)
            {
                if (MessageBox.Show( "Are you sure you want to delete this division?", "Delete division", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _dao.RemoveDivision(SelectedDivision);
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
