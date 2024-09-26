namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Dezena" modality of the Animal Draw Game, where the bettor chooses a number between 00 and 99.
    /// If the chosen number is drawn in the 1st category, the bettor receives 60 times the bet amount.
    /// </summary>
    public class TenBet : BetBase
    {
        /// <summary>
        /// The chosen number for the bet, which must be between 00 and 99.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the chosen number is outside the range of 00 to 99.</exception>
        public int TenNumbers { get; private set; }

        public override void CreateBet(object parameters)
        {
            var betNumbers = Convert.ToInt32(parameters);
            
            if (betNumbers is < 0 or > 99)
                throw new ArgumentOutOfRangeException(nameof(TenNumbers), "The value must be between 0 and 99.");
            
            TenNumbers = betNumbers;
        }

        /// <summary>
        /// Calculates the winnings based on the bet amount.
        /// </summary>
        /// <param name="betAmount">The amount bet by the user.</param>
        /// <returns>The total winnings, which is 60 times the bet amount.</returns>
        public override decimal CalculateWinnings(decimal betAmount)
        {
            return betAmount * 60;
        }
    }
}