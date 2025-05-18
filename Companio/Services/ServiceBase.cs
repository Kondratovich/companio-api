using Companio.Models;
using Companio.Services.Interfaces;
using System.Linq.Expressions;

namespace Companio.Services;

public class ServiceBase<T> : IServiceBase<T> where T : DatabaseObject
{

    protected ServiceBase()
    {
    }

    public List<T> GetAll()
    {
        return new List<T>();
    }

    public List<T> Find(Expression<Func<T, bool>> filter)
    {
        return new List<T>();
    }

    public T SingleByIdOrDefault(Guid id)
    {
        return default(T);
    }

    public T Create(T item)
    {
        return default(T);
    }

    public void Update(T item)
    {
    }

    public void Delete(Guid id)
    {
    }
}