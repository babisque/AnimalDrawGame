namespace BetService.Core.Dto.Messaging;

public class BetValidationRes
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public decimal BetAmount { get; set; }
}