﻿using Mottrist.Domain.Common;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Domain.Entities
{
    public class Traveler : BaseEntity
    {
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int UserId { get; set; }

        #region Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        #endregion
    }
}
