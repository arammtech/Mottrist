using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Service.Features.User.DTOs;
using Mottrist.Utilities.Identity;
using System.Linq.Expressions;

namespace Mottrist.Service.Features.User.Extenstions
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Retrieves a paginated list of users based on the specified criteria.
        /// </summary>
        /// <param name="userManager">The UserManager instance.</param>
        /// <param name="roleManager">The RoleManager instance.</param>
        /// <param name="context">The database context for accessing user roles.</param>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of users per page. Defaults to 10.</param>
        /// <param name="role">Optional: The role to filter users by.</param>
        /// <param name="filter">Optional: A filter expression to apply to the user query.</param>
        /// <param name="isLocked">Optional: Whether to include only locked or unlocked users.</param>
        /// <returns>
        /// A tuple containing:
        /// - Users: The paginated list of users.
        /// - TotalRecords: The total count of users matching the criteria.
        /// Returns null for both if there is an error or invalid input.
        /// </returns>
        public static async Task<(IEnumerable<UserDto>? Users, int? TotalRecords)> GetAllAsync(
            this UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AppDbContext context,
            int page,
            int pageSize = 10,
            string? role = null,
            Expression<Func<ApplicationUser, bool>>? filter = null,
            bool? isLocked = null)
        {
            // Validate pagination parameters
            if (page < 1 || pageSize < 1)
            {
                return (null, null);
            }

            try
            {
                // Start building the user query
                var usersQuery = userManager.Users.AsQueryable();

                // Apply filter expression if provided
                if (filter != null)
                {
                    usersQuery = usersQuery.Where(filter);
                }

                // Filter by role if specified
                if (!string.IsNullOrEmpty(role))
                {
                    var roleId = await roleManager.Roles
                        .Where(r => r.Name == role)
                        .Select(r => r.Id)
                        .FirstOrDefaultAsync();

                    if (roleId == null)
                    {
                        return (null, null);
                    }

                    usersQuery = from user in usersQuery
                                 join userRole in context.UserRoles on user.Id equals userRole.UserId
                                 where userRole.RoleId == roleId
                                 select user;
                }

                // Filter by lockout status if specified
                if (isLocked.HasValue)
                {
                    usersQuery = isLocked.Value
                        ? usersQuery.Where(u => u.LockoutEnd.HasValue && u.LockoutEnd.Value > DateTimeOffset.UtcNow)
                        : usersQuery.Where(u => !u.LockoutEnd.HasValue || u.LockoutEnd.Value <= DateTimeOffset.UtcNow);
                }

                // Get total record count
                var totalRecords = await usersQuery.CountAsync();

                // Paginate and map users to DTO
                var paginatedUsers = await usersQuery
                    .OrderBy(u => u.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(user => new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    })
                    .ToListAsync();

                return (paginatedUsers, totalRecords);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return (null, null);
            }
        }
        public static async Task<Result> LockUserAsync(this UserManager<ApplicationUser> userManager, int userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return Result.Failure("User not found");
                }

                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains(AppUserRoles.RoleAdmin))
                {
                    return Result.Failure("ا تستطيع ان تنفذ هذه العملية على الادمن");
                }

                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // Lockout indefinitely
                var result = await userManager.UpdateAsync(user);

                return result.Succeeded ? Result.Success() : Result.Failure("فشلت العملية");
            }
            catch (Exception)
            {
                return Result.Failure("فشلت العملية");
            }
        }

        public static async Task<Result> UnlockUserAsync(this UserManager<ApplicationUser> userManager, int userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return Result.Failure("User not found");
                }

                user.LockoutEnd = null; // Remove lockout
                var result = await userManager.UpdateAsync(user);

                return result.Succeeded
                    ? Result.Success()
                    : Result.Failure(result.Errors.Select(e => e.Description).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to unlock user: {ex.Message}");
            }
        }
    }

}
