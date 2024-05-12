using System;
using System.Globalization;
using System.Windows.Data;

namespace SupermarketApp.Converters
{
    public class ProdusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convertim obiectul Produs într-un format potrivit pentru afișarea în UI
            if (value is Produs)
            {
                Produs produs = (Produs)value;
                return $"{produs.NumeProdus} - {produs.Categorie} - {produs.Producator}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
