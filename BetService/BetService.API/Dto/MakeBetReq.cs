﻿namespace BetService.API.Dto;

public class MakeBetReq
{
    public string UserId { get; set; }
    public List<int> Numbers { get; set; } = [];
    public DateTime EventDateTime { get; set; }
}