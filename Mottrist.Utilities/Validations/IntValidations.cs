
namespace Mottrist.Utilities.Validations
{
    public static class IntValidations
    {
        // Checks if an integer is positive.
        public static bool IsPositive(this int number)
        {
            return number > 0;
        }

        // Checks if an integer falls within a specific range (inclusive).
        public static bool IsInRange(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }

    }

}
