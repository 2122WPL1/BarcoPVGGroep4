﻿using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.ViewModels.DataBase;
using BarcoDB_Admin.ViewModels.Edit;
using Prism.Commands;
using System;
using System.Windows;

namespace BarcoDB_Admin.ViewModels
{
    //Amy
    internal class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;

        #region DelegateCommand
        public DelegateCommand Exit { get; set; }
        public DelegateCommand DisplayDatabaseUserCommand { get; set; }
        public DelegateCommand DisplayDataResourceCommand { get; set; }
        public DelegateCommand DisplayDataBaseDivisionCommand { get; set; }
        public DelegateCommand DisplayAddUserCommand { get; set; }
        public DelegateCommand DisplayEditUserCommand { get; set; }
        public DelegateCommand DisplayAddResourcesCommand { get; set; }
        public DelegateCommand DisplayEditResourcesCommand { get; set; }
        public DelegateCommand DisplayAddDivisionCommand { get; set; }
        public DelegateCommand DisplayEditDivisionCommand { get; set; }
        public DelegateCommand SaveUserCommand { get; set; }
        public DelegateCommand SaveResourcesCommand { get; set; }
        public DelegateCommand SaveDivisionCommand { get; set; }

        #endregion

        public ViewModelMain()
        {
            DisplayDatabaseUserCommand = new DelegateCommand(DisplayDatabaseUserStartup);
            DisplayDataResourceCommand = new DelegateCommand(DisplayDataResourceStartup);
            DisplayDataBaseDivisionCommand = new DelegateCommand(DisplayDataBaseDivisionStartup);
            DisplayAddUserCommand = new DelegateCommand(DisplayAddUserStartup);
            DisplayEditUserCommand = new DelegateCommand(DisplayEditUserStartup);
            DisplayAddResourcesCommand = new DelegateCommand(DisplayAddResourcesStartup);
            DisplayEditResourcesCommand = new DelegateCommand(DisplayEditResourcesStartup);
            DisplayAddDivisionCommand = new DelegateCommand(DisplayAddDivisionStartup);
            DisplayEditDivisionCommand = new DelegateCommand(DisplayEditDivisionStartup);

            Exit = new DelegateCommand(exit);
        }

        public void exit() //sluiten project
        {
            Environment.Exit(0);
        }

        public AbstractViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnpropertyChanged();
            }
        }

        #region views
        public void DisplayDatabaseUserStartup()
        {
            this.ViewModel = new ViewModelDBUser();
        }


        public void DisplayDataResourceStartup()
        {
            this.ViewModel = new ViewModelDBResources();
        }

        public void DisplayDataBaseDivisionStartup()
        {
            this.ViewModel = new ViewModelDBDevision();
        }

        //Amy , Eakarach
        //User Management
        public void DisplayAddUserStartup()
        {
            this.ViewModel = new ViewModelAddUser();
            SaveUserCommand = new DelegateCommand(InsertUser);
        }
        #endregion

        public void DisplayEditUserStartup()
        {
            //EditUserUserControl can only be opened when there is a User selected
            if (((ViewModelDBUser)this.ViewModel).SelectedUser != null)
            {
                var user = ((ViewModelDBUser)this.ViewModel).SelectedUser.Afkorting; // make a new variable to keep the selected user
                
                SaveUserCommand = new DelegateCommand(UpdateUser);

                this.ViewModel = new ViewModelEditUser(user);
            }
            else
            {
                MessageBox.Show("No user selected!");
            }
        }

        public void DisplayAddResourcesStartup()
        {
            this.ViewModel = new ViewModelAddResources();
            SaveResourcesCommand = new DelegateCommand(InsertResource);
        }

        public void DisplayEditResourcesStartup()
        {
            //Amy
            //EditResourcesUserControl can only be opened when there is a resource selected
            if (((ViewModelDBResources)this.ViewModel).SelectedResouce != null) 
            {
                var resource = ((ViewModelDBResources)this.ViewModel).SelectedResouce.Id;
                SaveResourcesCommand = new DelegateCommand(UpdateResource);

                this.ViewModel = new ViewModelEditResources(resource);
            }
            else
            {
                MessageBox.Show("No Resource selected!");
            }
        }

        public void DisplayAddDivisionStartup()
        {
            this.ViewModel = new ViewModelAddDevision();
            SaveDivisionCommand = new DelegateCommand(InsertDivision);
        }

        public void DisplayEditDivisionStartup()
        {
            if (((ViewModelDBDevision)this.ViewModel).SelectedDivision != null)
            {
                var devision = ((ViewModelDBDevision)this.ViewModel).SelectedDivision.Afkorting;
                SaveDivisionCommand = new DelegateCommand(UpdateDivision);

                this.ViewModel = new ViewModelEditDevision(devision);
            }
            else
            {
                MessageBox.Show("No Devision selected!");
            }
        }

        //CRU USER
        public void InsertUser()
        {

            Person person = ((ViewModelAddUser)this.ViewModel).Person;

            RqTestDevision testTeam = ((ViewModelAddUser)this.ViewModel).TestDivision;
            BarcoDivision divisions = ((ViewModelAddUser)this.ViewModel).BarcoDivisions;

            //cheks if all the required fields are filled in
            if (CheckRequirment(person))
            {

                //Check if they choos other function than TEST and fill testteam or division
                if (((ViewModelAddUser)this.ViewModel).Person.Functie != "TEST" && 
                    (((ViewModelAddUser)this.ViewModel).TestDivision.Afkorting != null || ((ViewModelAddUser)this.ViewModel).BarcoDivisions.IsNull == false))
                {
                    MessageBox.Show("Only TEST team can choose Testteam and Division");
                }
                else if (((ViewModelAddUser)this.ViewModel).Person.Functie == "TEST" &&
                         (((ViewModelAddUser)this.ViewModel).TestDivision.Afkorting == null ||
                          ((ViewModelAddUser)this.ViewModel).BarcoDivisions.IsNull == true))
                {
                    MessageBox.Show("Testeam should have team and division");
                }
                else
                {

                    //Add to TB Person
                    // if abbreviation is not filled in the it creates one with the first 2 letters of firstname en the last 2 letters of surname
                    if (person.Afkorting is null || person.Afkorting == "")
                    {
                        person.Afkorting = (person.Voornaam.Substring(0, 2) + person.Familienaam.Substring(person.Familienaam.Length - 2)).ToUpper();

                    }
                    else
                    {
                        // ensures that abbreviation is in uppercase
                        person.Afkorting = person.Afkorting.ToUpper();
                    }

                    // if email is not filled in then it creates email with First name and surname
                    if (person.Email is null || person.Email == "")
                    {
                        person.Email = (person.Voornaam + "." + person.Familienaam + "@barco.com").ToLower();
                    }

                    _daoUser.AddUser(person);

                    //Add to TB BarcoDivisionPerson
                    if (testTeam is not null)
                    {
                        _daoUser.AddBarcoDivisionPerson(person, divisions, testTeam);
                    }

                    MessageBox.Show($"{person.Voornaam} is added to Database");
                    DisplayDatabaseUserStartup(); // toont view DBUSerUserControl
                }
               
                
               
            }
            else
            {
                MessageBox.Show("please fill all required fields");
            }
        }

        public void UpdateUser() // update
        {
            Person person = ((ViewModelEditUser)this.ViewModel).Person;

            RqTestDevision testTeam = ((ViewModelEditUser)this.ViewModel).TestDivision;
            BarcoDivision divisions = ((ViewModelEditUser)this.ViewModel).BarcoDivisions;



            if (CheckRequirment(person))
            {
                if (((ViewModelEditUser)this.ViewModel).Person.Functie != "TEST" &&
                   (((ViewModelEditUser)this.ViewModel).TestDivision.Afkorting != null || ((ViewModelEditUser)this.ViewModel).BarcoDivisions.IsNull == false))
                {
                    MessageBox.Show("Only TEST team can choose Testteam and Division");
                }
                else if (((ViewModelEditUser)this.ViewModel).Person.Functie == "TEST" &&
                         (((ViewModelEditUser)this.ViewModel).TestDivision.Afkorting == null ||
                          ((ViewModelEditUser)this.ViewModel).BarcoDivisions.IsNull == true))
                {
                    MessageBox.Show("Testeam should have team and division");
                }
                else
                {

                    //Add to TB Person
                    // if abbreviation is not filled in the it creates one with the first 2 letters of firstname en the last 2 letters of surname
                    if (person.Afkorting is null || person.Afkorting == "")
                    {
                        person.Afkorting = (person.Voornaam.Substring(0, 2) + person.Familienaam.Substring(person.Familienaam.Length - 2)).ToUpper();

                    }
                    else
                    {
                        // ensures that abbreviation is in uppercase
                        person.Afkorting = person.Afkorting.ToUpper();
                    }

                    // if email is not filled in then it creates email with First name and surname
                    if (person.Email is null || person.Email == "")
                    {
                        person.Email = (person.Voornaam + "." + person.Familienaam + "@barco.com").ToLower();
                    }

                    _daoUser.EditUser(person, divisions, testTeam);

                    MessageBox.Show($"{person.Voornaam} is updated");
                    DisplayDatabaseUserStartup(); // toont view DBUSerUserControl
                }
            }
            else
            {
                MessageBox.Show("please fill all required fields");
            }
        }

        //CRU Division
        public void InsertDivision()
        {
            RqBarcoDivision div = ((ViewModelAddDevision)this.ViewModel).Division;
            //cheks if all the required fields are filled in
            if (CheckRequirment(div))
            {
                div.Actief = true;

                _daoDivision.AddDivision(div);
                DisplayDataBaseDivisionStartup();
            }
            else
            {
                MessageBox.Show("please fill all required fields");
            }

        }

        public void UpdateDivision()
        {
            RqBarcoDivision div = ((AbstractViewModelContainer)this.ViewModel).Division;

            //cheks if all the required fields are filled in
            if (CheckRequirment(div))
            {
                _daoDivision.UpdateDivision(div);
                
                DisplayDataBaseDivisionStartup();
            }
            else
            {
                MessageBox.Show("please fill all required fields");
            }
        }

        //CRU Resource
        public void InsertResource()
        {
            PlResource res = ((ViewModelAddResources)this.ViewModel).Resource;
            //cheks if all the required fields are filled in
            if (CheckRequirment(res))
            {
                res.KleurHex = res.KleurHex is null ? "" : res.KleurHex.ToString();

                _daoResource.AddResource(res);
                DisplayDataResourceStartup();
            }
            else
            {
                MessageBox.Show("please fill all required fields");
            }

        }

        public void UpdateResource() // update
        {
            PlResource res = ((AbstractViewModelContainer)this.ViewModel).Resource;

            //cheks if all the required fields are filled in
            if (CheckRequirment(res))
            {
                _daoResource.UpdateResouce(res);
                DisplayDataResourceStartup();
            }
            else
            {
                MessageBox.Show("please fill all required fields");
            }
        }

        private bool CheckRequirment(object input)
        {

            if (input is Person) // cheks if the input comes from Person
            {
                Person checkPerson = (Person)input;
                //Cheks if the required fields are all filled in 
                //If fields are empty return false
                if (checkPerson.Voornaam is null || checkPerson.Familienaam is null || checkPerson.Functie is null || checkPerson.Wachtwoord is null || 
                    checkPerson.Voornaam == "" || checkPerson.Familienaam == "" || checkPerson.Functie == "" || checkPerson.Wachtwoord == "")
                {
                    return false;
                }

            }
            else if (input is PlResource) // cheks if the input comes from PlResource
            {
                PlResource checkResource = (PlResource)input;
                //Cheks if the required fields are all filled in 
                //If fields are empty return false
                if (checkResource.Naam is null || checkResource.Naam == "" ||
                    checkResource.KleurRgb is null || checkResource.KleurRgb == "")
                {
                    return false;
                }
            }
            else if (input is RqBarcoDivision) //cheks if the input comes from Person
            {
                RqBarcoDivision checkDivision = (RqBarcoDivision)input;
                //Cheks if the required fields are all filled in 
                //If fields are empty return false
                if (checkDivision.Afkorting is null || checkDivision.Afkorting == "")
                {
                    return false;
                }
            }
            return true;
        }
    }
}