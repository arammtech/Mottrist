using Mottrist.Domain.Common;

namespace Mottrist.Domain.Entities.CarDetails
{
    public class FuelType : LookupEntity
    {
        public string Type { get; set; } = null!;
    }
}
