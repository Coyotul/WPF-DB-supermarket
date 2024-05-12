using System;
using System.Globalization;
using System.Windows.Data;

namespace SupermarketApp.Converters
{
    public class UtilizatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convertim obiectul Utilizator într-un format potrivit pentru afișarea în UI
            if (value is Utilizator)
            {
                Utilizator utilizator = (Utilizator)value;
                return $"{utilizator.NumeUtilizator} - {utilizator.TipUtilizator}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
