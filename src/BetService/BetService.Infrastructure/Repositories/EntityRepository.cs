using BetService.Core.Entities;
using BetService.Core.Repositories;
using BetService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BetService.Infrastructure.Repositories;

public class EntityRepository<T> : IRepository<T> where T : EntityBase
{
    private readonly IMongoCollection<T> _collection;
    
    public EntityRepository(IOptions<MongoDbSettings> entityOptions)
    {
        var mongoClient = new MongoClient(entityOptions.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(entityOptions.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
    }
    
    public async Task<IList<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(T entity) => 
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}