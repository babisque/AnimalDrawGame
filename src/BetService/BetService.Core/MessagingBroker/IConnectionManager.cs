﻿namespace BetService.Core.MessagingBroker;

public interface IConnectionManager<out T>
{
    T CreateChannel(string channelName);
}