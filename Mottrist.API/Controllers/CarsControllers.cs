using Microsoft.AspNetCore.Mvc;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Cars.Services;
using Mottrist.Service.Features.Countries.Interfaces;
using Mottrist.Service.Features.Countries.Services;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    [Route("api/Cars")]
    [ApiController]
    public class CarsControllers : ControllerBase
    {

        private readonly ICarService _carService;

        public CarsControllers(ICarService carService)
        {
            _carService = carService;
        }
    }
}
