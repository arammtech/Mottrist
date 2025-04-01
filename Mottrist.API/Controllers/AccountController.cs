using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mottrist.API.DTOs2;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Users.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mottrist.API.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public readonly IConfiguration _Configuration;

        public AccountController( UserManager<ApplicationUser> userManager,IConfiguration configuration) 
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _Configuration = configuration;
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            // Check
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if(user == null)
                 return Unauthorized();

            // Check password
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!result)
                return Unauthorized();

            var Claims = new List<Claim>() { 
                new Claim(ClaimTypes.Name, user.UserName),
                      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var rules = await _userManager.GetRolesAsync(user);

            foreach (var item in rules)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
            }


            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Secret"]));

            SigningCredentials credentials = new(securityKey,SecurityAlgorithms.HmacSha256);

            // Create the token from the Username/Password
            JwtSecurityToken myToken = new(
                issuer: _Configuration["JWT:ValidIssuer"],
                audience: _Configuration["JWT:ValidAudience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(myToken),
                expiration = myToken.ValidTo
            });


        }
    }
}
