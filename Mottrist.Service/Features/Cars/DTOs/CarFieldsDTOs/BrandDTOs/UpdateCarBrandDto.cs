using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs.BrandDTOs
{
    public class UpdateCarBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
