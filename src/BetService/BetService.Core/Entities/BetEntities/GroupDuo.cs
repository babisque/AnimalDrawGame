using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Group Duo" category in Animal Draw Game, where the bettor selects two different groups/animals.
    /// If at least one number from each group appears in any of the five prize categories, the bettor wins 18 times the bet amount.
    /// </summary>
    public class GroupDuo : BetBase
    {
        private int[] _duoTens = new int[2];

        /// <summary>
        /// The two selected groups/animals for the bet.
        /// </summary>
        [BsonElement("Numbers")]
        public int[] DuoTens { get; private set; }

        public override void CreateBet(object parameters)
        {
            var betNumbers = parameters as int[];
            
            if (betNumbers.Length != 2)
                throw new ArgumentException("Exactly two groups/animals must be selected.");
                
            if (betNumbers[0] == betNumbers[1])
                throw new ArgumentException("The two groups/animals must be different.");
                
            DuoTens = betNumbers;
        }

        public override decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 18;
        }
    }
}