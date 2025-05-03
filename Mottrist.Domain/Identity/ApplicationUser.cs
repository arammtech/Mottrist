using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Entities;

namespace Mottrist.Domain.Identity
{
	public class ApplicationUser : IdentityUser<int>
	{
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;

        public virtual ICollection<DriverInteraction> DriverInteractions { get; set; } = new List<DriverInteraction>();
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
