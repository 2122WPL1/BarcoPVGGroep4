using BarcoDB_Admin.Models.Db;

namespace BarcoDB_Admin.ViewModels.Edit
{
    public class ViewModelAddUser : AbstractViewModelContainer
    {

        public ViewModelAddUser() : base()
        {
            Person = new Person();
        }

    }
}
