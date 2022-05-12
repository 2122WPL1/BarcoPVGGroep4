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
            //SelectedDivision = division;
            this._RqBarcoDivision = _daoDivision.GetDivision();
        }

        public RqBarcoDivision RqBarcoDivision
        {
            get { return _RqBarcoDivision; }
            set { _RqBarcoDivision = value; OnpropertyChanged(); }
        }
    }
}
