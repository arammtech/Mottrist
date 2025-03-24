using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Domain.Identity
{
	public class ApplicationUser : IdentityUser<int>
	{
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
    }
}
