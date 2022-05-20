using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy
    public class ViewModelEditResources : AbstractViewModelContainer
    {
        public PlResource SelectedResouce { get; set; }

        public ViewModelEditResources(int Id) : base()
        {
            //SelectedResouce = resource;
            Resource = _daoResource.GetResource(Id);
        }

    }
}
