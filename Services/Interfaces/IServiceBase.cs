using MongoDB.Bson;

namespace Companio.Services.Interfaces;

public interface IServiceBase<T>
{
    List<T> GetAll();
    T SingleByIdOrDefault(ObjectId id);
    T Create(T item);
    void Update(T item);
    void Delete(ObjectId id);
}