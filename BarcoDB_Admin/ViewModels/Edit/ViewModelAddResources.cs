using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.Edit
{
    
    public class ViewModelAddResources : AbstractViewModelContainer
    {
        public ViewModelAddResources() : base()
        {
            Resource = new PlResource();
        }
    }
}