using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.General.Images.Interface
{
    /// <summary>
    /// Provides image management services, including saving, replacing, and deleting images.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Saves an image asynchronously and returns its relative path.
        /// </summary>
        /// <param name="image">
        /// The image file to be saved.
        /// </param>
        /// <param name="imageCategory">
        /// The category of the image, used for organizing files.
        /// </param>
        /// <returns>
        /// The relative path of the saved image, or null if the operation fails.
        /// </returns>
        Task<string?> SaveImageAsync(IFormFile image, ImageCategory imageCategory);

        /// <summary>
        /// Replaces an existing image with a new one asynchronously.
        /// </summary>
        /// <param name="newImage">
        /// The new image file to replace the existing image.
        /// </param>
        /// <param name="existingImageUrl">
        /// The relative path of the existing image to be replaced.
        /// If null, a new image is saved without replacing.
        /// </param>
        /// <param name="imageCategory">
        /// The category of the image, used for organizing files.
        /// </param>
        /// <returns>
        /// The relative path of the updated image, or null if the operation fails.
        /// </returns>
        Task<string?> ReplaceImageAsync(IFormFile newImage, string? existingImageUrl, ImageCategory imageCategory);

        /// <summary>
        /// Deletes an image synchronously.
        /// </summary>
        /// <param name="relativePath">
        /// The relative path of the image to be deleted.
        /// </param>
        void DeleteImage(string relativePath);

        /// <summary>
        /// Deletes an image asynchronously.
        /// </summary>
        /// <param name="relativePath">
        /// The relative path of the image to be deleted.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous delete operation.
        /// </returns>
        Task DeleteImageAsync(string? relativePath);

        /// <summary>
        /// Deletes multiple images asynchronously.
        /// </summary>
        /// <param name="relativePaths">
        /// A list of relative paths for the images to be deleted.
        /// If null, no images will be deleted.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous delete operation.
        /// </returns>
        Task DeleteImagesAsync(List<string>? relativePaths);
    }


    public enum ImageCategory
    {
        Profiles,
        Cars,
        Places,
        Documents
    }
}
