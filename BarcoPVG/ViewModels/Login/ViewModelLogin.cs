using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BarcoPVG.Models.Db;
using BarcoPVG.Views;

namespace BarcoPVG.ViewModels.Login
{
    //Eakarach
    public class ViewModelLogin : AbstractViewModelBase
    {
        private ICommand loginCommand, exitCommand;
        private string _username = "bas";
        private string _password = "aaa";

        public ViewModelLogin() 
        {

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

        public string Username 
        {
            get => _username;
            set
            {
                _username = value;
                OnpropertyChanged("Username");
            }
        }
        public string Password
        { 
            get => _password;
            set
            {
                _password = value;
                OnpropertyChanged("Password");
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
            List<Person> allUser = _daoPerson.GetAllUser();
            Person loginPerson = null;

            foreach (Person person in allUser)
            {
                if (person.Afkorting == ((string)listParameter[0]).ToUpper() && person.wachtwoord == (string)listParameter[1])
                {
                    //MessageBox.Show("OK");
                    loginPerson = person;
                }
            }

            if (loginPerson != null)
            {
                _daoLogin.LoginSucceeded(loginPerson);
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
