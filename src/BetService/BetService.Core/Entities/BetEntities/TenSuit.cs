using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Terno de Dezena" category in Animal Draw Game, where the bettor chooses three numbers.
    /// If the selected numbers appear in any of the first five prizes, the bettor wins 3000 times the bet amount.
    /// </summary>
    public class TenSuit : IBetType
    {
        private int[] _tenNumbers = new int[3];

        /// <summary>
        /// The three selected numbers (dozens) for the bet.
        /// </summary>
        [BsonElement("Ten")]
        public int[] TenNumbers
        {
            get => _tenNumbers;
            set
            {
                if (value.Length != 3)
                    throw new ArgumentException("Exactly three numbers must be selected.");
                
                if (value.Any(number => number is < 0 or > 99))
                    throw new ArgumentOutOfRangeException(nameof(TenNumbers), "Numbers must be between 00 and 99.");

                _tenNumbers = value;
            }
        }
        
        public decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 3000;
        }
    }
}