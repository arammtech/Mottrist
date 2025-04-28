
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

        /// <summary>
        /// Replaces or deletes an image depending on the input parameters.
        /// </summary>
        /// <param name="newImage">The new image file, if provided.</param>
        /// <param name="folderPath">The folder path where the image should be stored.</param>
        /// <param name="existingImageUrl">The existing image URL, if applicable.</param>
        /// <returns>A structured result containing success state and the updated image URL.</returns>
        public static async Task<(bool IsSuccess, string? NewImageUrl)> UpdateImageAsync(IFormFile? newImage, string folderPath, string? existingImageUrl)
        {
            try
            {
                if (newImage != null)
                {
                    return (true, await ReplaceImageAsync(newImage, folderPath, existingImageUrl));
                }

                if (!string.IsNullOrWhiteSpace(existingImageUrl))
                {
                    await DeleteImageAsync(existingImageUrl);
                    return (true, null);
                }

                return (true, existingImageUrl);
            }
            catch (Exception ex)
            {
                return (false, existingImageUrl);
            }
        }

        /// <summary>
        /// Deletes an image file from the specified folder.
        /// </summary>
        /// <param name="imageUrl">The relative URL of the image to delete.</param>
        /// <returns>True if the image was deleted successfully; otherwise, false.</returns>
        public static Task<bool> DeleteImageAsync(string imageUrl)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(imageUrl))
                        return false;

                    // Convert the relative URL to an absolute file path
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));

                    // Check if the file exists before deleting
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }


        /// <summary>
        /// Replaces an existing image file with a new one in the specified folder.
        /// </summary>
        /// <param name="newImageFile">The new image file to save.</param>
        /// <param name="folderName">The folder where the image will be stored.</param>
        /// <param name="existingImageUrl">The relative URL of the existing image to be replaced.</param>
        /// <returns>The relative URL of the newly saved image file.</returns>
        public static async Task<string> ReplaceImageAsync(IFormFile newImageFile, string folderName, string? existingImageUrl)
        {
            // Delete the existing image if it exists
            if (!string.IsNullOrWhiteSpace(existingImageUrl))
            {
                await DeleteImageAsync(existingImageUrl);
            }

            // Save and return the new image URL
            return await SaveImageAsync(newImageFile, folderName);
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

    }
}
