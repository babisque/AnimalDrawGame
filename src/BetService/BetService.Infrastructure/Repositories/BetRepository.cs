using BetService.Core.Entities;
using BetService.Core.Repositories;
using BetService.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace BetService.Infrastructure.Repositories;

public class BetRepository(IOptions<MongoDbSettings> mongoDbSettings) : EntityRepository<Bet>(mongoDbSettings), IBetRepository;