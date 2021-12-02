using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities;
using eogrenci.Dal.Abstract.EFIRepository;
using eogrenci.Dal.Concrete.Context;

namespace eogrenci.Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _context;

        public UnitOfWork(StudentDbContext context)
        {
            _context = context;
        }

        public IBaseRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            //Context i buradan gönderdiğimiz için EfRepository de artık Dependency Injection dan değil buradaki context den çalışıyor.
            return new EFBaseRepo<TEntity>(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
