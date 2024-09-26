namespace BetService.Core.Entities;

public interface IBetType
{
    /// <summary>
    /// Populate the numbers of bet
    /// </summary>
    void CreateBet(object betNumbers);
    
    /// <summary>
    /// Calculate the winnings of the bet.
    /// </summary>
    /// <param name="betAmount">The amount wagered.</param>
    /// <returns>The amount of winnings if the bet is successful.</returns>
    decimal CalculateWinnings(decimal betAmount);
}