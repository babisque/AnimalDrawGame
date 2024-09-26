using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Group/Animal" category in Animal Draw Game, where the bettor selects an animal or group.
    /// If the selected animal/group is drawn, the bettor wins 18 times the bet amount.
    /// </summary>
    public class Group : BetBase
    {
        /// <summary>
        /// The name of the selected animal/group for the bet.
        /// </summary>
        [BsonElement("Numbers")]
        public string Animal { get; private set; }
        
        public override void CreateBet(object parameters)
        {
            var betNumbers = (string)parameters;
            
            if (string.IsNullOrWhiteSpace(betNumbers))
                throw new ArgumentException("Animal/group name cannot be empty or whitespace.", nameof(Animal));
                
            Animal = betNumbers;
        }

        public override decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 18.0m;
        }
    }
}