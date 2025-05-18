using System.Linq.Expressions;

namespace Companio.Services.Interfaces;

public interface IServiceBase<T>
{
    List<T> GetAll();
    List<T> Find(Expression<Func<T, bool>> filter);
    T SingleByIdOrDefault(Guid id);
    T Create(T item);
    void Update(T item);
    void Delete(Guid id);
}