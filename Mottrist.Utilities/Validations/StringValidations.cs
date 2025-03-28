using System.Text.RegularExpressions;

namespace Mottrist.Utilities.Validations
{
    public static class StringValidations
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Remove non-numeric characters before validation
            string digitsOnly = Regex.Replace(phoneNumber, @"\D", "");

            // Ensure length is between 10 (US standard) and 15 (international max)
            if (digitsOnly.Length < 10 || digitsOnly.Length > 15)
                return false;

            // Basic phone number pattern allowing country codes
            string pattern = @"^\+?(\d{1,3})?[\s.-]?(\(?\d{3}\)?)?[\s.-]?\d{3}[\s.-]?\d{4}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }

}
