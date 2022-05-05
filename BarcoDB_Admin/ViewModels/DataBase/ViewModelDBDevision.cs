using BarcoDB_Admin.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcoDB_Admin.Models.Db;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.DataBase
{
    class ViewModelDBDevision : AbstractViewModelBase
    {
        

        public List<RqBarcoDivision> AllDivisions
        {
            get; set;
        }

        public RqBarcoDivision SelectedDivision
        {
            get; set;
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
                _dao.RemoveDivision(SelectedDivision);
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
