using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.General.Images.Interface
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile image, ImageCategory imageCategory);
        Task<string?> ReplaceImageAsync(IFormFile newImage,string? existingImageUrl,ImageCategory imageCategory);
        void DeleteImage(string relativePath);
    }

    public enum ImageCategory
    {
        Profiles,
        Cars,
        Places,
        Documents
    }
}
