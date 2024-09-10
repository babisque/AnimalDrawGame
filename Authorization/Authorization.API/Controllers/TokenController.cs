using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Authorization.Core.DTO.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class TokenController(HttpClient httpClient) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult?> GetToken([FromBody]TokenPostReq req)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                req.UserName, req.Password
            }),
            Encoding.UTF8,
            "application/json");

        using var response = await httpClient.PostAsync($"http://userservice-user-service-api-1:8080/user/login", jsonContent);
        if (!response.IsSuccessStatusCode) return BadRequest();
        var userRes = await response.Content.ReadFromJsonAsync<TokenPostRes>();

        var user = new TokenPostRes
        {
            Id = userRes!.Id,
            UserName = userRes.UserName,
            Email = userRes.Email,
        };
        
        var key = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm,1234567890AOkopvdnsioHGYUASGVBI"u8.ToArray();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            ]),
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