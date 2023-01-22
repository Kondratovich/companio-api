using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Companio.Mongo;

public class MongoContext
{
    public IMongoDatabase Provider;

    public MongoContext(IOptions<MongoSettings> mongoSettings)
    {
        MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
        Provider = client.GetDatabase(mongoSettings.Value.DatabaseName);
    }
}

