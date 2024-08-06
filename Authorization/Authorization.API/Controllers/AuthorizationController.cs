using System.Security.Claims;
using Authorization.Core.DTO.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthorizationController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ActionResult> CreateUser(UserPostReq req)
    {
        var username = req.Email;
        username = username.Split('@')[0];
        var user = new IdentityUser
        {
            Email = req.Email,
            UserName = username
        };

        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors.ToList());

        var claims = new List<Claim>
        {
            new Claim("Fullname", $"{req.FirstName} {req.LastName}"),
            new Claim("Balance", "0", "decimal")
        };
        var claimResult = await _userManager.AddClaimsAsync(user, claims);
        if (!claimResult.Succeeded)
            return BadRequest(claimResult.Errors.ToList());

        return Created($"/{user.Id}", user.Id);
    }
}