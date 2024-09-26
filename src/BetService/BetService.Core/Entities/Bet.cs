using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities;

public class Bet : EntityBase
{
    [BsonElement("UserId")]
    public string UserId { get; set; }
    
    [BsonElement("Amount")]
    public decimal Amount { get; set; }
    
    [BsonElement("EventDateTime")]
    public required DateTime EventDateTime { get; set; }

    [BsonElement("Confirmed")] 
    public bool Confirmed { get; set; } = false;
    
    [BsonElement("Type")]
    public IBetType Type { get; set; }

    public decimal GetWinnings()
    {
        return Type.CalculateWinnings(Amount);
    }
}