using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Email.Interfaces;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.JWT.Interface;
using Mottrist.Service.Features.Users.DTOs;
using Mottrist.Service.Features.Users.Interface;
using Mottrist.Utilities.Identity;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Mottrist.Service.Features.Users.Services
{
    /// <summary>
    /// Service class for managing user authentication, registration, and related operations.
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management.</param>
        /// <param name="jwtService">Service for generating JWT tokens.</param>
        /// <param name="userManager">User manager for handling user operations.</param>
        /// <param name="signInManager">Sign-in manager for authentication operations.</param>
        /// <param name="mapper">AutoMapper instance for object mapping.</param>
        public UserService(IUnitOfWork unitOfWork , IJwtService jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper, IEmailSender emailService, IConfiguration configuration) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailSender = emailService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="userDto">User DTO containing authentication details.</param>
        /// <returns>JWT token with expiration date.</returns>
        public async Task<TokenDto> LoginAsync(UserDto userDto)
        {



            try
            {
                JwtSecurityToken token = _jwtService.GenerateToken(_jwtService.GenerateUserClaims(userDto.UserName, userDto.Id, userDto.Roles));

                TokenDto tokenDto = new()
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiredDateTime = token.ValidTo
                };

                return tokenDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Adds a new user and assigns roles to them.
        /// </summary>
        /// <param name="addUserDto">DTO containing user details.</param>
        /// <returns>Result containing user DTO on success.</returns>
        public async Task<Result> AddUserAsync(AddUserDto addUserDto)
        {

            try
            {
                ApplicationUser user = _mapper.Map<ApplicationUser>(addUserDto);
                user.UserName = user.Email;

                var addUserResult = await _userManager.CreateAsync(user, addUserDto.Password);

                if (!addUserResult.Succeeded)
                    return Result.Failure("Failed to save the user to the database.");

                // Assign roles to user
                foreach (var role in addUserDto.Roles)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if (!roleResult.Succeeded)
                        return Result.Failure("Failed to add the roles to the user");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error creating a user: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves user details by email.
        /// </summary>
        /// <param name="Email">User's email address.</param>
        /// <returns>User DTO containing user details.</returns>
        public async Task<UserDto> GetUserByEmail(string Email)
        {
           try
            {
                var user = await _userManager.FindByEmailAsync(Email);

                UserDto userDto = new();
                _mapper.Map(user, userDto);

                return userDto;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<Result> SendEmailAsync(string Email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(Email);

            if (user == null) 
                return Result.Failure("Failed to confirm user email.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmUrl = $"{_configuration["JWT:ValidIssuer"]}/api/user/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            var emailHtml = EmailTemplates.GetEmailConfirmEmailBody(confirmUrl);
            
            //await _emailSender.SendEmailAsync(user.Email, "Confirm Your Email", $"Click here: {confirmUrl}");
            await _emailSender.SendEmailAsync("merykassis48@gmail.com", "Confirm Your Email", emailHtml);

            return Result.Success();

        }

        public async Task<Result> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Failure("Invalid user ID.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return Result.Success();

            return Result.Failure("Email confirmation failed.");

        }

    }
}

