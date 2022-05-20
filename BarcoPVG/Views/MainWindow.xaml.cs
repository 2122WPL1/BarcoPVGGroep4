using System.Windows;
using BarcoPVG.ViewModels;

namespace BarcoPVG.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
            // Global variables
        {
            DataContext = new ViewModelMain(); //user ingeven als parameter

            InitializeComponent();
        }
    }
}