using System.Linq.Expressions;

namespace ParkApp.Data.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T?> Get(Expression<Func<T, bool>> predicate);
    public Task<List<T>> GetAll(bool withoutTracking = true);
    public void Update(T parkingSpace);
}
