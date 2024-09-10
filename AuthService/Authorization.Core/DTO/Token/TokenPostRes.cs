namespace Authorization.Core.DTO.Token;

public class TokenPostRes
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}