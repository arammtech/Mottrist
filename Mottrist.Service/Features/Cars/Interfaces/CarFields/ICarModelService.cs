using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.General.DTOs;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.Cars.Interfaces.CarFields
{
    public interface ICarModelService : IBaseService
    {
        Task<DataResult<CarModelDto>> GetAllAsync(Expression<Func<Model, bool>>? filter = null); 
    }
}
