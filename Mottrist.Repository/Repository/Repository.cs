using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Mottrist.Domain.Common.Interfaces;
using Mottrist.Domain.Common.IRepository;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Domain.Common;

namespace Mottrist.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Table => _dbSet;

        public void Add(TEntity entity)
        {
            _OnAdd(entity);
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            _OnAdd(entity);
            await _dbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _OnAdd(entity);
            }
            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _OnAdd(entity);
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var entities = _dbSet.Where(filter);
            _dbSet.RemoveRange(entities);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            await Task.Run(() =>
            {
                var entities = _dbSet.Where(filter);
                _dbSet.RemoveRange(entities);
            });
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.RemoveRange(entities));
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? _dbSet : _dbSet.Where(filter);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return await (filter == null ? _dbSet.ToListAsync() : _dbSet.Where(filter).ToListAsync());
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        //public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    foreach (var includeProperty in includeProperties)
        //        query = query.Include(includeProperty);

        //    return query;
        //}

        /// <summary>
        /// Dynamically includes navigation properties for a query.
        /// </summary>
        /// <param name="includeProperties">An array of expressions representing the navigation properties to include.</param>
        /// <returns>
        /// An <see cref="IQueryable{TEntity}"/> with the specified navigation properties included.
        /// </returns>
        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Validate input
            if (includeProperties == null || includeProperties.Length == 0)
            {
                throw new ArgumentException("Include properties cannot be null or empty.", nameof(includeProperties));
            }

            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                // Validate each includeProperty
                if (includeProperty == null)
                {
                    throw new ArgumentException("One of the include properties is null.", nameof(includeProperties));
                }

                try
                {
                    query = query.Include(includeProperty);
                }
                catch (Exception ex)
                {
                    // Re-throw the exception or handle it as needed
                    throw new InvalidOperationException($"Failed to include property {includeProperty}.", ex);
                }
            }

            return query;
        }

        public void SoftDelete(TEntity entity)
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedAt = DateTime.Now;
                Update(entity);
            }
            else
            {
                throw new InvalidOperationException("Entity does not support soft delete.");
            }
        }

        public void SoftDelete(Expression<Func<TEntity, bool>> filter)
        {
            var entities = _dbSet.Where(filter).ToList();

            foreach (var entity in entities)
            {
                SoftDelete(entity);
            }
        }

        public async Task SoftDeleteAsync(TEntity entity)
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedAt = DateTime.Now;
                await UpdateAsync(entity);
            }
            else
            {
                throw new InvalidOperationException("Entity does not support soft delete.");
            }
        }

        public async Task SoftDeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            var entities = await _dbSet.Where(filter).ToListAsync();
            foreach (var entity in entities)
            {
                await SoftDeleteAsync(entity);
            }
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.UpdateRange(entities));
        }
        private void _OnAdd(TEntity entity)
        {
            if (entity is ICreateAt createAt)
            {
                createAt.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}
