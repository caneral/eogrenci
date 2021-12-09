using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class EFBaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity, new()
    {

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFBaseRepo(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

        }

        /// <summary>
        /// Asenkron bir şekilde ekleme işlemi yapmak için kullanılır.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }


        /// <summary>
        /// Tracking olmadan listeleme işlemi yapılır.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                if (asNoTracking)
                {
                    query = query.Where(filter);
                }
                query = query.AsNoTracking().Where(filter);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }

            return  await query.ToListAsync();
        }



        /// <summary>
        /// AsNoTracking durumuna göre filtreleme yap.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ?
                await _dbSet.SingleOrDefaultAsync(filter) :
                await _dbSet.AsNoTracking().SingleOrDefaultAsync(filter);
        }


        /// <summary>
        /// Id ye göre getirme işlemi yapar. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }



        /// <summary>
        /// Query olarak getirmek için.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }


        /// <summary>
        /// Remove async değildir. Burada aslında bir güncelleme işlemi yapıyoruz. Kayıtları silmiyoruz.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Update async değildir. Veritabanında zaten var olduğunu bildiğimiz ancak üzerinde değişiklik yapılmış olabilecek bir varlığımız varsa, içeriğe varlığı eklemesini ve durumunu değiştirildi olarak güncellemesini söylemek için Entry kullanırız.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity, TEntity unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(entity);
        }



        public IQueryable<TEntity> GetListQueryable(Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetQueryable(filter, includeProperties);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
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

    }
}
