using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;
using BarcoPVG.Viewmodels;
using BarcoPVG.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarcoPVG.ViewModels.Login
{
    public class ViewModelLogin : AbstractViewModelBase
    {
        private ICommand loginCommand, exitCommand;
        //public DelegateCommand MainWindowCommand { get; set; }

        public ViewModelLogin() 
        {
            //MainWindowCommand = new DelegateCommand(DisplayMainWindow);
        }

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new Command((param) => this.DisplayLogin(param));
                }
                return loginCommand;
            }
            set
            {
                loginCommand = value;
            }
        }

        public ICommand ExitCommand
        { 
            get 
            {
                if (exitCommand == null)
                {
                    exitCommand = new Command((param) => this.ExitCommandMethode());
                }
                return exitCommand; 
            }
        }

        private void ExitCommandMethode()
        {
            foreach (Window item in Application.Current.Windows)
            {
                item.Close();
            }
        }

        private void DisplayLogin(object param)
        {
            ObservableCollection<object> listParameter = (ObservableCollection<object>)param;
            List<Person> allUser = _dao.GetAllUser();
            Person loginPerson = null;

            foreach (Person person in allUser)
            {
                if (person.Voornaam == (string)listParameter[0] && person.Password == (string)listParameter[1])
                {
                    //MessageBox.Show("OK");
                    loginPerson = person;
                }
            }

            if (loginPerson != null)
            {
                _dao.LoginSucceedded(loginPerson);
                foreach (Window item in Application.Current.Windows)
                {
                    item.Hide();
                }
                DisplayMainWindow();
            }
            else
            {
                MessageBox.Show("User or/and Password is not correct");
            }
        }


        public void DisplayMainWindow()
        {         
            MainWindow mainw = new MainWindow();
            mainw.Show();
        }
    }
}
