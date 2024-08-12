using System.Security.Claims;
using Authorization.Core.DTO.Users;
using Authorization.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserPostReq req)
    {
        var username = req.Email;
        username = username.Split('@')[0];
        var user = new ApplicationUser
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
        };
        var claimResult = await _userManager.AddClaimsAsync(user, claims);
        if (!claimResult.Succeeded)
            return BadRequest(claimResult.Errors.ToList());

        return Created($"/{user.Id}", user.Id);
    }
}