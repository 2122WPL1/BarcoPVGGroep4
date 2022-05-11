using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.ViewModels.Edit
{
    
    public class ViewModelAddResources : AbstractViewModelBase
    {
        private PlResource _PlResource { get; set; }

        public PlResource PlResource
        {
            get { return _PlResource; }
            set { _PlResource = value; OnpropertyChanged(); }
        }
    }

   
}
