using Mottrist.Domain.Common;

namespace Mottrist.Domain.Entities.CarDetails
{
    public class Brand : LookupEntity
    {
        public string Name { get; set; } = null!;
    }
}
