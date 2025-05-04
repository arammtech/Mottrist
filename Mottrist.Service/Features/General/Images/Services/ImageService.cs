using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Mottrist.Service.Features.General.Images.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Mottrist.Service.Features.General.Images.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
           _hostEnvironment = hostEnvironment;
        }


        public async Task<string?> SaveImageAsync(IFormFile image, ImageCategory imageCategory)
        { 
            // Validate the image file
            if (!IsValidImage(image))
                return null;

            // Get file extension and generate a unique filename
            var extension = Path.GetExtension(image.FileName).ToLower();
            var fileName = $"{Guid.NewGuid()}{extension}";

            // Construct the folder path
            var folderPath = Path.Combine(_hostEnvironment.WebRootPath ?? "wwwroot", "uploads", "images", imageCategory.ToString());
            try
            {
                // Create the folder if it doesn't exist
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // Generate full file path
                var filePath = Path.Combine(folderPath, fileName);

                // Save the image to the file system
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Return relative path for public access
                return $"/uploads/images/{imageCategory}/{fileName}";
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }


        public void DeleteImage(string relativePath)
        {
            try
            { 
                var filePath = Path.Combine(_hostEnvironment.WebRootPath ?? "wwwroot", relativePath.TrimStart('/'));
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch
            {
                throw;
            }
        }


        public async Task<string?> ReplaceImageAsync(IFormFile newImage, string? existingImageUrl, ImageCategory imageCategory)
        {
            // Delete the existing image if it exists
            if (!string.IsNullOrWhiteSpace(existingImageUrl))
            {
                DeleteImage(existingImageUrl);
            }

            // Save and return the new image URL
            return await SaveImageAsync(newImage, imageCategory);
        }



        #region Helper methods
        private bool HasAllowedExtension(string extension)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".bmp", ".heic", ".heif" };
            return allowedExtensions.Contains(extension);
        }

        private bool IsWithinAllowedSize(IFormFile image)
        {
            const long maxAllowedSizeInBytes = 5 * 1024 * 1024; // 5MB
            return image.Length <= maxAllowedSizeInBytes;
        }

        private bool IsValidImage(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLower();
            return HasAllowedExtension(extension) && IsWithinAllowedSize(image);
        }
        #endregion
    }
}
