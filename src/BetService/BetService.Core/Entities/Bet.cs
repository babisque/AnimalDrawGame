﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetService.Core.Entities;

public class Bet : EntityBase
{
    [BsonElement("UserId")]
    public string UserId { get; set; }
    
    [BsonElement("Numbers")]
    public required List<int> Numbers { get; set; } = [];
    
    [BsonElement("Amount")]
    public decimal Amount { get; set; }
    
    [BsonElement("EventDateTime")]
    public required DateTime EventDateTime { get; set; }

    [BsonElement("Confirmed")] 
    public bool Confirmed { get; set; } = false;
    
    [BsonElement("Type")]
    public string Type { get; set; } = string.Empty;
}