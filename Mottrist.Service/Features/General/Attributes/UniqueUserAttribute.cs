using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;

public class UniqueUserAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // If the value is null or empty, let other validations (like [Required]) handle it
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        // Retrieve the UserManager service from the dependency injection container
        var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
        if (userManager == null)
            throw new InvalidOperationException("UserManager is not available in the ValidationContext.");

        // Check if the user exists; here we assume that value is the username. Adjust as needed.
        // Note: Using .Result or .GetAwaiter().GetResult() can cause issues in certain scenarios;
        // consider using asynchronous validation patterns if needed.
        var userExists = userManager.FindByEmailAsync(value.ToString()).GetAwaiter().GetResult();

        if (userExists != null)
        {
            return new ValidationResult("A user with this email already exists.");
        }

        return ValidationResult.Success;
    }
}
