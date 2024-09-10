using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.DTO.User;
using UserService.Core.Entities;

namespace UserService.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserPostReq req)
    {
        var user = new ApplicationUser
        {
            UserName = req.UserName,
            Email = req.Email,
            FullName = $"{req.FirstName} {req.LastName}"
        };
        
        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors.ToList());
        
        return Created($"users/{user.Id}", user.Id);
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(UserLoginReq req)
    {
        var user = await _userManager.FindByNameAsync(req.UserName);
        if (user == null || !(await _userManager.CheckPasswordAsync(user, req.Password)))
            return BadRequest(new { message = "Username or password is incorrect" });

        var res = new UserLoginRes
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName
        };

        return Ok(res);
    }
}