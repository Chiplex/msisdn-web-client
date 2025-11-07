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

    /// <summary>
    /// Convertidor genérico de bool a color (para pestañas, etc.)
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        public bool Inverted { get; set; } = false;
        public string ActiveColor { get; set; } = "#FFFFFF";
        public string InactiveColor { get; set; } = "#888888";

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                bool result = Inverted ? !isActive : isActive;
                return result ? Color.FromArgb(ActiveColor) : Color.FromArgb(InactiveColor);
            }
            return Color.FromArgb(InactiveColor);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convertidor de bool a FontAttributes
    /// </summary>
    public class BoolToFontAttributeConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isActive && isActive)
                return FontAttributes.Bold;
            
            return FontAttributes.None;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
