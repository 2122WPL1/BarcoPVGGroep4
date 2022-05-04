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

        public List<PlResource> Resouces 
        {
            get
            {
                return _Resouces;
            }
            set
            {
                _Resouces = value;
            }
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
