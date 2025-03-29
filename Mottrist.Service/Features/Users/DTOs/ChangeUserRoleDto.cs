using Mottrist.Domain.Identity;
using System.ComponentModel.DataAnnotations;
namespace Mottrist.Service.Features.Users.DTOs
{
    public class ChangeUserRoleDto
    {
        public int Id { get; set; }
        public string oldRole { get; set; }
        public string? newRole { get; set; }
        public List<ApplicationRole> Roles { get; set; } = [];
    }
}
