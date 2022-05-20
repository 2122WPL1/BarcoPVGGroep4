using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace BarcoPVG.ViewModels
{
    internal class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<object> listParam = new ObservableCollection<object>();
            foreach (var item in values)
            {
                listParam.Add(item);
            }

            return listParam;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
