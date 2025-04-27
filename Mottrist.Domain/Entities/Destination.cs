using Mottrist.Domain.Common;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Domain.Entities
{
    public class Destination : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int CityId { get; set; }
        public string? Type { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        #region Navigation Properties
        public virtual City City { get; set; } = null!;
        #endregion

    }
}
