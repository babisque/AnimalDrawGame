using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Duke of Ten" category in Animal Draw Game, where the bettor selects two different tens (00-99).
    /// If both selected numbers appear across any of the five prize categories, the bettor wins 300 times the bet amount.
    /// </summary>
    public class DukeOfTen : BetBase
    {
        /// <summary>
        /// The two selected tens (00-99) for the bet.
        /// </summary>
        [BsonElement("Numbers")]
        public int[] Ten { get; private set; } = null!;

        public override void CreateBet(object parameters)
        {
            var numbers = (int[])parameters;
            
            if (numbers.Length != 2)
                throw new ArgumentException("Exactly two tens must be selected.");
                
            if (numbers[0] is < 0 or > 99 || numbers[1] is < 0 or > 99)
                throw new ArgumentOutOfRangeException(nameof(Ten), "Each ten must be between 00 and 99.");
                
            if (numbers[0] == numbers[1])
                throw new ArgumentException("The two selected tens must be different.");
                
            Ten = numbers;
        }
        
        public override decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 300.0m;
        }
    }
}