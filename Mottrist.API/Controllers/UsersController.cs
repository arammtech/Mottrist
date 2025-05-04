using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Users.DTOs;
using Mottrist.Service.Features.Users.Interface;
using Mottrist.Service.Features.Users.Services;
using System.Text.RegularExpressions;
using Mottrist.Utilities.Global;
using static Mottrist.API.Response.ApiResponseHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mottrist.API.Controllers
{
    /// <summary>
    /// Manages user authentication and authorization.
    /// </summary>

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
   
        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IUserService userService)
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
        [HttpPost("auth/login")]
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

                // Validate if the user's account is locked
                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                    return StatusCodeResponse(StatusCodes.Status423Locked,"USER_ACCOUNT_lOCKED","User account is locked");

                //Prevent Brute Force Attacks
                var result = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, false, lockoutOnFailure: true);
                if (result.IsLockedOut)
                    return Unauthorized("Account locked due to multiple failed attempts.");

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
        [HttpPost("auth/logout")]
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

        /// <summary>
        /// Sends a confirmation email to the specified address.
        /// </summary>
        /// <remarks>
        /// This endpoint is public (no authentication required).  
        /// It validates the email format, then calls into the user service to generate and send
        /// an email confirmation link.
        /// </remarks>
        /// <param name="email">The email address to which the confirmation link will be sent.</param>
        /// <returns>
        /// • 200 OK with a success message if the email was sent successfully.  
        /// • 400 Bad Request if the email is missing or malformatted.  
        /// • 500 Internal Server Error on any service or unexpected exception.
        /// </returns>
        /// <response code="200">Confirmation email sent successfully.</response>
        /// <response code="400">Invalid or missing email address.</response>
        /// <response code="500">An error occurred while sending the email.</response>
        [AllowAnonymous]
        [HttpPost("send-confirm-email")]
        public async Task<IActionResult> SendConfirmEmailToUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequestResponse("INVALID_EMAIL", "Email is required.", "Email is required");

            if (!Regex.IsMatch(email, GlobalSettings.EmailPattern))
                return BadRequestResponse("INVALID_EMAIL_FORMAT", "Email format is incorrect.", "Provide a valid email.");

            try
            {
                var result = await _userService.SendEmailAsync(email);

                return result.IsSuccess ? SuccessResponse("Email confirmed") : StatusCodeResponse(StatusCodes.Status500InternalServerError, "ConfirmingError", "Failed to confirm user's email.");
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
        /// Confirms a user’s email address using the provided user ID and token.
        /// </summary>
        /// <remarks>
        /// This endpoint is public (no authentication required).  
        /// It takes the user’s ID and the confirmation token that was emailed to them, 
        /// validates both, and then marks the user’s email as confirmed if valid.
        /// </remarks>
        /// <param name="userId">The ID of the user to confirm (must be a positive integer).</param>
        /// <param name="token">The email confirmation token that was generated and sent earlier.</param>
        /// <returns>
        /// • 200 OK if the email was confirmed successfully.  
        /// • 400 Bad Request if the inputs are invalid or the confirmation fails.  
        /// • 500 Internal Server Error on any service or unexpected exception.
        /// </returns>
        /// <response code="200">Email confirmed successfully.</response>
        /// <response code="400">Invalid user ID, token, or confirmation failure.</response>
        /// <response code="500">An error occurred while confirming the email.</response>
        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(int userId, string token)
        {
            if(userId == null || userId <= 0)
                return BadRequestResponse("INVALID_USER_ID", "User id not valid.", "User Id is required and should be positive number");

            if(string.IsNullOrWhiteSpace(token))
                return BadRequestResponse("INVALID_TOKEN", "Token is required.", "Email token is required");

            try
            {
                var result = await _userService.ConfirmEmailAsync(userId.ToString(), token);

                return result.IsSuccess ?
                    SuccessResponse("Email confirmed successfully.")
                    : BadRequestResponse("Email_Confirmation_Failed", "Email confirmation failed", result.Errors.ToArray());
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