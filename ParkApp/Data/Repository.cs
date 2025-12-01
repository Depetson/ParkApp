using Microsoft.EntityFrameworkCore;
using ParkApp.Data.Interfaces;
using System.Linq.Expressions;

namespace ParkApp.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ParkAppDbContext _ctx;

    public Repository(ParkAppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _ctx.Set<T>().FirstOrDefaultAsync(predicate, new CancellationToken());
    }

    public async Task<List<T>> GetAll(bool withoutTracking = true)
    {
        DbSet<T> set = _ctx.Set<T>();
        if (withoutTracking)
        {
            set.AsNoTracking<T>();
        }
        return await set.ToListAsync<T>();
    }

    public void Update(T entity)
    {
        _ctx.Set<T>().Update(entity);
        _ctx.SaveChanges();
    }
}
