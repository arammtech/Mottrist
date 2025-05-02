using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Users.DTOs;
using Mottrist.Service.Features.Users.Interface;
using Mottrist.Service.Features.Users.Services;
using static Mottrist.API.Response.ApiResponseHelper;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// Manages user authentication and authorization.
    /// </summary>

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
   
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IUserService userService)
        {
            _userManager = userManager ;
            _signInManager = signInManager;
            _userService = userService;
        }


        /// <summary>
        /// Logs in a user with email and password authentication.
        /// </summary>
        /// <param name="userLoginDto">The login request DTO.</param>
        /// <returns>JWT token if authentication is successful.</returns>
        /// <response code="200">User logged in successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">Unauthorized, invalid credentials.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                return BadRequestResponse("DATA_INVALID", "Validation error in UserLoginDto.", errors);
            }

            try
              {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user == null)
                    return Unauthorized("Invalid username or password.");

                // Validate password
                var passwordValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
                if (!passwordValid)
                    return Unauthorized("Invalid username or password.");

                //Prevent Brute Force Attacks
                var result = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, false, lockoutOnFailure: true);
                if (result.IsLockedOut)
                    return Unauthorized("Account locked due to multiple failed attempts.");

                // map it auto later
                UserDto userDto = new()
                {
                    Id = user.Id,
                    UserName = user.Email,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList()
                };

                TokenDto tokenDto = await _userService.LoginAsync(userDto);

                return SuccessResponse<TokenDto>(tokenDto, "User logged successfully.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }

        // <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>A success response if logout is successful.</returns>
        /// <response code="200">User logged out successfully.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync();
                return SuccessResponse("User logged out successfully.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="addUserDto">User registration DTO.</param>
        /// <returns>JWT token if registration is successful.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Validation error in user data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserDto addUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                return BadRequestResponse("DATA_INVALID", "Validation error in AddUserDto.", errors);
            }

           try
              {
                var addUserResult = await _userService.AddUserAsync(addUserDto);

                return addUserResult.IsSuccess
                    ? CreatedResponse(null,null,"User created successfully.")
                    : StatusCodeResponse(StatusCodes.Status500InternalServerError, "CreationError", "Failed to create user.");

            }
            catch (HttpRequestException ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Service error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "ERROR_ACCRUED", "An error accrued", $"Unexpected error: {ex.Message}");
            }
        }


    }
}