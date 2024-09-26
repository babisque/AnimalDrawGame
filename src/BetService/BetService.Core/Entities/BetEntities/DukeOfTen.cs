using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Duke of Ten" category in Animal Draw Game, where the bettor selects two different tens (00-99).
    /// If both selected numbers appear across any of the five prize categories, the bettor wins 300 times the bet amount.
    /// </summary>
    public class DukeOfTen : IBetType
    {
        private int[] _tenNumbers = new int[2];

        /// <summary>
        /// The two selected tens (00-99) for the bet.
        /// </summary>
        [BsonElement("Numbers")]
        public int[] Ten
        {
            get => _tenNumbers;
            set
            {
                if (value.Length != 2)
                    throw new ArgumentException("Exactly two tens must be selected.");
                
                if (value[0] is < 0 or > 99 || value[1] is < 0 or > 99)
                    throw new ArgumentOutOfRangeException(nameof(Ten), "Each ten must be between 00 and 99.");
                
                if (value[0] == value[1])
                    throw new ArgumentException("The two selected tens must be different.");
                
                _tenNumbers = value;
            }
        }
        
        public decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 300.0m;
        }
    }
}