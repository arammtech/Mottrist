using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mottrist.Service.Features.JWT.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mottrist.Service.Features.JWT.Services
{
    /// <summary>
    /// Service class for handling JWT token generation and claims management.
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="configuration">Configuration for JWT settings.</param>
        public JwtService( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates claims for a given user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="userRoles">List of roles assigned to the user.</param>
        /// <returns>A list of claims representing user information.</returns>
        public List<Claim> GenerateUserClaims(string userName, int userId, List<string> userRoles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add user roles as claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        /// <summary>
        /// Generates a JWT token using provided claims.
        /// </summary>
        /// <param name="claims">User claims to be included in the token.</param>
        /// <returns>A JWT security token.</returns>
        public JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            // Create JWT token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return token;
        }
    }
}
