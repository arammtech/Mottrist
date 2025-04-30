using Mottrist.Domain.Entities;
using Mottrist.Service.Interfaces;
using System.Linq.Expressions;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.Destinations.DTOs;
using Microsoft.EntityFrameworkCore;
using static Mottrist.Utilities.Global.GlobalFunctions;
using AutoMapper;
using Mottrist.Service.Features.Cities.Dtos;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.General.Images.Interface;

namespace Mottrist.Service.Features.DestinationServices
{
    /// <summary>
    /// Service for managing destination-related operations, including retrieval and pagination.
    /// </summary>
    public class DestinationService : BaseService, IDestinationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public DestinationService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<DataResult<DestinationDto>?> GetAllAsync(Expression<Func<Destination, bool>>? filter = null)
        {
            try
            {
                // Build query from repository  
                var destinationsQuery = _unitOfWork.Repository<Destination>().Table;

                // Apply filter if provided  
                if (filter != null)
                {
                    destinationsQuery = destinationsQuery.Where(filter);
                }

                destinationsQuery = destinationsQuery
                    .Include(x => x.City)
                    .ThenInclude(x => x.Country);

                var destinations = await _mapper.ProjectTo<DestinationDto>(destinationsQuery).ToListAsync();

                return new DataResult<DestinationDto>
                {
                    Data = destinations.Any() ? destinations : Enumerable.Empty<DestinationDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaginatedResult<DestinationDto>?> GetAllWithPaginationAsync(int page = 1, int pageSize = 10, Expression<Func<Destination, bool>>? filter = null)
        {
            try
            {
                // Build query from repository  
                var destinationsQuery = _unitOfWork.Repository<Destination>().Table;

                // Apply filter if provided  
                if (filter != null)
                {
                    destinationsQuery = destinationsQuery.Where(filter);
                }

                destinationsQuery = destinationsQuery
                    .Include(x => x.City)
                    .ThenInclude(x => x.Country);

                var destinations = await _mapper.ProjectTo<DestinationDto>(destinationsQuery).ToListAsync();

                return new PaginatedResult<DestinationDto>
                {
                    Data = destinations,
                    TotalRecordsCount = 10,
                    PageNumber = page,
                    PageSize = pageSize,
                    DataRecordsCount = destinations.Count
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DestinationDto?> GetByIdAsync(int destinationId)
        {
            try
            {
                var destination = await _unitOfWork.Repository<Destination>()
                    .Table
                    .Include(x => x.City)
                    .ThenInclude(x => x.Country)
                    .FirstOrDefaultAsync(x=> x.Id == destinationId);

                return _mapper.Map<DestinationDto>(destination);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DestinationDto?> GetByIdAsync111111(int destinationId)
        {
            try
            {
                var destinationDto = await _unitOfWork.Repository<Destination>()
                    .Table
                    .Include(x => x.City)
                    .ThenInclude(x => x.Country)
                    .Select(x => new DestinationDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        ImageUrl = x.ImageUrl,
                        Description = x.Description,
                        Country = new CountryDto
                        {
                            Id = x.City.Country.Id,
                            Name = x.City.Country.Name,
                        },
                        City = new CityDto
                        {
                            Id = x.City.Id,
                            Name = x.City.Name,
                        },
                    })
                    .FirstOrDefaultAsync(x => x.Id == destinationId);

                return destinationDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Result<DestinationDto>> AddAsync(AddDestinationDto destinationDto)
        {
            try
            {
                string? savedImageUrl = await _imageService.SaveImageAsync(destinationDto.Image, ImageCategory.Places);

                if (string.IsNullOrEmpty(savedImageUrl))
                    return Result<DestinationDto>.Failure("Failed to save image.");

                var destination = _mapper.Map<Destination>(destinationDto);
                destination.ImageUrl = savedImageUrl;

                await _unitOfWork.Repository<Destination>().AddAsync(destination);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                if (destination.Id <= 0 || !saveResult.IsSuccess)
                    return Result<DestinationDto>.Failure("Failed to save destination.");

                var desntiationDto = _mapper.Map<DestinationDto>(destination);
                return Result<DestinationDto>.Success(desntiationDto);
            }
            catch (Exception ex)
            {
                return Result<DestinationDto>.Failure($"Error creating destination: {ex.Message}");
            }
        }

        public async Task<Result<DestinationDto>> UpdateAsync(UpdateDestinationDto updateDestinationDto)
        {
            try
            {
                var destination = await _unitOfWork.Repository<Destination>().GetAsync(d => d.Id == updateDestinationDto.Id);

                if (destination == null)
                    return Result<DestinationDto>.Failure("Destination not found.");

                // Check if a new image is provided
                if (updateDestinationDto.Image != null)
                {
                    // Save the new image and update the URL
                    string? savedImageUrl = await  _imageService.ReplaceImageAsync(updateDestinationDto.Image, destination.ImageUrl,ImageCategory.Places);
                    destination.ImageUrl = savedImageUrl ?? throw new ArgumentNullException();
                }

                _mapper.Map(updateDestinationDto, destination);

                await _unitOfWork.Repository<Destination>().UpdateAsync(destination);
                var result = await _unitOfWork.SaveChangesAsync();

                return Result<DestinationDto>.Success(await GetByIdAsync(destination.Id));
            }
            catch (Exception ex)
            {
                return Result<DestinationDto>.Failure($"Error updating destination: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(int destinationId)
        {
            // Validate input parameter
            if (destinationId < 1)
                return Result.Failure("Invalid destination ID.");

            try
            {
                var destination = await _unitOfWork.Repository<Destination>().GetAsync(d => d.Id == destinationId);

                if (destination == null)
                    return Result.Failure("Destination not found.");

                // Delete the image
                _imageService.DeleteImage(destination.ImageUrl);

                //if (!imageDeleted)
                //    return Result.Failure("Failed to delete image.");

                await _unitOfWork.Repository<Destination>().DeleteAsync(destination);
                var result = await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"Unexpected error while deleting destination: {ex.Message}");
            }
        }
    }
}
