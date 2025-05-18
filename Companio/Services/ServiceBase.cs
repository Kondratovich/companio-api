using System.Linq.Expressions;
using Companio.Models;
using Companio.Mongo;
using Companio.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Companio.Services;

public class ServiceBase<T> : IServiceBase<T> where T : DatabaseObject
{
    private readonly MongoContext _mongoContext;

    protected ServiceBase(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public List<T> GetAll()
    {
        return _mongoContext.GetAll<T>().ToList();
    }

    public List<T> Find(Expression<Func<T, bool>> filter)
    {
        return _mongoContext.Find(Builders<T>.Filter.Where(filter)).ToList();
    }

    public T SingleByIdOrDefault(ObjectId id)
    {
        return _mongoContext.SingleByIdOrDefault<T>(id);
    }

    public T Create(T item)
    {
        return _mongoContext.Create(item);
    }

    public void Update(T item)
    {
        _mongoContext.Update(item);
    }

    public void Delete(ObjectId id)
    {
        _mongoContext.Delete<T>(id);
    }
}