namespace BetService.Core.Dto;

public class MakeBetReq
{
    public string UserId { get; set; }
    public List<int> Numbers { get; set; } = [];
    public decimal Amount { get; set; }
    public DateTime EventDateTime { get; set; }
    public string Type { get; set; }
}