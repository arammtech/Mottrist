using AutoMapper;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Country.Interfaces;
using Mottrist.Service.Features.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Country.Services
{
    public class Countryservice : BaseService, ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Countryservice(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool CountryExists(int Id)
        {
            //var country = await _unitOfWork.Repository<Country>().get; ;

            return true;
        }
    }
}
