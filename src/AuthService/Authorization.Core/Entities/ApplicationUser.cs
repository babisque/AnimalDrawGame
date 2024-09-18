using Microsoft.AspNetCore.Identity;

namespace Authorization.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public decimal Balance { get; set; } = 0.0m;
}