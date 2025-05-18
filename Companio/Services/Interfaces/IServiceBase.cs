using System.Linq.Expressions;
using MongoDB.Bson;

namespace Companio.Services.Interfaces;

public interface IServiceBase<T>
{
    List<T> GetAll();
    List<T> Find(Expression<Func<T, bool>> filter);
    T SingleByIdOrDefault(ObjectId id);
    T Create(T item);
    void Update(T item);
    void Delete(ObjectId id);
}