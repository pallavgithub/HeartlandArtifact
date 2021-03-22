using System;
using System.Globalization;
using Xamarin.Forms;

namespace HeartlandArtifact.Converters
{
    public class SelectedTabColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color LabelColor = Color.Black;
            if (value as string == parameter as string)
            {
                LabelColor = Color.FromHex("#823E21");
            }
            else
            {
                LabelColor = Color.Black;
            }
            return LabelColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
