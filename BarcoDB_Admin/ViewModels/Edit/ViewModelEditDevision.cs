using BarcoDB_Admin.Models.Db;

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

            //this._RqBarcoDivision = _daoUser.GetDevision(Afkorting);
        }

        public RqBarcoDivision RqBarcoDivision
        {
            get { return _RqBarcoDivision; }
            set { _RqBarcoDivision = value; OnpropertyChanged(); }
        }
    }
}
