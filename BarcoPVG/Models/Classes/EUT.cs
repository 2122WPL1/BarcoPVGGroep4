using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BarcoPVG.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;

namespace BarcoPVG.Models.Classes
{
    // Bridge between UI and Datamodels
    // Use class so only one getter/setter/onPropertyChanged needs to be created
    public class EUT : AbstractViewModelContainer
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
            // Tests are not active on start
            EMC = false;
            ENV = false;
            REL = false;
            SAV = false;
            PCK = false;
            ECO = false;
            RemoveSingleEUTCommand = new RelayCommand<object>(id => DeleteItem(id));
        }

        private void DeleteItem(object? id)
        {
            if (id == null)
                return;
            EUT toRemove = EUTs.FirstOrDefault(x => x.IdRqDetail == (int)id);
            //TODO hier nog ervoor zorgen dat de parameter word meegestuurd vanuit xaml
            if (toRemove != null)
            {
                //remove EUT ViewModelApproveJRForm
                EUTs.Remove(toRemove);
                //MessageBox.Show(obj.ToString());
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
