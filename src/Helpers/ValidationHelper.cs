using System.Text.RegularExpressions;

namespace MSISDNWebClient.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidMSISDN(string msisdn)
        {
            // Validar que el MSISDN no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(msisdn))
            {
                return false;
            }

            // Expresión regular para validar el formato del MSISDN
            var regex = new Regex(@"^\+?[1-9]\d{1,14}$");
            return regex.IsMatch(msisdn);
        }

        public static bool IsValidEmail(string email)
        {
            // Validar que el correo electrónico no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Expresión regular para validar el formato del correo electrónico
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
    }
}