using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Group Suit" (Terno de Grupo) category in Animal Draw Game, where the bettor selects three different groups/animals.
    /// If all three selected groups appear in any of the five prize categories, the bettor wins 130 times the bet amount.
    /// </summary>
    public class GroupSuit : IBetType
    {
        private int[] _groupNumbers = new int[3];

        /// <summary>
        /// The three selected groups/animals for the bet.
        /// </summary>
        [BsonElement("Groups")]
        public int[] Groups
        {
            get => _groupNumbers;
            set
            {
                if (value.Length != 3)
                    throw new ArgumentException("Exactly three groups/animals must be selected.");
                
                if (value[0] == value[1] || value[1] == value[2] || value[0] == value[2])
                    throw new ArgumentException("All three groups/animals must be different.");
                
                _groupNumbers = value;
            }
        }
        
        public decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 130;
        }
    }
}