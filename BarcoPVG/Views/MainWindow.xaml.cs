using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BarcoPVG.Models.Classes;
using BarcoPVG.Viewmodels;

namespace BarcoPVG.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsLoggedIn { get; set; } = false;
        
        public MainWindow()
        // Global variables
        {
            InitializeComponent();

            //if a user is already logged in then the MainWindow pops-up 
            //else the Login screen pops-up
            //Jarne
            if (IsLoggedIn == true)
            {
                MainWindow;
            }
            else
            {
                Login;
            }

            DataContext = new ViewModelMain(); //user ingeven als parameter

        }
    }
}