namespace BetService.Core.Dto;

public class GetBetRes
{
    public string? Id { get; set; }
    public List<int> Numbers { get; set; }
    public string UserId { get; set; }
    public DateTime EventDateTime { get; set; }
}