using System.Text.RegularExpressions;

namespace PE_PRN232_FA25_LeCongHung_BLL.Services
{
    public static class ValidationService
    {
        public static bool ValidateBearName(string bearName, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(bearName))
            {
                errorMessage = "BearName is required.";
                return false;
            }

            // Trim whitespace
            bearName = bearName.Trim();

            // Check length: 4-50 characters
            if (bearName.Length < 4 || bearName.Length > 50)
            {
                errorMessage = "BearName must be between 4 and 50 characters.";
                return false;
            }

            // Check for special characters: #, @, &, (, )
            if (bearName.Contains('#') || bearName.Contains('@') || 
                bearName.Contains('&') || bearName.Contains('(') || bearName.Contains(')'))
            {
                errorMessage = "BearName cannot contain special characters: #, @, &, (, )";
                return false;
            }

            // Check format: Only a-z, A-Z, and spaces
            // Each word must start with uppercase letter
            // Allow single spaces between words
            if (!Regex.IsMatch(bearName, @"^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$"))
            {
                errorMessage = "BearName must contain only letters (a-z, A-Z) and spaces. Each word must start with an uppercase letter.";
                return false;
            }

            return true;
        }

        public static bool ValidateBearWeight(decimal bearWeight, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (bearWeight <= 200)
            {
                errorMessage = "BearWeight must be greater than 200.";
                return false;
            }

            return true;
        }
    }
}

