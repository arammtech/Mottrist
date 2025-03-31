
using Microsoft.AspNetCore.Http;

namespace Mottrist.Utilities.Global
{
    public static class GlobalFunctions
    {
        // Get Resend Code Time
        public static int GetResendCodeTime(int time)
        {
            switch (time)
            {
                case 1:
                    return EmailCodeTimes.resendCodeTimeMins1;
                case 2:
                    return EmailCodeTimes.resendCodeTimeMins2;
                case 3:
                    return EmailCodeTimes.resendCodeTimeMins3;
                default:
                    return 15;
            }
        }
        // Helper method to generate a default UserName based on available details
        public static string GenerateDefaultUserName(string FirstName, string LastName)
        {
            // Example: Combine FirstName and LastName with a random number to ensure uniqueness
            return $"{FirstName}.{LastName}{new Random().Next(1000, 9999)}";
        }


        /// <summary>
        /// Saves an uploaded image file to the specified folder and returns its relative URL.
        /// </summary>
        /// <param name="imageFile">The uploaded image file to save.</param>
        /// <param name="folderName">The folder where the image will be stored.</param>
        /// <returns>The relative URL of the saved image file.</returns>
        public static async Task<string> SaveImageAsync(IFormFile imageFile, string folderName)
        {
            // Ensure the image file is not null
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file.");
            }

            // Generate a unique file name to prevent overwriting
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

            // Build the full folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Build the full file path
            var filePath = Path.Combine(folderPath, uniqueFileName);

            // Save the file to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Return the relative URL (to be used for serving the image)
            return $"/images/{folderName}/{uniqueFileName}";
        }

        // Generic Filter
        public static List<T> _Filter<T>(List<T> collection, string filterProperty, string filterValue)
        {
            var propertyInfo = typeof(T).GetProperty(filterProperty);
            if (propertyInfo != null)
            {
                return collection.Where(item => propertyInfo.GetValue(item)?.ToString() == filterValue)
                    .ToList();
            }

            return collection;
        }






        //public static string SetOrderStatus(byte Status)
        //{
        //    switch (Status)
        //    {
        //        case 1:
        //            return GlobalSettings.StatusApproved;
        //        case 2:
        //            return GlobalSettings.StatusInProcess;
        //        case 3:
        //            return GlobalSettings.StatusShipped;
        //        case 4:
        //            return GlobalSettings.StatusCanceled;
        //        default:
        //            return "غير معروف";
        //    }

        //}

        //public static byte SetOrderStatus(string Status)
        //{
        //    switch (Status)
        //    {
        //        case GlobalSettings.StatusApproved:
        //            return 1;
        //        case GlobalSettings.StatusInProcess:
        //            return 2;
        //        case GlobalSettings.StatusShipped:
        //            return 3;
        //        case GlobalSettings.StatusCanceled:
        //            return 4;
        //        default:
        //            return 0;
        //    }

        //}
    }
}
