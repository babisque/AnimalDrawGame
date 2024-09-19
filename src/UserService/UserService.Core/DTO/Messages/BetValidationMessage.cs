namespace UserService.Core.DTO.Messages;

public class BetValidationMessage
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public decimal BetAmount { get; set; }
}