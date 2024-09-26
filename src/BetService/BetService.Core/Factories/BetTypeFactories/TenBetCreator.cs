using BetService.Core.Entities;
using BetService.Core.Entities.BetEntities;

namespace BetService.Core.Factories.BetTypeFactories;

public class TenBetCreator : TypeCreator
{
    public override IBetType Create()
    {
        return new TenBet();
    }
}