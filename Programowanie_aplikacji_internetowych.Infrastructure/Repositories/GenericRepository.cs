using Microsoft.EntityFrameworkCore;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbContext.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        var items = await _dbContext.Set<T>().ToListAsync();
        return items;
    }

    public virtual async Task<T> GetById(Guid id)
    {
        var item = await _dbContext.Set<T>().FindAsync(id);
        return item;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }


}
