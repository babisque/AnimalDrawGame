using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Hundred" category of the Animal Draw Game, where the bettor chooses a three-digit number (000-999).
    /// If the chosen number appears in the last three digits of the 1st prize, the bettor wins 600 times the bet amount.
    /// </summary>
    public class HundredBet : BetBase
    {
        /// <summary>
        /// The chosen three-digit number for the bet, ranging from 000 to 999.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the number is outside the range of 000 to 999.</exception>
        [BsonElement("HundredNumber")]
        public int HundredNumber { get; private set; }

        public override void CreateBet(object parameters)
        {
            var betNumbers = (int)parameters;
            
            if (betNumbers is < 0 or > 999)
                throw new ArgumentOutOfRangeException(nameof(HundredNumber), "The hundred number must be between 000 and 999.");
                
            HundredNumber = betNumbers;
        }

        public override decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 600;
        }
    }
}