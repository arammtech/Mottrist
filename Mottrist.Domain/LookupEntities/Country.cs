using Mottrist.Domain.Common;
using Mottrist.Domain.Enums;

namespace Mottrist.Domain.LookupEntities
{
    public class Country : LookupEntity
    {
        public string Name { get; set; } = null!;
        public Continent Continent { get; set; }
    }
}
