using Mottrist.Domain.Common;

namespace Mottrist.Domain.LookupEntities
{
    public class Language : LookupEntity
    {
        public string Name { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<DriverLanguage> DriverLanguages { get; set; } = new List<DriverLanguage>();
        #endregion
    }
}
