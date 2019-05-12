using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITI.Enterprise.InterviewTask.Repositories.Services
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext: DbContext
    {
        private bool _disposed;
        protected readonly TContext Context;
        protected DbSet<TEntity> Set;
        public Repository(TContext context)
        {
            Context = context;
            Set = Context.Set<TEntity>();
            _disposed = false;

        }
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Set.Add(entity);
           SaveChanges();
            return entity;

        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Set.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public int Count()
        {
            return Set.Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await Set.CountAsync();
        }

        public void Delete(TEntity entity)
        {
           Set.Remove(entity);
           Context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            Set.Remove(entity);
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
           return Set.SingleOrDefault(match);
        }

        public virtual ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return Set.Where(match).ToList();
        }

        public virtual  async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Set.Where(match).ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Set.FirstOrDefaultAsync(match);
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate);
        }

        public virtual async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Set.Where(predicate).ToListAsync();
        }

        public virtual TEntity Get(object id)
        {
            return Set.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Set;
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = GetAll();
            foreach (var includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }


        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await Set.FindAsync(id);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity, object key)
        {
            if (entity == null)
                return null;
            var exist =Set.Find(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(entity);
               SaveChanges();
            }
            return exist;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, object key)
        {
            if (entity == null)
                return null;
            var exists = Set.FindAsync(key);
            if (exists != null)
            {
                Context.Entry(exists).CurrentValues.SetValues(entity);
                await Context.SaveChangesAsync();
            }

            return await exists;
        }
    }
}
