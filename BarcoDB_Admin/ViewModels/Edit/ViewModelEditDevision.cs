using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy
    public class ViewModelEditDevision : AbstractViewModelBase
    {
        public RqBarcoDivision SelectedDivision { get; set; }

        public ViewModelEditDevision(RqBarcoDivision devision) : base()
        {
            SelectedDivision = devision;
        }
    }
}
