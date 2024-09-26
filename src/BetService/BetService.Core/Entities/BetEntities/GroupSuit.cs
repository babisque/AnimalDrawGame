using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Group Suit" (Terno de Grupo) category in Animal Draw Game, where the bettor selects three different groups/animals.
    /// If all three selected groups appear in any of the five prize categories, the bettor wins 130 times the bet amount.
    /// </summary>
    public class GroupSuit : BetBase
    {
        /// <summary>
        /// The three selected groups/animals for the bet.
        /// </summary>
        [BsonElement("Groups")]
        public int[] Groups { get; private set; }

        public override void CreateBet(object parameters)
        {
            var betNumbers = parameters as int[];
            
            if (betNumbers.Length != 3)
                throw new ArgumentException("Exactly three groups/animals must be selected.");
                
            if (betNumbers[0] == betNumbers[1] || betNumbers[1] == betNumbers[2] || betNumbers[0] == betNumbers[2])
                throw new ArgumentException("All three groups/animals must be different.");
                
            Groups = betNumbers;
        }

        public override decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 130;
        }
    }
}