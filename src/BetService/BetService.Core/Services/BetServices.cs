using BetService.Core.Entities;
using BetService.Core.Enums;
using BetService.Core.Factories.BetTypeFactories;

namespace BetService.Core.Services;

public class BetServices : IBetServices
{
    private readonly Dictionary<BetTypes, TypeCreator> _creators;

    public BetServices(Dictionary<BetTypes, TypeCreator> creators)
    {
        _creators = creators ?? throw new ArgumentNullException(nameof(creators));
    }
    
    public IBetType CreateBetType(string type)
    {
        if (Enum.TryParse<BetTypes>(type, out var betType) && _creators.TryGetValue(betType, out var creator))
        {
            return creator.Create();
        }

        throw new ArgumentException($"Invalid bet type: {type}");
    }

    public static Dictionary<BetTypes, TypeCreator> GetDefaultCreators()
    {
        return new Dictionary<BetTypes, TypeCreator>
        {
            { BetTypes.DukeOfTen, new DukeOfTenCreator() },
            { BetTypes.Group, new GroupCreator() },
            { BetTypes.GroupCorner, new GroupCornerCreator() },
            { BetTypes.GroupDuo, new GroupDuoCreator() },
            { BetTypes.HundredBet, new HundredBetCreator() },
            { BetTypes.TenBet, new TenBetCreator() },
            { BetTypes.TenSuit, new TenSuitCreator() },
            { BetTypes.ThousandBet, new ThousandBetCreator() }
        };
    }
}