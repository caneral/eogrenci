using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class EFBaseeRepo<TEntity> : IBaseeRepo<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public EFBaseeRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _dbSet.Count() : _dbSet.Where(filter).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter == null ? _dbSet.CountAsync() : _dbSet.Where(filter).CountAsync());
        }

        public void Delete(int id, bool IsHardDelete = false)
        {
            var currentEntity = GetById(id);
            if (currentEntity != null)
            {
                Delete(currentEntity, IsHardDelete);
            }
        }

        public void Delete(TEntity entity, bool IsHardDelete = false)
        {
            if (!IsHardDelete)
            {
                entity.IsDeleted = true;
                Update(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
                _dbSet.Remove(entity);
            }
        }

        public void DeleteAll(IEnumerable<TEntity> entities, bool IsHardDelete = false)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    Delete(entity, IsHardDelete);
                }
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            query = query.Where(filter);
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return query.SingleOrDefault();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            var sorgu = query.Where(filter);
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return await sorgu.SingleOrDefaultAsync();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetListQueryable(filter, includeProperties).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await GetListQueryable(filter, includeProperties).ToListAsync();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return query;
        }
        public IQueryable<TEntity> GetListQueryable(Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetQueryable(filter, includeProperties);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            {
                return filter == null
                    ? _dbSet.ToList()
                    : _dbSet.Where(filter).ToList();
            }
        }
        //Sonradan eklenenler
        public TEntity GetByyId(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Addd(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AdddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(int id)
        {
            _dbSet.Remove(GetByyId(id));
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
