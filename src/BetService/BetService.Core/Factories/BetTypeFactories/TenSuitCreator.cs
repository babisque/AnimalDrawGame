using BetService.Core.Entities;
using BetService.Core.Entities.BetEntities;

namespace BetService.Core.Factories.BetTypeFactories;

public class TenSuitCreator : TypeCreator
{
    public override IBetType Create()
    {
        return new TenSuit();
    }
}