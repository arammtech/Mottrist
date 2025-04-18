﻿using Mottrist.Service.Features.Users.DTOs;

namespace Mottrist.Service.Features.Users
{
    public class UserGenerate
    {
        public static string GenerateDefaultUserNameFromEmailOrNames(UserDto userDto)
        {
            if (!string.IsNullOrWhiteSpace(userDto.Email))
            {
                // Use the part of the email before the "@" symbol
                return userDto.Email.Split('@')[0];
            }

            // Generate a username using first and last name if email is null or invalid
            return GenerateDefaultUserName(userDto.FirstName, userDto.LastName);
        }

        public static string GenerateDefaultUserName(string firstName, string lastName)
        {
            // Combine first name, last name, and a random number for uniqueness
            return $"{firstName}.{lastName}{new Random().Next(1000, 9999)}";
        }

    }
}
