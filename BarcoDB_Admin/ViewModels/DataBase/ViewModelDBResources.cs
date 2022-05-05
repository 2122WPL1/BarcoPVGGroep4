using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.ViewModels;
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
            _Resouces = _dao.GetResources();
        }
    }
}
