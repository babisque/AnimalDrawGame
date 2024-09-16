using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities;

public class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}