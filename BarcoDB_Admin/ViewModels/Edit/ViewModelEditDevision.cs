using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.ViewModels;
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
        private RqBarcoDivision _RqBarcoDivision;
        public RqBarcoDivision SelectedDivision { get; set; }

        public ViewModelEditDevision(string Afkorting) : base()
        {
            //SelectedDivision = devision;

            this._RqBarcoDivision = _dao.GetDevision(Afkorting);
        }

        public RqBarcoDivision RqBarcoDivision
        {
            get { return _RqBarcoDivision; }
            set { _RqBarcoDivision = value; OnpropertyChanged(); }
        }
    }
}
