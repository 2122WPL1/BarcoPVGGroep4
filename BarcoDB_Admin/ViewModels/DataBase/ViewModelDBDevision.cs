using BarcoDB_Admin.ViewModels;
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
            _AllDivisions = _dao.GetAllDivisions();
        }
    }
}
