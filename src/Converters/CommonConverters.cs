using System;
using System.Globalization;

namespace MSISDNWebClient.Converters
{
    /// <summary>
    /// Convertidor que invierte un valor booleano
    /// </summary>
    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            
            return false;
        }
    }

    /// <summary>
    /// Convertidor que verifica si una colección no está vacía
    /// </summary>
    public class CollectionNotEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is System.Collections.ICollection collection)
                return collection.Count > 0;
            
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convertidor para estado de verificación
    /// </summary>
    public class BoolToVerifiedStatusConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isVerified)
                return isVerified ? "✓ Verificado" : "⚠ No Verificado";
            
            return "⚠ No Verificado";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convertidor para color del estado de verificación
    /// </summary>
    public class BoolToVerifiedColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isVerified)
                return isVerified ? Color.FromArgb("#10B981") : Color.FromArgb("#F59E0B");
            
            return Color.FromArgb("#F59E0B");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
