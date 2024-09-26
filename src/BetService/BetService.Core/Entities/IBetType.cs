namespace BetService.Core.Entities;

public interface IBetType
{
    /// <summary>
    /// Calcula os ganhos da aposta. Se o animal escolhido for sorteado, 
    /// o valor do prêmio é 18 vezes o valor da aposta.
    /// </summary>
    /// <param name="betAmount">O valor apostado.</param>
    /// <returns>O valor dos ganhos, caso a aposta seja vencedora.</returns>
    decimal CalculateWinnings(decimal betAmount);
}