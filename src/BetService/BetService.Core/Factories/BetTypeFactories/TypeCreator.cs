using BetService.Core.Entities;

namespace BetService.Core.Factories.BetTypeFactories;

public abstract class TypeCreator
{
    public abstract IBetType Create();
}