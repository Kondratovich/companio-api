using Companio.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Companio.Mongo;

public class MongoContext
{
    private readonly IMongoDatabase _provider;

    public MongoContext(IOptions<MongoSettings> mongoSettings)
    {
        MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
        _provider = client.GetDatabase(mongoSettings.Value.DatabaseName);
    }

    public IFindFluent<T, T> GetAll<T>()
    {
        return _provider.GetCollection<T>(typeof(T).Name).Find(s => true);
    }

    public IFindFluent<T, T> Find<T>(FilterDefinition<T> filter)
    {
        return _provider.GetCollection<T>(typeof(T).Name).Find(filter);
    }

    /// <summary>
    /// Returns null if specified object does not exist
    /// </summary>
    public T SingleByIdOrDefault<T>(ObjectId id)
    {
        return _provider.GetCollection<T>(typeof(T).Name).Find(Builders<T>.Filter.Eq("_id", id))
            .SingleOrDefault();
    }

    /// <summary>
    /// Inserts the object into the database
    /// Updates item.Id but doesn't read back other properties
    /// </summary>
    public T Create<T>(T item)
    {
        _provider.GetCollection<T>(typeof(T).Name).InsertOne(item);
        return item;
    }

    public void Update<T>(T item) where T : DatabaseObject
    {
        _provider.GetCollection<T>(typeof(T).Name).ReplaceOne(Builders<T>.Filter.Eq("_id", item.Id), item);
    }

    public void Delete<T>(ObjectId id)
    {
        _provider.GetCollection<T>(typeof(T).Name).DeleteOne(Builders<T>.Filter.Eq("_id", id));
    }
}

