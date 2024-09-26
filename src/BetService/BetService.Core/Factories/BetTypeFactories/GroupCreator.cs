using BetService.Core.Entities;
using BetService.Core.Entities.BetEntities;

namespace BetService.Core.Factories.BetTypeFactories;

public class GroupCreator : TypeCreator
{
    public override IBetType Create()
    {
        return new Group();
    }
}