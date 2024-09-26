using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities.BetEntities
{
    /// <summary>
    /// Represents a bet in the "Group Corner" (Quina de Grupo) category Animal Draw Game, where the bettor selects five animals/groups.
    /// If all five selected animals are drawn in the results, the bettor wins 5000 times the bet amount.
    /// </summary>
    public class GroupCorner : BetBase
    {
        

        /// <summary>
        /// The five selected animals/groups for the bet.
        /// </summary>
        [BsonElement("Numbers")]
        public string[] Animals { get; private set; }

        public override void CreateBet(object parameters)
        {
            string[] betNumbers = (string[])parameters;
            if (betNumbers.Length != 5)
                throw new ArgumentException("Exactly five animals must be selected.");
                
            if (betNumbers.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Animal/group names cannot be empty or whitespace.");
            }

            Animals = betNumbers;
        }

        public override decimal CalculateWinnings(decimal betAmount)
        {
            if (betAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be greater than zero.");
            
            return betAmount * 5000m;
        }
    }
}