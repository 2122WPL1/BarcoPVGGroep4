using System.Windows;
using BarcoPVG.ViewModels.Login;

namespace BarcoPVG.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new ViewModelLogin(); //user ingeven als parameter
        }
    }
}