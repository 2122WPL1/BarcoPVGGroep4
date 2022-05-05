using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using BarcoPVG.Models;
using BarcoPVG.Models.Db;

namespace BarcoDB_Admin.ViewModels
{
    public abstract class AbstractViewModelCollectionRQ : AbstractViewModelBase, IValueConverter
    {
        protected RqRequest _selectedJR;
        protected SolidColorBrush jobNatureColor;

        //Constructor
        public AbstractViewModelCollectionRQ() : base()
        {
            // Collection initialization
            IdRequestsOnly = new ObservableCollection<RqRequest>();

            // empty jr selected by default
            _selectedJR = new RqRequest();
        }

        // Getters/Setters
        public ObservableCollection<RqRequest> IdRequestsOnly 
        {
            get; 
            set;
        }

        public RqRequest SelectedJR
        {
            get => _selectedJR;
            set
            {
                _selectedJR = value;
                OnpropertyChanged();
            }
        }

        public SolidColorBrush JobNatureColor
        {
            get => jobNatureColor;
            set => jobNatureColor = value;
        }



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var jobNature = (string)value;

            jobNatureColor = jobNature switch
            {
                "Qualification (FQR)" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFCC99")),
                //case "Confidence (CDR)":
                //    jobNatureColor = Brushes.Green;
                //    break;
                //case "Confidence (IDR)":
                //    jobNatureColor = Brushes.Green;
                //    break;
                _ => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC0C0C0")),
            };
            return JobNatureColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
