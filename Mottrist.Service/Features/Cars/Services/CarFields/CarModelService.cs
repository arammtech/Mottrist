using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.Countries.DTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Services.CarFields
{
    public class CarModelService : BaseService, ICarModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITravelerService _travelerService;

        public CarModelService(IUnitOfWork unitOfWork, IMapper mapper, ITravelerService travelerService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _travelerService = travelerService;
        }

        public async Task<DataResult<CarModelDto>> GetAllAsync(Expression<Func<Model, bool>>? filter = null)
        {
            try
            {
                var modelsQuery = _unitOfWork.Repository<Model>().Query().AsQueryable();

                if (filter != null)
                    modelsQuery = modelsQuery.Where(filter);

                var models = await modelsQuery
                  .ProjectTo<CarModelDto>(_mapper.ConfigurationProvider)
                  .ToListAsync();

                return new DataResult<CarModelDto>
                {
                    Data = models.Any() ? models : Enumerable.Empty<CarModelDto>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
