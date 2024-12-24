using System;
using Microsoft.AspNetCore.Identity;

namespace Skinet.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
