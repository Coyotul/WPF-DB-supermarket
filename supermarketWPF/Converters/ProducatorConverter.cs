using System;
using System.Globalization;
using System.Windows.Data;

namespace SupermarketApp.Converters
{
    public class ProducatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convertim obiectul Producator într-un format potrivit pentru afișarea în UI
            if (value is Producator)
            {
                Producator producator = (Producator)value;
                return $"{producator.NumeProducator} - {producator.TaraOrigine}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
