namespace BetService.Core.Entities;

public abstract class BetBase : IBetType
{
    public abstract void CreateBet(object parameters);

    public abstract decimal CalculateWinnings(decimal betAmount);
}