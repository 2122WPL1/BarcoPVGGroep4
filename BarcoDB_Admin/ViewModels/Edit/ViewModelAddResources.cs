using BarcoDB_Admin.Models.Db;

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