using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Input;

namespace BarcoDB_Admin.Models.Classes
{
    // Bridge between UI and Datamodels
    // Use class so only one getter/setter/onPropertyChanged needs to be created
    public class EUT
    {
        public ICommand RemoveSingleEUTCommand { get; set; }
        // Variables
        public int IdRqDetail { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public string OmschrijvingEut { get; set; }
        public string PartNr { get; set; }

        // Tests to execute
        public bool EMC { get; set; }
        public bool ENV { get; set; }
        public bool REL { get; set; }
        public bool SAV { get; set; }
        public bool PCK { get; set; }
        public bool ECO { get; set; }

        // Constructor
        public EUT()
        {
            //anders komt datum van vandaag in datetimepicker te staan
            //AvailabilityDate = DateTime.Now;

            // Tests are not active on start
            EMC = false;
            ENV = false;
            REL = false;
            SAV = false;
            PCK = false;
            ECO = false;
            RemoveSingleEUTCommand = new RelayCommand<object>((obj) => DeleteItem(obj));
        }
        private void DeleteItem(object? obj)
        {
            if (obj != null)
            {
                //verwijder EUT ViewModelApproveJRForm
               // geen idee hoe dit gedaan moet worden er is all een multivalueconverter maar is nog niet toegevoegd

                MessageBox.Show(obj.ToString());
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
