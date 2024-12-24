using System;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skinet.Entities.Identity;

namespace Skinet.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static async Task<AppUser> GetUserByEmail(
        this UserManager<AppUser> userManager,
        ClaimsPrincipal user
    )
    {
        AppUser userToReturn =
            await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail())
            ?? throw new AuthenticationException("user not found");
        return userToReturn;
    }

    public static async Task<AppUser> GetUserByEmailWithAddress(
        this UserManager<AppUser> userManager,
        ClaimsPrincipal user
    )
    {
        AppUser userToReturn =
            await userManager
                .Users.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail())
            ?? throw new AuthenticationException("user not found");
        return userToReturn;
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        var email =
            user.FindFirstValue(ClaimTypes.Email)
            ?? throw new AuthenticationException("Email Claim Not found");
        return email;
    }
}
