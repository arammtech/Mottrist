﻿using Mottrist.Domain.Common.Interfaces;

namespace Mottrist.Domain.Common
{
    public abstract class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}
