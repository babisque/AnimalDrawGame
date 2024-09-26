using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities
{
    public class Bet : EntityBase
    {
        [BsonElement("UserId")]
        public string UserId { get; set; } = null!; // Null-forgiving operator para evitar warnings de nullability

        [BsonElement("Amount")]
        public decimal Amount { get; set; }

        [BsonElement("EventDateTime")]
        public DateTime EventDateTime { get; set; }

        [BsonElement("Confirmed")] 
        public bool Confirmed { get; set; } = false;

        [BsonElement("Type")]
        public IBetType Type { get; set; } = default!; // Null-forgiving operator para evitar warnings de nullability

        public Bet() { }

        public Bet(string userId, decimal amount, DateTime eventDateTime, IBetType type)
        {
            UserId = userId;
            Amount = amount;
            EventDateTime = eventDateTime;
            Type = type;
        }

        public decimal GetWinnings()
        {
            return Type.CalculateWinnings(Amount);
        }
    }
}