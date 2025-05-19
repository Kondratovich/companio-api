using Companio.Data;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Companio.Services;

public class ServiceBase<T> : IServiceBase<T> where T : DatabaseObject
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _set;

    protected ServiceBase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = _dbContext.Set<T>();
    }

    public List<T> GetAll()
    {
        return _set.ToList();
    }

    public List<T> Find(Expression<Func<T, bool>> filter)
    {
        return _set.Where(filter).ToList();
    }

    public T SingleByIdOrDefault(Guid id)
    {
        return _set.FirstOrDefault(x => x.Id == id);
    }

    public T Create(T item)
    {
        item.Id = Guid.NewGuid();
        _set.Add(item);
        _dbContext.SaveChanges();
        return item;
    }

    public void Update(T item)
    {
        _set.Update(item);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var item = _set.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _set.Remove(item);
            _dbContext.SaveChanges();
        }
    }
}