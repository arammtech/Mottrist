using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Drivers.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Drivers.Helpers
{
    /// <summary>
    /// Provides helper methods for user-related operations, such as creating users and assigning roles.
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// Assigns a specified role to a given user.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance for user management.</param>
        /// <param name="user">The user to assign the role to.</param>
        /// <param name="role">The role to assign to the user.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// On success, returns <see cref="Result.Success()"/>.
        /// On failure, returns <see cref="Result.Failure(string)"/> containing error details.
        /// </returns>
        public static async Task<Result> AssignUserRoleAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
        {
            var roleResult = await userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                // Extract and combine error codes into a single error message
                var errors = roleResult.Errors?.Select(e => e.Code).ToArray() ?? new[] { "Unknown error." };
                return Result.Failure(string.Join(", ", errors));
            }

            return Result.Success();
        }

        /// <summary>
        /// Creates a new user based on the details provided in the driver DTO.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance for user management.</param>
        /// <param name="driverDto">The DTO containing the user's details, such as email, password, and name.</param>
        /// <param name="user">An instance of <see cref="ApplicationUser"/> to populate with the user details.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the user creation process.
        /// On success, returns <see cref="Result.Success()"/>.
        /// On failure, returns <see cref="Result.Failure(string)"/> containing error details.
        /// </returns>
        public static async Task<Result> AddUserAsync(UserManager<ApplicationUser> userManager, AddDriverDto driverDto, ApplicationUser user)
        {
            // Map the username from the driver's email
            user.UserName = driverDto.Email;
            user.Id = 0;
            // Attempt to create the user
            var addUserResult = await userManager.CreateAsync(user);
            if (!addUserResult.Succeeded)
            {
                // Extract and combine error codes into a single error message
                var errors = addUserResult.Errors?.Select(e => e.Code).ToArray() ?? new[] { "Unknown error." };
                return Result.Failure(string.Join(", ", errors));
            }

            return Result.Success();
        }

        /// <summary>
        /// Updates the associated user details based on the given driver DTO.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance for user management.</param>
        /// <param name="driverDto">The driver DTO containing updated user details.</param>
        /// <param name="userId">The ID of the associated user to update.</param>
        /// <returns>
        /// A <see cref="Result"/> object indicating the outcome of the operation.
        /// On success, returns <see cref="Result.Success()"/>.
        /// On failure, returns <see cref="Result.Failure(string)"/> containing error details.
        /// </returns>
        public static async Task<Result> UpdateUserDetailsAsync(UserManager<ApplicationUser> userManager, UpdateDriverDto driverDto, int userId)
        {
            // Fetch the user by ID
            var existingUser = await userManager.FindByIdAsync(userId.ToString());
            if (existingUser == null)
            {
                return Result.Failure("Associated user not found.");
            }

            // Map updated details from the driver DTO to the existing user
            existingUser.FirstName = driverDto.FirstName;
            existingUser.LastName = driverDto.LastName;
            existingUser.PhoneNumber = driverDto.PhoneNumber;

            // Attempt to update the user
            var userUpdateResult = await userManager.UpdateAsync(existingUser);
            if (!userUpdateResult.Succeeded)
            {
                var errors = userUpdateResult.Errors?.Select(e => e.Code).ToArray() ?? new[] { "Unknown error." };
                return Result.Failure(string.Join(", ", errors), true);
            }

            return Result.Success();
        }
    
    }
}