using Microsoft.AspNetCore.Identity;

namespace UserService.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public decimal Balance { get; set; } = 100.0m;
}