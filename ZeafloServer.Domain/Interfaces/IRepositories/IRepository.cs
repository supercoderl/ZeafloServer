using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain.Interfaces.IRepositories
{
    public interface IRepository<TEntity, TId> : IDisposable 
        where TEntity : Entity<TId> 
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Methods Add
        /// </summary>
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);


        /// <summary>
        /// Methods Get
        /// </summary>
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllNoTracking();
        Task<TEntity?> GetByIdAsync(TId id);

        /// <summary>
        /// Method Count
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Method Update
        /// </summary>
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Method Check
        /// </summary>
        Task<bool> ExistsAsync(TId id);

        /// <summary>
        /// Methods Remove
        /// </summary>
        void Remove(TEntity entity, bool hardDelete = false);
        void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false);
    }
}
