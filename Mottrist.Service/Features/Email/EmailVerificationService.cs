﻿using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Email.Interfaces;
using Mottrist.Service.Features.General.Token;
using System.Net;

namespace Mottrist.Service.Features.Email
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public EmailVerificationService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public string GenerateLinkToVerifyTokenAsync(string token, int userId)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            return $"https://localhost:5500/index.html?userId={userId}&token={_tokenService.ShortenToken(encodedToken)}";
        }

        public async Task<Result> VerifyEmailAsync(int userId, string token)
        {
            token = _tokenService.DecodeShortenToken(token);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return Result.Failure("User not found.");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded ?  Result.Success(): Result.Failure("Email Not Verified");
        }
    }
}
