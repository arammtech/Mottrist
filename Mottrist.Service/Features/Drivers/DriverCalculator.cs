using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Drivers
{
    public class DriverCalculator
    {
        /// <summary>
        /// Calculates a driver's rating on a scale from 1 (lowest) to 5 (highest) based on likes and dislikes.
        /// </summary>
        /// <param name="likes">The number of likes received by the driver.</param>
        /// <param name="dislikes">The number of dislikes received by the driver.</param>
        /// <returns>A byte representing the calculated rating (1 to 5).</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="likes"/> or <paramref name="dislikes"/> is negative.
        /// </exception>
        /// <remarks>
        /// The rating is calculated as follows:
        /// - If there are no likes or dislikes, the rating is 1.
        /// - Otherwise, the rating is based on the ratio of likes to total votes (likes + dislikes),
        ///   scaled to a 1–5 range: rating = 1 + (likeRatio * 4), rounded to the nearest integer.
        /// </remarks>
        public byte CalculateRating(int likes, int dislikes)
        {
            if (likes < 0 || dislikes < 0)
                return 0;

            // Handle zero total votes
            if (likes == 0 && dislikes == 0)
                return 1;

            // Calculate like ratio (0 to 1)
            double totalVotes = likes + dislikes;
            double likeRatio = likes / totalVotes;

            // Scale to 1–5 range: 1 + (likeRatio * 4)
            double rawRating = 1 + (likeRatio * 4);

            // Round to nearest integer and clamp to 1–5
            int roundedRating = (int)Math.Round(rawRating);
            return (byte)Math.Clamp(roundedRating, 1, 5);
        }
    }
}

