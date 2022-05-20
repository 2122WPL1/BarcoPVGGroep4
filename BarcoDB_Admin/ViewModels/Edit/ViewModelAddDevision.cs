using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.Edit
{
    public class ViewModelAddDevision : AbstractViewModelContainer
    {
        public ViewModelAddDevision() : base()
        {
            Division = new RqBarcoDivision();
        }
    }
}
