using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Milhar" category of Animal Draw Game, where the bettor chooses a four-digit sequence.
    /// If the chosen sequence appears in any of the prize categories, the bettor wins 4000 times the bet amount.
    /// </summary>
    public class ThousandBet : IBetType
    {
        private int _numbersSequence;

        /// <summary>
        /// The four-digit sequence chosen for the bet, ranging from 0000 to 9999.
        /// </summary>
        [BsonElement("NumbersSequence")]
        public int NumbersSequence
        {
            get => _numbersSequence;
            set
            {
                if (value is < 0 or > 9999)
                    throw new ArgumentOutOfRangeException(nameof(NumbersSequence), "The number sequence must be between 0000 and 9999.");
                
                _numbersSequence = value;
            }
        }

        public decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 4000;
        }
    }
}