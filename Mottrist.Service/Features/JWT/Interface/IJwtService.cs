using Mottrist.Service.Features.Users.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mottrist.Service.Features.JWT.Interface
{
    /// <summary>
    /// Interface for JWT service, responsible for generating user claims and tokens.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates claims for a given user based on their identity and roles.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="userRoles">List of roles assigned to the user.</param>
        /// <returns>A list of claims representing user identity.</returns>
        List<Claim> GenerateUserClaims(string userName, int userId, List<string> userRoles);

        /// <summary>
        /// Generates a JWT token using provided claims.
        /// </summary>
        /// <param name="claims">User claims to be included in the token.</param>
        /// <returns>A JWT security token.</returns>
        JwtSecurityToken GenerateToken(List<Claim> claims);
    }
}