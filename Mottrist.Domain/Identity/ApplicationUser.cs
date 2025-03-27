using Microsoft.AspNetCore.Identity;

namespace Mottrist.Domain.Identity
{
	public class ApplicationUser : IdentityUser<int>
	{
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
