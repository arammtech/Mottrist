using Mottrist.Domain.Common;

namespace Mottrist.Domain.Entities.CarDetails
{
    public class BodyType : LookupEntity
    {
        public string Type { get; set; } = null!;
    }
}
