using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IBaseRepo<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Tüm verileri asenkron olarak listeler.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();

       
        /// <summary>
        /// Gelen Id ye göre veriyi asenkron olarak getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(object id);



        /// <summary>
        /// Asenkron şekilde filtreleme yapmak için kullanılır. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"> asNoTracking = false olmasının sebebi ise ben bir update yapmayacağım için efcore un izlemesine gerek yok. </param>
        /// <returns></returns>
        Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false);

        /// <summary>
        /// Asenkron olarak ekleme işlemi yapar.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Add(TEntity entity);

        /// <summary>
        ///  Güncelleme yapar.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(TEntity entity, TEntity unchanged);

        /// <summary>
        ///  Silmek için kullanılır.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Remove(TEntity entity);

        /// <summary>
        /// Query e göre getirme yapar.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();
    }
}