using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository;

public class GenericRepository<TEntity> where TEntity : class
{
    internal DBContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(DBContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }


    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        int? pageIndex = null,
        int? pageSize = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null) query = query.Where(filter);

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty.Trim());

        if (orderBy != null) query = orderBy(query);

        // Implementing pagination
        if (pageIndex.HasValue && pageSize.HasValue)
        {
            // Ensure the pageIndex and pageSize are valid
            var validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
            var validPageSize =
                pageSize.Value > 0
                    ? pageSize.Value
                    : 10; // Assuming a default pageSize of 10 if an invalid value is passed

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIDAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual async Task DeleteAsync(object id)
    {
        var entityToDelete = await dbSet.FindAsync(id);
        await DeleteAsync(entityToDelete);
    }

    public virtual async Task DeleteAsync(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached) dbSet.Attach(entityToDelete);
        dbSet.Remove(entityToDelete);
        await context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entityToUpdate)
    {
        // context.Entry(entityToUpdate).State = EntityState.Detached;
        context.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }


    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null) query = query.Where(filter);
        return await query.CountAsync();
    }
}