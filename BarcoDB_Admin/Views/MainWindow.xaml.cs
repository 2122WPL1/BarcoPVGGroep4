using BarcoDB_Admin.ViewModels;
using System.Windows;

namespace BarcoDB_Admin.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new ViewModelMain();

            InitializeComponent();
        }
    }
}
