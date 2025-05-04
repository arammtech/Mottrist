namespace Mottrist.Service.Features.Drivers.Helpers
{
    public class DriverCalculator
    {
        /// <summary>
        /// Calculates a driver's rating on a scale from 1.0 (lowest) to 5.0 (highest) based on likes and dislikes.
        /// </summary>
        /// <param name="likes">The number of likes received by the driver.</param>
        /// <param name="dislikes">The number of dislikes received by the driver.</param>
        /// <returns>A double representing the calculated rating (1.0 to 5.0).</returns>
        /// <remarks>
        /// The rating is calculated as follows:
        /// - If there are no likes or dislikes, the rating is 1.
        /// - Otherwise, the rating is based on the ratio of likes to total votes (likes + dislikes),
        ///   scaled to a 1–5 range: rating = 1 + (likeRatio * 4), rounded to the nearest integer.
        /// </remarks>
        public static double CalculateRating(int likes, int dislikes)
        {
            if (likes < 0 || dislikes < 0)
                return 1.0; // Invalid input, return 1.0

            // Handle zero total votes
            if (likes == 0 && dislikes == 0)
                return 1.0;

            // Calculate like ratio (0 to 1)
            double totalVotes = likes + dislikes;
            double likeRatio = likes / totalVotes;

            // Scale to 1–5 range: 1 + (likeRatio * 4)
            double rating = 1 + (likeRatio * 4);

            // Format rating to one decimal place (e.g., 4.8)
            return Math.Round(rating, 1);
        }

    }
}
