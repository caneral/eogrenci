using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities;
using eogrenci.Dal.Abstract.EFIRepository;

namespace eogrenci.Dal.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        Task SaveChanges();
    }
}
