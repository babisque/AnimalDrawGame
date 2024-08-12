using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Core.DTO.Token;
using Authorization.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class TokenController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager;

    public TokenController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult> GetToken(TokenPostReq req)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user == null || !(await _userManager.CheckPasswordAsync(user, req.Password)))
            return BadRequest("User or password is incorrect.");

        var claims = await _userManager.GetClaimsAsync(user);
        var key = Encoding.ASCII.GetBytes(
            "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm,1234567890AOkopvdnsioHGYUASGVBI");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
            }),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "AnimalDrawGame",
            Issuer = "Issuer"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}