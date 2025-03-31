using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : Entity<TId> 
        where TId : IEquatable<TId>
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task<bool> ExistsAsync(TId id)
        {
            return await DbSet.AnyAsync(entity => entity.Id != null && entity.Id.Equals(id));
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAllNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Remove(TEntity entity, bool hardDelete = false)
        {
            if (hardDelete)
            {
                DbSet.Remove(entity);
                return;
            }
            entity.Delete();
            DbSet.Update(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false)
        {
            if (hardDelete)
            {
                DbSet.RemoveRange(entities);
                return;
            }
            foreach (TEntity entity in entities)
            {
                entity.Delete();
            }
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.CountAsync(predicate);
        }
    }
}
