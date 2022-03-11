using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BarcoPVG.Views.DatabaseManagement
{
    /// <summary>
    /// Interaction logic for DatabaseManagementUserControl.xaml
    /// </summary>
    public partial class DatabaseManagementUserControl : Window
    {
        public DatabaseManagementUserControl()
        {
            InitializeComponent();
            lblName.Content = "";
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            lblName.Content = "Add User";
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            lblName.Content = "Remove User";
        }

        private void btnAddResources_Click(object sender, RoutedEventArgs e)
        {
            lblName.Content = "Add Resources";
        }

        private void btnRemoveResources_Click(object sender, RoutedEventArgs e)
        {
            lblName.Content = "Remove Resources";
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
