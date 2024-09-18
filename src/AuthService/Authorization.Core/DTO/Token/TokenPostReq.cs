namespace Authorization.Core.DTO.Token;

public class TokenPostReq
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}