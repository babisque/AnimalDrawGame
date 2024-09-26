using BetService.Core.Entities;

namespace BetService.Core.Services;

public interface IBetServices
{
    IBetType CreateBetType(string type);
}