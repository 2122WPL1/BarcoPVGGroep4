using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy
    public class ViewModelEditResources : AbstractViewModelBase
    {
        private PlResource _PlResource;
        public PlResource SelectedResouce { get; set; }

        public ViewModelEditResources(int Id) : base()
        {
            //SelectedResouce = resource;
            this._PlResource = _daoResource.GetResource(Id);
        }

        public PlResource PlResource
        {
            get { return _PlResource; }
            set { _PlResource = value; OnpropertyChanged(); }
        }
    }
}
