namespace BetService.Core.Dto.Messaging;

public class BetValidationRes
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public decimal BetAmount { get; set; }
}