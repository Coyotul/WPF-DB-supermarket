using System;
using System.Globalization;
using System.Windows.Data;

namespace SupermarketApp.Converters
{
    public class CategorieProdusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convertim obiectul CategorieProdus într-un format potrivit pentru afișarea în UI
            if (value is CategorieProdus)
            {
                CategorieProdus categorie = (CategorieProdus)value;
                return categorie.NumeCategorie;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
