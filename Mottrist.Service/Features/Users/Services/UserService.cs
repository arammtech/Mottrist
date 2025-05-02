using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.General;
using Mottrist.Service.Features.JWT.Interface;
using Mottrist.Service.Features.Users.DTOs;
using Mottrist.Service.Features.Users.Interface;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management.</param>
        /// <param name="jwtService">Service for generating JWT tokens.</param>
        /// <param name="userManager">User manager for handling user operations.</param>
        /// <param name="signInManager">Sign-in manager for authentication operations.</param>
        /// <param name="mapper">AutoMapper instance for object mapping.</param>
        public UserService(IUnitOfWork unitOfWork , IJwtService jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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
        /// Registers a new user and returns authentication token.
        /// </summary>
        /// <param name="addUserDto">User details for registration.</param>
        /// <returns>Result containing authentication token upon successful registration.</returns>
        public async Task<Result<TokenDto>> RegisterAsync(AddUserDto addUserDto)
        {
            try
            {
                var addUserResult = await AddUserAsync(addUserDto);

                return Result<TokenDto>.Failure("Failed to register the user");
            }
            catch (Exception ex)
            {
                return Result<TokenDto>.Failure("Failed to register the user");
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
    }
}

